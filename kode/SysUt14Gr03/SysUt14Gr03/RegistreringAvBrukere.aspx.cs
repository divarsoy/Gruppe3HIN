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
using System.Text.RegularExpressions;


/// <summary>
/// Klasse for å registrere brukere. Denne siden er for prosjektleder og administrator. Er en sjekk som sjekker om du er 
/// prosjektleder eller administrator. Når det blir registrert en bruker, så
/// blir det sendt ut en link med epost og token til den brukeren. I databasen blir det lagret info som etternavn, fornavn, rettighet og epost 
/// som dukker opp når brukeren skal aktivere kontoen sin.
/// </summary>
namespace SysUt14Gr03
{
    public partial class RegistreringAvBrukere : System.Web.UI.Page
    {
        
        private string etternavn; //etternavn
        private string fornavn; //fornavn
        private string epost; //epost
        private int bruker_id;
        private string password = "blahimmel"; //Passord til avsender
        private MailMessage msg;
   
        private string subject;
        private string ActivationUrl; //Aktiverings link
        private string email;
        private List<Rettighet> rettighetListe = null; //rettighetsliste
        private int rettighet_id; //rettighets id
        Guid token = Guid.NewGuid();


        protected void Page_PreInit(Object sener, EventArgs e)
        {
            string master = SessionSjekk.findMaster();
            this.MasterPageFile = master;
        }

        protected void Page_Load(object sender, EventArgs e)
        {

            SessionSjekk.sjekkForBruker_id();
            bruker_id = Validator.KonverterTilTall(Session["bruker_id"].ToString());
            
            //Sjekker hvilke rettighet brukeren har, og henter opp riktig rettighetliste til dropdownmenyen.
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



            if (!IsPostBack)
            {
                for (int i = 0; i < rettighetListe.Count; i++)
                {
                    Rettighet rettighet = rettighetListe[i];
                    ddlRettighet.Items.Add(new ListItem(rettighet.RettighetNavn, rettighet.Rettighet_id.ToString()));
                }

                if (Session["tb_etternavn"] != null)
                {
                    if (tb_reg_etternavn.Text != Session["tb_etternavn"].ToString())
                        tb_reg_etternavn.Text = Session["tb_etternavn"].ToString();
                }
                if (Session["tb_fornavn"] != null)
                {
                    if (tb_reg_fornavn.Text != Session["tb_fornavn"].ToString())
                        tb_reg_fornavn.Text = Session["tb_fornavn"].ToString();
                }
                if (Session["tb_epost"] != null)
                {
                    if (tb_reg_epost.Text != Session["tb_epost"].ToString())
                        tb_reg_epost.Text = Session["tb_epost"].ToString();
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
                    Token = token.ToString(),
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

                //Sletter lagrede sessionsobjekter for valgene på siden.
                slettSession();

                Session["flashMelding"] = "Ny bruker har blitt registrert";
                Session["flashStatus"] = Konstanter.notifikasjonsTyper.success;

                // Hvis brukeren er administrator så redirect til oversikt over brukere
                if (Validator.SjekkRettighet(bruker_id, Konstanter.rettighet.Administrator))
                {
                    Response.Redirect("~/Administrator/EndreBrukerinformasjonSomAdministrator");
                }
                //Hvis ikke, så tøm feltene og forbli på siden.
                else
                {
                    tb_reg_epost.Text = "";
                    tb_reg_etternavn.Text = "";
                    tb_reg_fornavn.Text = "";
                    Response.Redirect(Request.RawUrl);
                }
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
            lagreTekstbokser();

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

            String EpostRegex = @"\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*";
            if(!Regex.IsMatch(tb_reg_epost.Text, EpostRegex))
            {
                Session["flashMelding"] += "Du må skrive inn en gyldig epostadresse<br />";
            }
            
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
                Response.Redirect(Request.RawUrl);
            }
        }

        public void EpostFullforReg()
        {
            try
            {
                email = tb_reg_epost.Text;
                subject = "Bekreftelses epost for konto aktivering";

                
                ActivationUrl = Server.HtmlEncode("http://malmen.hin.no/SysUt14Gr03/AktiverKonto?Epost=" + email + "&Token=" + token);
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