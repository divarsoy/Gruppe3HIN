using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
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
        private Bruker bruker = new Bruker();
        private string brukernavn;
        private string etternavn;
        private string fornavn;
        private string epost;
        private string imAdresse;
        private string passord;

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

                    Aftername.Text = Request.QueryString["Etternavn"];
                    Firstname.Text = Request.QueryString["Brukernavn"];
                    epost = Email.Text = Request.QueryString["Epost"];
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
        protected void confirmUser(string _brukernavn, string _etternavn, string _fornavn, string _epost, string _imadresse, string _passord)
        {
            using (var db = new Context())
            {

                var confirmUser = new Bruker { Brukernavn = brukernavn, Etternavn = etternavn, Fornavn = fornavn, Epost = epost, IM = imAdresse, Passord = passord };
                db.Brukere.Add(confirmUser);
                db.SaveChanges();
            }
        }
        protected void ConfirmButton_Click(object sender, EventArgs e)
        {
            try
            {
                passord = ComputeHash(Password.Text, new SHA256CryptoServiceProvider());
                epost = Email.Text;
                brukernavn = Username.Text;
                etternavn = Aftername.Text;
                fornavn = Firstname.Text;
                imAdresse = Im_adress.Text;

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
        private string ComputeHash(string input, HashAlgorithm algorithm)
        {
            Byte[] inputBytes = Encoding.UTF8.GetBytes(input);

            Byte[] hashedBytes = algorithm.ComputeHash(inputBytes);

            return BitConverter.ToString(hashedBytes);
        }
    }
}
