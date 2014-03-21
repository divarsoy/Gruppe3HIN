using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.UI;
using SysUt14Gr03.Models;

namespace SysUt14Gr03.Classes
{
    public class sendEmail : System.Web.UI.Page // (bør bruke stor forbokstav i klassenavn)
    {
        private string password = "blahimmel";
        private MailMessage msg;

        public void sendEpost(string epost, string message, string subject, string activationURL, List<Bruker> users, ArrayList brukere)
        {
            try
            {
                msg = new MailMessage();
                SmtpClient smtp = new SmtpClient();

                //bruker gruppe eposten som avsender
                msg.From = new MailAddress("sysut14gr03@gmail.com");
                /* Tips: bruk overload-metoder i stedet for if-tester              
                 * sendEpost(string epost, string message, string subject) {
                 * sendEpost(epost, message, subject, null, null, null);
                 * }
                 **/
                if(users != null)
                    for (int i = 0; i < users.Count; i++)
                        msg.To.Add(users[i].Epost);
                else if(brukere != null)
                    for (int i = 0; i < users.Count; i++)
                        msg.To.Add(brukere[i].ToString());
                else
                    msg.To.Add(epost);

                msg.Subject = subject;

                msg.Body = message;
                msg.IsBodyHtml = true;
                smtp.Credentials = new NetworkCredential("sysut14gr03@gmail.com", password);
                smtp.Port = 587;
                smtp.Host = "smtp.gmail.com";
                smtp.EnableSsl = true;
                smtp.Send(msg);

                if(activationURL != null)
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('En link for å aktivere brukerkontoen er sendt til brukereposten');", true);

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Error occured : " + ex.Message.ToString() + "');", true);
                return;
            }
        }
    }
}