using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SysUt14Gr03.Classes;
using SysUt14Gr03.Models;

namespace SysUt14Gr03
{
    public partial class changeUser : System.Web.UI.Page
    {
        private int userID;
        private string oldDBPassword;
        private lostPassword updatePassord;
        private string newPassword;
        private string oldPassword;
        private string confirmPassword;

        protected void Page_PreInit(Object sener, EventArgs e)
        {
            string master = SessionSjekk.findMaster();
            this.MasterPageFile = master;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["bruker_id"] != null)
            {
                updatePassord = new lostPassword();
                userID = Validator.KonverterTilTall(Session["bruker_id"].ToString());
                Bruker bruker = Queries.GetBruker(userID);
                oldDBPassword = bruker.Passord;
                FirstName.Text = bruker.Fornavn;
                SurName.Text = bruker.Etternavn;
                UserName.Text = bruker.Brukernavn;
                Email.Text = bruker.Epost;
                IM.Text = bruker.IM;
                OldPassord.Text = bruker.Passord;
            }
            else
                Response.Redirect("Login.aspx");
        }
        protected void btnPasswordChange_Click(object sender, EventArgs e)
        {
            HiddenPanel.Visible = true;
        }
        protected void btnUserChange_Click(object sender, EventArgs e)
        {
            string firstName = FirstName.Text.Trim();
            string surName = SurName.Text.Trim();
            string userName = UserName.Text.Trim();
            string email = Email.Text.Trim();
            string im = IM.Text.Trim();

            updateUser(email, firstName, surName, userID, im, userName);

            if(HiddenPanel.Visible)
            {
                oldPassword = OldPassord.Text.Trim();
                newPassword = NewPassword.Text.Trim();
                confirmPassword = ConfirmPassword.Text.Trim();
                
                if(oldDBPassword == oldPassword && newPassword == confirmPassword)
                    updatePassord.updatePassword(email, newPassword);
            }
        }
        public void updateUser(string email,string firstnavn, string surName, int id, string im, string userName)
        {
            using (SqlCommand command = new SqlCommand())
            {
                command.Connection = new SqlConnection(ConfigurationManager.ConnectionStrings["sysUt14Gr03"].ConnectionString);
                command.CommandText = @"update Bruker where Bruker_id='" + id + "' set Etternavn='" + surName + "' set ForNavn='" + firstnavn + "' set Brukernavn='" + userName + "' set Epost='" + email + "' set IM='" + im + "'";
                command.Connection.Open();
                command.ExecuteNonQuery();
                command.Connection.Close();
            }
        }
    }
}