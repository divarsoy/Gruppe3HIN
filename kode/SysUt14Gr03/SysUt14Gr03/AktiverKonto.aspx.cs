using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SysUt14Gr03.Classes;
using SysUt14Gr03.Models;


namespace SysUt14Gr03
{
    public partial class AktiverKonto : System.Web.UI.Page
    {
       // static string initialFornavn;
       // static string initialEtternavn;
      //  static string initialEpost;
        private string brukernavn;
        private string etternavn;
        private string fornavn;
        private string epost;
        private string imAdresse;
        private string token;
        private int bruker_id;
        private bool check = true;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                ActivateMyAccount();
            }
        }

   /*     public static void SetBrukerFelter(string _fornavn, string _etternavn, string _epost) {
            initialFornavn = _fornavn;
            initialEtternavn = _etternavn;
            initialEpost = _epost;
        } */

        private void ActivateMyAccount()
        {

            try
            {
                if ((!string.IsNullOrEmpty(Request.QueryString["Epost"])) & (!string.IsNullOrEmpty(Request.QueryString["Token"])))
                {
                    using (var context = new Context())
                    {
                        epost = Email.Text = Request.QueryString["Epost"];
                        Bruker bruk = context.Brukere.Where(b => b.Epost == epost).First();
                        bruker_id = bruk.Bruker_id;
                        Firstname.Text = bruk.Fornavn;
                        Aftername.Text = bruk.Etternavn;
                    }
                    Session["flashMelding"] = "<h2 align=center> Fyll ut resterende felt for å aktivere kontoen din</h2>";
                    Session["flashStatus"] = Konstanter.notifikasjonsTyper.danger;
                }
                else
                {
                    disable();
                    Session["flashMelding"] = "<h2 align=center>Det skjedde noe galt, Kontoen din ble ikke aktivert!</h2>";
                    Session["flashStatus"] = Konstanter.notifikasjonsTyper.danger;
                    //Response.Write("<h2 align=center>Det skjedde noe galt, Kontoen din ble ikke aktivert!</h2>");
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Error occured : " + ex.Message.ToString() + "');", true);
                Response.Redirect("RegistreringAvBrukere.aspx");
                return;
                
            }
        } 
     
        protected void ConfirmButton_Click(object sender, EventArgs e)
        {
            if (Password.Text == ConfirmPassword.Text)
            {
                token = Request.QueryString["Token"];
                //passord = MD5Hash(Password.Text);
                //passord = Passord.HashPassord(Password.Text);
                string salt = Hash.GetSalt();
                string hash = Hash.GetHash(Password.Text, salt);
                epost = Email.Text;
                brukernavn = Username.Text;
                etternavn = Aftername.Text;
                fornavn = Firstname.Text;
                imAdresse = Im_adress.Text;
                // int id = bruker_id;

                using (var db = new Context())
                {
                    var Bruker = (from bruker in db.Brukere
                                  where bruker.Epost == epost
                                  where bruker.Aktiv == false
                                  select bruker).FirstOrDefault();

                    // Default rettighet er utvikler
                    // string rettighetUtviklerString = Konstanter.rettighet.Utvikler.ToString();
                    // var rettighetUtvikler = db.Rettigheter.Where(rettighet => rettighet.RettighetNavn == rettighetUtviklerString).FirstOrDefault();
                    var queryBrukernavn = db.Brukere.FirstOrDefault(b => b.Brukernavn == brukernavn);
                    if (queryBrukernavn != null)
                    {
                        Session["flashMelding"] = "Brukernavnet er allerede tatt";
                        Session["flashStatus"] = Konstanter.notifikasjonsTyper.danger;
                        check = false;
                    }
                    if (check)
                    {
                        Bruker.Aktiv = true;
                        Bruker.Brukernavn = brukernavn;
                        Bruker.Epost = epost;
                        Bruker.Etternavn = etternavn;
                        Bruker.IM = imAdresse;
                        Bruker.Passord = hash;
                        Bruker.Token = token;
                        Bruker.Salt = salt;
                        db.SaveChanges();
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Kontoen din er aktivert, du vil bli videresendt til logg inn siden');", true);

                        Response.AddHeader("REFRESH", "4;URL=Login.aspx");
                        //Response.Write("<h2>Du kan logge deg inn nå  <a href=Login.aspx>Klikk her for å logge inn</a> </h2>");
                        disable();
                    }
                }
            }
            else
            {
                Session["flashMelding"] = "Vennligst fyll inn like passord";
                Session["flashStatus"] = Konstanter.notifikasjonsTyper.danger;
                Username.Text = "";
                Im_adress.Text = "";
                Password.Text = "";
                ConfirmPassword.Text = "";
                Response.Redirect(Request.RawUrl);
            }
        }
        protected void disable()
        {
            Username.Visible = false;
            lblUsername.Visible = false;
            Aftername.Visible = false;
            lblAftername.Visible = false;
            Firstname.Visible = false;
            lblFirstname.Visible = false;
            Email.Visible = false;
            lblEmail.Visible = false;
            Im_adress.Visible = false;
            lblImadress.Visible = false;
            Password.Visible = false;
            ConfirmPassword.Visible = false;
            lblPassword.Visible = false;
            lblConfirmPassword.Visible = false;
            ConfirmButton.Visible = false;
        }
    } 
}
