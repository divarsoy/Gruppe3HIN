using System;
using System.Collections;
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
        private Login getInfo;
        private int userID;
        private string oldDBPassword = "johnsen";
        private lostPassword updatePassord;
        private string newPassword;
        private string oldPassword;
        private string confirmPassword;

        protected void Page_Load(object sender, EventArgs e)
        {
            getInfo = new Login();
            updatePassord = new lostPassword();
            userID = getInfo.getBrukerID();
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
        public ArrayList getUserInfo()
        {
            using (SqlCommand command = new SqlCommand())
            {
                string query = "SELECT * FROM Bruker WHERE Bruker_id = " + userID + "'";
                command.Connection = new SqlConnection(ConfigurationManager.ConnectionStrings["sysUt14Gr03"].ConnectionString);

                var list = new ArrayList();
                var reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        string etterNavn = reader.GetString(1);
                        string forNavn = reader.GetString(2);
                        string brukerNavn = reader.GetString(3);
                        string epost = reader.GetString(4);
                        oldDBPassword = reader.GetString(5);
                        string im = reader.GetString(6);
                        list.Add(forNavn);
                        list.Add(etterNavn);
                        list.Add(brukerNavn);
                        list.Add(epost);
                        list.Add(oldDBPassword);
                        list.Add(im);
                    }
                }
                else
                {
                    string respons = "Fikk ikke hentet ut informasjon fra tabellen Bruker";
                    list.Add(respons);
                }

                reader.Close();
                command.Connection.Close();
                return list;
            }
        }
    }
}