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
using System.Drawing;

namespace SysUt14Gr03
{
    public partial class RegistreringAvBrukere : System.Web.UI.Page
    {
        private string etternavn;
        private string fornavn;
        private string epost;
//        private bool emailUnq = true;
        private string password = "blahimmel";
        private MailMessage msg;
    
        private string subject;
        //private Classes.sendEmail sendMsg
        private string ActivationUrl;
        private string email;
        private List<Rettighet> rettighetListe = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            SessionSjekk.sjekkForRettighetPaaInnloggetBruker(Konstanter.rettighet.Prosjektleder);
            if (!IsPostBack)
            {
                rettighetListe = Queries.GetNoenRettigheter();
                for (int i = 0; i < rettighetListe.Count; i++)
                {
                    Rettighet rettighet = rettighetListe[i];
                    ddlRettighet.Items.Add(new ListItem(rettighet.RettighetNavn, rettighet.Rettighet_id.ToString()));
                }
            }
        }

        protected void opprettBruker(string _etternavn, string _fornavn, string _epost)
        {
            using (var db = new Context())
            {
                List<Rettighet> listRett = new List<Rettighet>();
                int rettighed_id = Convert.ToInt32(ddlRettighet.SelectedValue);

                bool rettighetIsLegal = false;
                rettighetListe = Queries.GetNoenRettigheter();

                // sjekker at brukeren ikke lurer inn en administrator rettighet
                foreach (Rettighet rettighetssjekk in rettighetListe)
                {
                    if (rettighetssjekk.Rettighet_id == rettighed_id)
                        rettighetIsLegal = true;
                }
                if (rettighetIsLegal)
                {
                    var rettighet = db.Rettigheter.Where(rett => rett.Rettighet_id == rettighed_id).FirstOrDefault();
                    listRett.Add(rettighet);
                    var nyBruker = new Bruker
                    {
                        Etternavn = etternavn,
                        Fornavn = fornavn,
                        Epost = epost,
                        Brukernavn = "",
                        IM = "",
                        Token = "",
                        Aktivert = false,
                        Aktiv = false,
                        Opprettet = DateTime.Now,
                        Rettigheter = listRett
                    };
                    db.Brukere.Add(nyBruker);
                    db.SaveChanges();

                    String hendelse = "Bruker med navn " + fornavn + " " + etternavn + "ble opprettet";
                    OppretteLogg.opprettLoggForBruker(hendelse, DateTime.Now, (int)Session["bruker_id"]);
                }
                else
                {
                    Session["flashMelding"] = "Du har valgt en ugyldig rettighet!";
                    Session["flashStatus"] = Konstanter.notifikasjonsTyper.danger;
                }
            }
        } 

        protected void bt_adm_reg_Click(object sender, EventArgs e)
        {
            lblRettighetfeil.Visible = false;
            if (tb_reg_etternavn.Text.Length < 256)
                etternavn = tb_reg_etternavn.Text;
            else
                FeilmeldingEtternavn.Visible = true;
            if (tb_reg_fornavn.Text.Length < 256)
                fornavn = tb_reg_fornavn.Text;
            else
                FeilMeldingFornavn.Visible = true;

            /* //Utgår!
            using (var context = new Context())
            {
                
                var query = context.Brukere.FirstOrDefault(bruker =>bruker.Epost == tb_reg_epost.Text);
                if (query != null)
                {
                    emailUnq = false;
//                    string _epost = query.Epost;
                }                
            }
             * */

            if (ddlRettighet.SelectedValue == "0")
            {
                lblRettighetfeil.Visible = true;
                lblRettighetfeil.ForeColor = Color.Red;
                lblRettighetfeil.Text = "Du må velge en rettighet";
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

            if (tb_reg_epost.Text.Length < 256) {
                epost = tb_reg_epost.Text;
                opprettBruker(etternavn, fornavn, epost);
                //Oppretter logg i database STH PGGS
                string hendelse = "Bruker " + fornavn + " " + etternavn + " har blitt opprettet av Prosjektleder";
                int idBruker = Queries.GetBrukerVedEpost(epost).Bruker_id;
                OppretteLogg.opprettLoggForBruker(hendelse, DateTime.Now, (int)Session["bruker_id"]);
                OppretteLogg.opprettLoggForBruker(hendelse, DateTime.Now, idBruker);
                //AktiverKonto.SetBrukerFelter(fornavn, etternavn, epost);
                EpostFullforReg();          
            }    
            else
                FeilMeldingEpost.Visible = true;
        }

        public void EpostFullforReg()
        {
            try
            {
                Guid token = Guid.NewGuid();
                //   msg = new MailMessage();
                email = tb_reg_epost.Text;
                subject = "Bekreftelses epost for konto aktivering";

                //Rett link tips?
                ActivationUrl = Server.HtmlEncode("http://malmen.hin.no/SysUt14Gr03/AktiverKonto?Epost=" + email + "&Token=" + token);
                //sendEmail.sendEpost(email, msg, subject, ActivationUrl, null, null);
                msg = new MailMessage();
                SmtpClient smtp = new SmtpClient();

                //bruker gruppe eposten som avsender
                msg.From = new MailAddress("sysut14gr03@gmail.com");
                msg.To.Add(epost);

                msg.Subject = subject;

                msg.Body = "Hei " + tb_reg_fornavn.Text.Trim() + "!\n" + "Takk for at du registrerte deg hos oss\n" + " <a href='" + ActivationUrl + "'>Klikk her for å aktivere</a>  din konto.";
                msg.IsBodyHtml = true;
                smtp.Credentials = new NetworkCredential("sysut14gr03@gmail.com", password);
                smtp.Port = 587;
                smtp.Host = "smtp.gmail.com";
                smtp.EnableSsl = true;
                smtp.Send(msg);

                if(ActivationUrl != null)
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('En link for å aktivere brukerkontoen er sendt til brukereposten');", true);
                tb_reg_epost.Text = string.Empty;
                tb_reg_etternavn.Text = string.Empty;
                tb_reg_fornavn.Text = string.Empty;
                tb_reg_fornavn.Focus();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Error occured : " + ex.Message.ToString() + "');", true);
                return;
            }
   
          
        }

    }
}