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
        private string password = "blahimmel";
        private string newPassword = CreatePassword(10);
        private MailMessage msg;

        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void sendPasswordButton_Click(object sender, EventArgs e)
        {
            string email = string.Empty;

            try
            {
                msg = new MailMessage();
                SmtpClient smtp = new SmtpClient();
                email = Email.Text.Trim();
                //bruker gruppe eposten som avsender
                msg.From = new MailAddress("sysut14gr03@gmail.com");
                msg.To.Add(email);

                updatePassword(email, newPassword);
                
                msg.Subject = "tilsendt nytt passord";

                msg.Body = "Hei " + Email.Text.Trim() + "!\n" + "Her har du et nytt passord for din bruker: " + newPassword + "\nVi vil anbefale deg å å skifte passord når du får logget deg inn til noe som er mer personlig";
                msg.IsBodyHtml = true;
                smtp.Credentials = new NetworkCredential("sysut14gr03@gmail.com", password);
                smtp.Port = 587;
                smtp.Host = "smtp.gmail.com";
                smtp.EnableSsl = true;
                smtp.Send(msg);

                Email.Text = string.Empty;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "Du har nu fått tilsendt et nytt passord", true);

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Error occured : " + ex.Message.ToString() + "');", true);
                return;
            }
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