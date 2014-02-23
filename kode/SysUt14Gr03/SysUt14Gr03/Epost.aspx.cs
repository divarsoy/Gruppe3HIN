using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SysUt14Gr03
{
    public partial class Epost : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void CreateUser_Click(object sender, EventArgs e)
        {

            MailMessage msg;

            string ActivationUrl = string.Empty;
            string email = string.Empty;

            try
            {
                Guid token = Guid.NewGuid();

                msg = new MailMessage();
                SmtpClient smtp = new SmtpClient();
                email = Email.Text.Trim();
                //bruker gruppe eposten som avsender
                msg.From = new MailAddress("sysut14gr03@gmail.com");
                msg.To.Add(email);

                msg.Subject = "Bekreftelses epost for konto aktivering";

                //har begynt å lage en aktiverkonto side 
                ActivationUrl = Server.HtmlEncode("http://localhost:60154/AktiverKonto.aspx?Epost=" + email + "&Token=" + token);

                msg.Body = "Hei " + UserName.Text.Trim() + "!\n" + "Takk for at du registrerte deg hos oss\n" + " <a href='" + ActivationUrl + "'>Klikk her for å aktivere</a>  din konto.";
                msg.IsBodyHtml = true;
                smtp.Credentials = new NetworkCredential("sysut14gr03@gmail.com", "blahimmel");
                smtp.Port = 587;
                smtp.Host = "smtp.gmail.com";
                smtp.EnableSsl = true;
                smtp.Send(msg);


                clear_controls();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('En link for å aktivere kontoen din er sendt til eposten din');", true);

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Error occured : " + ex.Message.ToString() + "');", true);
                return;
            }
        }
        private void clear_controls()
        {
            UserName.Text = string.Empty;
            Email.Text = string.Empty;
            UserName.Focus();
        }
    }
}