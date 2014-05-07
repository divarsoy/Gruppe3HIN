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
        private int bruker_id;
//        private bool emailUnq = true;
        private string password = "blahimmel";
        private MailMessage msg;
    
        private string subject;
        //private Classes.sendEmail sendMsg
        private string ActivationUrl;
        private string email;
        private List<Rettighet> rettighetListe = null;
        private int rettighet_id;

        protected void Page_PreInit(Object sener, EventArgs e)
        {
            string master = SessionSjekk.findMaster();
            this.MasterPageFile = master;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            SessionSjekk.sjekkForBruker_id();
            bruker_id = Validator.KonverterTilTall(Session["bruker_id"].ToString());
            
            if (Validator.SjekkRettighet(bruker_id, Konstanter.rettighet.Administrator))
            {
                rettighetListe = Queries.GetAlleRettigheter();
            }

            else if (Validator.SjekkRettighet(bruker_id, Konstanter.rettighet.Prosjektleder))
            {
                rettighetListe = Queries.GetNoenRettigheter();
            }
            else
            {
                SessionSjekk.LoggutFeilRettighet();
            }
/*
                if (Session["tb_etternavn"] != null)
                {
                    if (tb_reg_etternavn.Text != string.Empty)
                    {
                        tb_reg_etternavn.Text = Session["tb_etternavn"].ToString();
                    }
                }
                if (Session["tb_fornavn"] != null)
                {
                    tb_reg_fornavn.Text = Session["tb_fornavn"].ToString();
                }
                if (Session["tb_epost"] != null)
                {
                    tb_reg_epost.Text = Session["tb_epost"].ToString();
                }
*/
            if (!IsPostBack)
            {
                for (int i = 0; i < rettighetListe.Count; i++)
                {
                    Rettighet rettighet = rettighetListe[i];
                    ddlRettighet.Items.Add(new ListItem(rettighet.RettighetNavn, rettighet.Rettighet_id.ToString()));
                }
            }          
        }

        protected void opprettBruker(string _etternavn, string _fornavn, string _epost, int _rettighet_id)
        {
            using (var db = new Context())
            {
                var rettighet = db.Rettigheter.Where(rett => rett.Rettighet_id == _rettighet_id).FirstOrDefault();
                List<Rettighet> nyRettighet = new List<Rettighet>();
                nyRettighet.Add(rettighet);

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
                    Rettigheter = nyRettighet
                };
                db.Brukere.Add(nyBruker);
                db.SaveChanges();

                bool boolDefault = true;

                // Slår av epostutsendelse av notifikasjoner for opprettelse av nye administratorer
                if (rettighet.RettighetNavn == Konstanter.rettighet.Administrator.ToString())
                {
                    boolDefault = false;
                }

                var nybrukerPreferanse = new BrukerPreferanse
                {
                    Bruker_id = nyBruker.Bruker_id,
                    EpostTeam = boolDefault,
                    EpostProsjekt = boolDefault,
                    EpostOppgave = boolDefault,
                    EpostKommentar = boolDefault,
                    EpostTidsfrist = boolDefault,
                    Sheperd = true
                };
                db.BrukerPreferanser.Add(nybrukerPreferanse);
                db.SaveChanges();

                //Oppretter logg i database STH PGGS
                string hendelse = "Bruker " + fornavn + " " + etternavn + " har blitt opprettet av Prosjektleder";
                int idBruker = Queries.GetBrukerVedEpost(epost).Bruker_id;
                OppretteLogg.opprettLoggForBruker(hendelse, DateTime.Now, (int)Session["bruker_id"]);
                OppretteLogg.opprettLoggForBruker(hendelse, DateTime.Now, idBruker);
                //AktiverKonto.SetBrukerFelter(fornavn, etternavn, epost);
                //     slettSession();
                EpostFullforReg(); 

                hendelse = "Bruker med navn " + fornavn + " " + etternavn + "ble opprettet";
                OppretteLogg.opprettLoggForBruker(hendelse, DateTime.Now, (int)Session["bruker_id"]);

                Session["flashMelding"] = "Ny bruker har blitt registrert";
                Session["flashStatus"] = Konstanter.notifikasjonsTyper.success;
                Response.Redirect(Request.RawUrl);

            }

        }              

        private void lagreTekstbokser()
        {
            Session["tb_etternavn"] = tb_reg_etternavn.Text;
            Session["tb_fornavn"] = tb_reg_fornavn.Text;
            Session["tb_epost"] = tb_reg_epost.Text;
        }

        private void slettSession()
        {
            Session["tb_etternavn"] = null;
            Session["tb_fornavn"] = null;
            Session["tb_epost"] = null;
        }

        protected void bt_adm_reg_Click(object sender, EventArgs e)
        {
            //lagreTekstbokser();

            Session["flashMelding"] = string.Empty;

            if (ddlRettighet.SelectedValue == "0")
                Session["flashMelding"] += "Du må velge en rettighet<br />";
            if (tb_reg_etternavn.Text == string.Empty)
                Session["flashMelding"] += "Etternavn må fylles ut<br />";
            if (tb_reg_fornavn.Text == string.Empty)
                Session["flashMelding"] += "Fornavn må fylles ut<br />";
            if (tb_reg_epost.Text == string.Empty)
                Session["flashMelding"] += "Epost må fylles ut<br />";

            if (tb_reg_etternavn.Text.Length < 256 && tb_reg_etternavn.Text.Length >= 0)
                etternavn = tb_reg_etternavn.Text;
            else
                Session["flashMelding"] += "Etternavn kan ikke være lenger enn 256 tegn<br />";

            if (tb_reg_fornavn.Text.Length < 256 && tb_reg_fornavn.Text.Length >= 0)
                fornavn = tb_reg_fornavn.Text;
            else
                Session["flashMelding"] += "Fornavn kan ikke være lenger enn 256 tegn<br />";

            if (tb_reg_epost.Text.Length < 256 && tb_reg_epost.Text.Length >= 0) 
                epost = tb_reg_epost.Text;
            else
                Session["flashMelding"] += "Epost kan ikke være lenger enn 256 tegn<br />";

            // sjekker at brukeren ikke lurer inn en administrator rettighet
            if (ddlRettighet.SelectedValue != null && ddlRettighet.SelectedValue != "0")
            {
                rettighet_id = Validator.KonverterTilTall(ddlRettighet.SelectedValue);
                bool rettighetIsLegal = false;

                foreach (Rettighet rettighetssjekk in rettighetListe)
                {
                    if (rettighetssjekk.Rettighet_id == rettighet_id)
                        rettighetIsLegal = true;
                }
                if (!rettighetIsLegal)
                    Session["flashMelding"] += "Du har valgt en ugyldig rettighet!<br />";
            }

            if (Session["flashMelding"].ToString() == string.Empty){
                opprettBruker(etternavn, fornavn, epost, rettighet_id);
         
            }

            else
            {
                Session["flashStatus"] = Konstanter.notifikasjonsTyper.danger;
             //   Response.Redirect(Request.RawUrl);
            }
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
                smtp.UseDefaultCredentials = false;

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