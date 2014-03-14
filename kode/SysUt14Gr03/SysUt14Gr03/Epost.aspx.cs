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
        private MailMessage msg;
        private Classes.sendEmail sendMsg;
        private string ActivationUrl;
        private string email;

        protected void Page_Load(object sender, EventArgs e)
        {
            msg = new MailMessage();
            sendMsg = new Classes.sendEmail();
        }
        protected void CreateUser_Click(object sender, EventArgs e)
        {
            Guid token = Guid.NewGuid();
            email = Email.Text.Trim();
            msg.Subject = "Bekreftelses epost for konto aktivering";
            //har begynt å lage en aktiverkonto side 
            ActivationUrl = Server.HtmlEncode("http://localhost:60154/AktiverKonto.aspx?Epost=" + email + "&Token=" + token);
            msg.Body = "Hei " + UserName.Text.Trim() + "!\n" + "Takk for at du registrerte deg hos oss\n" + " <a href='" + ActivationUrl + "'>Klikk her for å aktivere</a>  din konto.";

            sendMsg.sendEpost(email, msg.Body, msg.Subject, ActivationUrl, null, null);
        }
        private void clear_controls()
        {
            UserName.Text = string.Empty;
            Email.Text = string.Empty;
            UserName.Focus();
        }
    }
}