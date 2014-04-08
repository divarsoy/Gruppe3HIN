using System;
using System.Collections.Generic;
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

namespace SysUt14Gr03
{
    public partial class lostPassword : System.Web.UI.Page
    {
        private string newPassword = CreatePassword(10);
        private MailMessage msg;
        private Classes.sendEmail sendMsg;

        protected void Page_Load(object sender, EventArgs e)
        {
            msg = new MailMessage();
            sendMsg = new Classes.sendEmail();
        }
        protected void sendPasswordButton_Click(object sender, EventArgs e)
        {
            string email = Email.Text.Trim();
            updatePassword(email, newPassword);
            msg.Subject = "Tilsendt nytt passord";
            msg.Body = "Hei " + email + "!\n" + "Her har du et nytt passord for din bruker: " + newPassword + "\nVi vil anbefale deg å å skifte passord når du får logget deg inn til noe som er mer personlig";

            sendMsg.sendEpost(email, msg.Body, msg.Subject, null, null, null);
        }
        public static string CreatePassword(int length)
        {
            string valid = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890,.+?!";
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
                using (SqlCommand command = new SqlCommand())
                {
                    command.Connection = new SqlConnection(ConfigurationManager.ConnectionStrings["sysUt14Gr03"].ConnectionString);
                    command.CommandText = @"update Bruker where Epost='" + email + "'set Passord='" + passord + "'";
                    command.Connection.Open();
                    command.ExecuteNonQuery();
                    command.Connection.Close();
                }
            }
        }
    }
}