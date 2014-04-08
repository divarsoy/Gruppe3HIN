using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net.Mail;
using System.Net;
using System.Web.UI;
using System.Data.SqlClient;
using System.Configuration;

namespace SysUt14Gr03.Tests
{
    [TestClass]
    public class sendPassword
    {
        private string password = "blahimmel";
        private string newPassword = CreatePassword(10);
        private MailMessage msg;

        [TestMethod]
        public void hei()
        {
            string email = string.Empty;

            msg = new MailMessage();
            SmtpClient smtp = new SmtpClient();
            email = "eivindjs@olebrumm.nnth.org";
            //bruker gruppe eposten som avsender
            msg.From = new MailAddress("sysut14gr03@gmail.com");
            msg.To.Add(email);

            msg.Subject = "tilsendt nytt passord";

            msg.Body = "Hei Frederik !\n" + "Her har du et nytt passord for din bruker: " + newPassword + "\nVi vil anbefale deg å å skifte passord når du får logget deg inn til noe som er mer personlig";
            msg.IsBodyHtml = true;
            smtp.Credentials = new NetworkCredential("sysut14gr03@gmail.com", password);
            smtp.Port = 587;
            smtp.Host = "smtp.gmail.com";
            smtp.EnableSsl = true;
            smtp.Send(msg);

            email = string.Empty;
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
    }
}
