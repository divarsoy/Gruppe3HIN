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
        private Bruker bruker;
        private bool emailUnq = true;

        private MailMessage msg;
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
                
            for (int i = 0; i < Queries.GetAlleAktiveBrukere().Count; i++)
            {
                Bruker bruker = Queries.GetBruker(i);
                if (bruker.Epost == tb_reg_epost.Text)
                    emailUnq = false;
            }

            if (emailUnq && tb_reg_epost.Text.Length < 256)
                epost = tb_reg_epost.Text;
            else
                FeilMeldingEpost.Visible = true;
        }

        public void EpostFullforReg()
        {
            Guid token = Guid.NewGuid();
            email = tb_reg_epost.Text.Trim();
            msg.Subject = "Bekreftelses epost for konto aktivering";
            //har begynt å lage en aktiverkonto side 
            ActivationUrl = Server.HtmlEncode("http://localhost:60154/AktiverKonto.aspx?Epost=" + email + "&Token=" + token);
            msg.Body = "Hei " + tb_reg_fornavn.Text.Trim() + "!\n" + "Takk for at du registrerte deg hos oss\n" + " <a href='" + ActivationUrl + "'>Klikk her for å aktivere</a>  din konto.";

            sendMsg.sendEpost(email, msg.Body, msg.Subject, ActivationUrl, null, null);
        }

    }
}