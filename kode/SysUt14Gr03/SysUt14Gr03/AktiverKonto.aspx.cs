using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
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
        private string passord;
        private string token;
        private int bruker_id;


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
                    Response.Write("<h2 align=center> Fyll ut resterende felt for å aktivere kontoen din</h2>");
                    using (var context = new Context())
                    {
                        epost = Email.Text = Request.QueryString["Epost"];
                        Bruker bruk = context.Brukere.Where(b => b.Epost == epost).First();
                        bruker_id = bruk.Bruker_id;
                        Firstname.Text = bruk.Fornavn;
                        Aftername.Text = bruk.Etternavn;
                       
                    }
                    
                }
                else
                {
                    disable();
                    Response.Write("<h2 align=center>Det skjedde noe galt, Kontoen din ble ikke aktivert!</h2>");

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
            try
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
                                  select bruker).FirstOrDefault();

                    Bruker.Aktiv = true;
                    Bruker.Brukernavn = brukernavn;
                    Bruker.Epost = epost;
                    Bruker.Etternavn = etternavn;
                    Bruker.IM = imAdresse;
                    Bruker.Passord = hash;
                    Bruker.Token = token;
                    Bruker.Salt = salt;                                   
                    db.SaveChanges();
                
                }
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Kontoen din er aktivert');", true);

                Response.Write("<h2>Du kan logge deg inn nå  <a href=Login.aspx>Klikk her for å logge inn</a> </h2>");
                disable();

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Error occured : " + ex.Message.ToString() + "');", true);
                return;
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
            lblPassword.Visible = false;
            ConfirmButton.Visible = false;
        }
    
        public static string MD5Hash(string text)
        {
            MD5 md5 = new MD5CryptoServiceProvider();

            //compute hash from the bytes of text
            md5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(text));

            //get hash result after compute it
            byte[] result = md5.Hash;

            StringBuilder strBuilder = new StringBuilder();
            for (int i = 0; i < result.Length; i++)
            {
                //change it into 2 hexadecimal digits
                //for each byte
                strBuilder.Append(result[i].ToString("x2"));
            }

            return strBuilder.ToString();
        }
    } 
}
