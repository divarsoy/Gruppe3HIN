using System;
using System.Collections.Generic;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SysUt14Gr03.Models;
using SysUt14Gr03.Classes;

/// <summary>
/// Festet til innlogging siden. Hvis man nu har mistet/glemt passordet sitt, så kan man få et nytt et. 
/// Webformen her får inn et bruker navn hvor han da henter ut resten av brukeren sin informasjon fra databasen utfra det bruker navnet. 
/// hvis han da får hentet ut fra databasen (bruker != null), så betyr det at brukeren eksisterer. Han setter da det nye passordet til en random 
/// 10 strings lengde med store/små bokstaver og tall. Salter og hasher passordet og oppdatere databasen. også sende den en email til brukeren 
/// om det nye passordet i klar tekst. 
/// </summary>

namespace SysUt14Gr03
{
    public partial class lostPassword : System.Web.UI.Page
    {
        private string newPassword;
        private MailMessage msg;
        private Classes.sendEmail sendMsg;
        private Bruker bruker;
        private string brukernavn;
        private bool updated;

        protected void Page_PreInit(Object sener, EventArgs e)
        {
            string master = SessionSjekk.findMaster();
            this.MasterPageFile = master;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            updated = false;
            brukernavn = Email.Text.Trim();
            bruker = Classes.Queries.GetBrukerVedBrukernavn(brukernavn);
            msg = new MailMessage();
            sendMsg = new Classes.sendEmail();
        }

        protected void sendPasswordButton_Click(object sender, EventArgs e)
        {
            if (bruker != null)
            {
                newPassword = CreatePassword(10);
                string email = bruker.Epost;
                msg.Subject = "Tilsendt nytt passord";
                msg.Body = "Hei " + email + "!\n" + "Her har du et nytt passord for din bruker: " + newPassword + "\nVi vil anbefale deg å å skifte passord når du får logget deg inn til noe som er mer personlig";
                updatePassword(email, newPassword);
                if (updated)
                {
                    sendMsg.sendEpost(email, msg.Body, msg.Subject, null, null, null);
                    Session["flashMelding"] = "Du har nu fått en epost om ditt nye passord";
                    Session["flashStatus"] = Konstanter.notifikasjonsTyper.success.ToString();
                    Response.Redirect("Login.aspx");
                }
                else
                {
                    Session["flashMelding"] = "Ville ikke oppdateres i databasen";
                    Session["flashStatus"] = Konstanter.notifikasjonsTyper.danger.ToString();
                }
            }
            else
            {
                Session["flashMelding"] = "Brukeren eksisterer ikke!";
                Session["flashStatus"] = Konstanter.notifikasjonsTyper.danger.ToString();
            }
        }

        public static string CreatePassword(int length)
        {
            string valid = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            string res = "";
            Random rnd = new Random();
            while (0 < length--)
                res += valid[rnd.Next(valid.Length)];
            return res;
        }

        public void updatePassword(string email, string passord)
        {
            if (email != null)
            {
                using (var context = new Context())
                {
                    bruker = context.Brukere.Where(b => b.Epost == email).FirstOrDefault();
                    Hashtable table = Classes.Hash.GetHashAndSalt(passord);
                    bruker.Passord = table["hash"].ToString();
                    bruker.Salt = table["salt"].ToString();

                    context.SaveChanges();
                    updated = true;
                }
            }
        }
    }
}