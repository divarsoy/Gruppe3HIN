using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Net.Mail;
using System.Web.UI;
using System.Web.UI.WebControls;
using SysUt14Gr03.Classes;
using SysUt14Gr03.Models;

namespace SysUt14Gr03
{
    public partial class RegistreringAvBrukere : System.Web.UI.Page
    {
        private string etternavn;
        private string fornavn;
        private string epost;
        private bool emailUnq = true;

        private string msg;
        private string subject;
        private Classes.sendEmail sendMsg;
        private string ActivationUrl;
        private string email;

        protected void Page_Load(object sender, EventArgs e)
        {
   
        }

        protected void opprettBruker(string _etternavn, string _fornavn, string _epost)
        {
            using (var db = new Context())
            {
                var nyBruker = new Bruker { Etternavn = etternavn, Fornavn = fornavn, Epost = epost };
                db.Brukere.Add(nyBruker);
                db.SaveChanges();
            }
        }

        protected void bt_adm_reg_Click(object sender, EventArgs e)
        {
            if (tb_reg_etternavn.Text.Length < 256)
                etternavn = tb_reg_etternavn.Text;
            else
                FeilmeldingEtternavn.Visible = true;
            if (tb_reg_fornavn.Text.Length < 256)
                fornavn = tb_reg_fornavn.Text;
            else
                FeilMeldingFornavn.Visible = true;

            using (var context = new Context())
            {
                
                var query = context.Brukere.FirstOrDefault(bruker =>bruker.Epost == tb_reg_epost.Text);
                if (query != null)
                {
                    emailUnq = false;
//                    string _epost = query.Epost;
                }
                    
               /* string nyEpost = tb_reg_epost.Text;
                if(_epost == tb_reg_epost.Text)
                {
                    emailUnq = false;
                } */
            }
                
          /*  for (int i = 0; i < Queries.GetAlleAktiveBrukere().Count; i++)
            {
                using (var context = new Context())
                {
                    Bruker _bruker = context.Brukere.Find(i);
                    Epost _epost = _bruker.Epost;

                    if (_epost == tb_reg_epost.Text)
                        emailUnq = false;
                }
                
            } */

            if (emailUnq && tb_reg_epost.Text.Length < 256) {
                epost = tb_reg_epost.Text;
                EpostFullforReg();
            }    
            else
                FeilMeldingEpost.Visible = true;
        }

        public void EpostFullforReg()
        {
            Guid token = Guid.NewGuid();
         //   msg = new MailMessage();
            email = "lillesith@gmail.com";
            subject = "Bekreftelses epost for konto aktivering";
            //har begynt å lage en aktiverkonto side 
            ActivationUrl = Server.HtmlEncode("http://localhost:60154/AktiverKonto.aspx?Epost=" + email + "&Token=" + token);
            msg = "Hei " + tb_reg_fornavn.Text.Trim() + "!\n" + "Takk for at du registrerte deg hos oss\n" + " <a href='" + ActivationUrl + "'>Klikk her for å aktivere</a>  din konto.";
            sendEmail.sendEpost(email, msg, subject, ActivationUrl, null, null);
        }

    }
}