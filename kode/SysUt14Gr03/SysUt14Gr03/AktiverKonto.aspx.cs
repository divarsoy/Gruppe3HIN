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
        private string brukernavn;
        private string etternavn;
        private string fornavn;
        private string epost;
        private string imAdresse;
        private string passord;
        private string token;
    
        private List<Bruker> brukerList;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                ActivateMyAccount();
            }
        }

        private void ActivateMyAccount()
        {

            try
            {


                if ((!string.IsNullOrEmpty(Request.QueryString["Epost"])) & (!string.IsNullOrEmpty(Request.QueryString["Token"])))
                {
                    Response.Write("<h2 align=center> Fyll ut resterende felt for å aktivere kontoen din</h2>");

                    brukerList = Queries.GetBruker(Request.QueryString["Epost"]);
                    for (int i = 0; i < brukerList.Count; i++)
                    {
                        Bruker bruker = brukerList[i];
                        Aftername.Text = bruker.Etternavn;
                        Firstname.Text = bruker.Fornavn;
                        epost = Email.Text = Request.QueryString["Epost"];
                        
                     
                    }

                    token = Request.QueryString["Token"];
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
                return;
            }


        }
     
        protected void ConfirmButton_Click(object sender, EventArgs e)
        {
            try
            {
             //   passord = ComputeHash(Password.Text, new SHA256CryptoServiceProvider());
                passord = MD5Hash(Password.Text); 
                epost = Email.Text;
                brukernavn = Username.Text;
                etternavn = Aftername.Text;
                fornavn = Firstname.Text;
                imAdresse = Im_adress.Text;
            

                using (var db = new Context())
                {
                    var conUser = db.Brukere.Where(user => user.Epost == epost).First();
                    conUser.Brukernavn = brukernavn;
                    conUser.Etternavn = etternavn;
                    conUser.Epost = epost;
                    conUser.IM = imAdresse;
                    conUser.Passord = passord;
                    conUser.Token = token;
                    conUser.Aktiv = true;
                    db.Brukere.Add(conUser);
                    db.SaveChanges();
                }
                // confirmUser(Username.Text, Aftername.Text, Firstname.Text, Email.Text, Im_adress.Text, password);
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
