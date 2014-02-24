using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SysUt14Gr03
{
    public partial class changeUser : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }
        protected void btnPasswordChange_Click(object sender, EventArgs e)
        {
            HiddenPanel.Visible = true;
        }
        protected void btnUserChange_Click(object sender, EventArgs e)
        {
            int id = 0;
            string firstName = FirstName.Text.Trim();
            string surName = SurName.Text.Trim();
            string userName = UserName.Text.Trim();
            string email = Email.Text.Trim();
            string im = IM.Text.Trim();
            string oldPassword = OldPassord.Text.Trim();
            string newPassword = NewPassword.Text.Trim();
            string confirmPassword = ConfirmPassword.Text.Trim();

            updateUser(email, firstName, surName, id, im, userName, newPassword);
        }
        public void updateUser(string email,string firstnavn, string surName, int id, string im, string userName, string password)
        {
            using (SqlCommand command = new SqlCommand())
            {
                command.Connection = new SqlConnection(ConfigurationManager.ConnectionStrings["sysUt14Gr03"].ConnectionString);
                command.CommandText = @"update Bruker where Bruker_id='" + id + "' set Etternavn='" + surName + "' set ForNavn='" + firstnavn + "' set Brukernavn='" + userName + "' set Epost='" + email + "' set IM='" + im + "' set Passord=" + password + "'";
                command.Connection.Open();
                command.ExecuteNonQuery();
                command.Connection.Close();
            }
        }
    }
}