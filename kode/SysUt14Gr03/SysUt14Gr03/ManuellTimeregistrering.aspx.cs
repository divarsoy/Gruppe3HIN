using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SysUt14Gr03.Models;
using SysUt14Gr03.Classes;
using System.Drawing;

namespace SysUt14Gr03
{
    /// <summary>
    /// Dette er klassen som bruker kan registrere timer manuellt, med pauser.
    /// Siden trenger en oppgave_id, og brukeren kan legge til så mange pauser han
    /// vil. Hvis timeregistreringen er utenfor tillatte intervaller må faseleder
    /// godkjenne timen.
    /// </summary>
    public partial class ManuellTimeregistrering : System.Web.UI.Page
    {
        private int pauseTeller; // Antall pauser
        private int bruker_id; // Brukeren
        private int oppgave_id; // Oppgaven
        private Oppgave oppgave; //ditto
        private TimeSpan bruktTid; // Tiden som er brukt
        // Eez good
        private List<DateTime> pauseStartListe = new List<DateTime>();
        private List<DateTime> pauseStoppListe = new List<DateTime>();

        protected void Page_PreInit(Object sener, EventArgs e)
        {
            string master = SessionSjekk.findMaster();
            this.MasterPageFile = master;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            SessionSjekk.sjekkForRettighetPaaInnloggetBruker(Konstanter.rettighet.Utvikler);
            bruker_id = Validator.KonverterTilTall(Session["bruker_id"].ToString());

            if (Request.QueryString["oppgave_id"] != null)
            {

                oppgave_id = Validator.KonverterTilTall(Request.QueryString["oppgave_id"]);
                oppgave = Queries.GetOppgave(oppgave_id);
                if (oppgave != null)
                {
                    pauseTeller = 0;

                    if (!IsPostBack)
                    {

                        DateTime dato = DateTime.Now;
                        txtDato.Text = dato.ToString("yyyy-MM-dd");
                        txtStart.Text = dato.ToShortTimeString();

                    }

                    // Tusen takk til http://forums.asp.net/post/2145517.aspx
                    string eventTarget = (this.Request["__EVENTTARGET"] == null) ? string.Empty : this.Request["__EVENTTARGET"];
                    string eventArgument = (this.Request["__EVENTARGUMENT"] == null) ? string.Empty : this.Request["__EVENTARGUMENT"];

                    if (eventTarget == "UserConfirmationPostBack")
                    {
                        if (eventArgument == "true")
                        {
                            Lagre();
                            // Rydd opp
                            RyddOpp();

                        }

                        // User said NOT to do it...
                        // You're the worst, you forget things quicker
                        // than they're told to you. It goes in one ear,
                        // and out your arse.

                    }

                    lblTittel.Text = "Manuell timeregistrering på oppgave " + oppgave.Tittel;

                    if (ViewState["pauseteller"] != null)
                    {
                        pauseTeller = Validator.KonverterTilTall(ViewState["pauseteller"].ToString());
                    }

                    // Legger til nytt felt for pauser
                    // http://www.codeproject.com/Articles/35360/ViewState-in-Dynamic-Control
                    LeggTilPausefelt(pauseTeller);

                    Label1.Visible = true;
                    Label2.Visible = true;
                    Label3.Visible = true;
                    btnAddPause.Visible = true;
                    btnLagre.Visible = true;
                    txtDato.Visible = true;
                    txtStart.Visible = true;
                    txtSlutt.Visible = true;

                }
                else
                {
                    lblTittel.Text = "Oppgaven finnes ikke";
                }

            }
            else
            {
                lblTittel.Text = "Ingen oppgave valgt";
            }

        }

        /// <summary>
        /// Metode som rydder opp i tekstfelter
        /// </summary>
        private void RyddOpp()
        {
            txtStart.Text = "";
            txtSlutt.Text = "";

            for (int i = 0; i < pauseTeller; i++)
            {
                TextBox txtPST = pnlPauser.FindControl("txtPauseStart" + i) as TextBox;
                TextBox txtPSL = pnlPauser.FindControl("txtPauseSlutt" + i) as TextBox;
                txtPST.Text = "";
                txtPSL.Text = "";
            }

            Session["flashMelding"] = "Timer registrert på " + oppgave.Tittel;
            Session["flashStatus"] = Konstanter.notifikasjonsTyper.success.ToString();
            Response.Redirect("Utvikler/InnsynIEgneRegistrerteTimerSomBruker.aspx", true);
        }

        protected void btnAddPause_Click(object sender, EventArgs e)
        {
            LeggTilPausefelt();
            pauseTeller++;
            ViewState["pauseteller"] = pauseTeller;

        }

        // Legger til et pausefelt
        private void LeggTilPausefelt()
        {
            Label lblPST = new Label();
            lblPST.Text = "Pause start:";
            lblPST.ID = "lblPST" + pauseTeller;
            Label lblPSL = new Label();
            lblPSL.Text = "Pause slutt:";
            lblPSL.ID = "lblPSL" + pauseTeller;
            TextBox txtPST = new TextBox();
            TextBox txtPSL = new TextBox();
            txtPST.TextMode = TextBoxMode.Time;
            txtPSL.TextMode = TextBoxMode.Time;
            txtPST.ID = "txtPauseStart" + pauseTeller;
            txtPSL.ID = "txtPauseSlutt" + pauseTeller;
            Button btnFjernPause = new Button();
            btnFjernPause.Text = "X";
            btnFjernPause.ID = "btnFjernPause" + pauseTeller;
            btnFjernPause.CssClass = "btn btn-danger";
            btnFjernPause.Click += btnFjernPause_Click;
            pnlPauser.Controls.Add(lblPST);
            pnlPauser.Controls.Add(txtPST);
            pnlPauser.Controls.Add(lblPSL);
            pnlPauser.Controls.Add(txtPSL);
            pnlPauser.Controls.Add(btnFjernPause);
            pnlPauser.Controls.Add(new LiteralControl("<br />"));
        }

        // Legger til alle pausefelt
        private void LeggTilPausefelt(int p)
        {
            for (int i = 0; i < p; i++)
            {
                Label lblPST = new Label();
                lblPST.Text = "Pause start:";
                lblPST.ID = "lblPST" + i;
                Label lblPSL = new Label();
                lblPSL.Text = "Pause slutt:";
                lblPSL.ID = "lblPSL" + i;
                TextBox txtPST = new TextBox();
                TextBox txtPSL = new TextBox();
                txtPST.TextMode = TextBoxMode.Time;
                txtPSL.TextMode = TextBoxMode.Time;
                txtPST.ID = "txtPauseStart" + i;
                txtPSL.ID = "txtPauseSlutt" + i;
                Button btnFjernPause = new Button();
                btnFjernPause.Text = "X"; // so hardcore
                btnFjernPause.CssClass = "btn btn-danger";
                btnFjernPause.ID = "btnFjernPause" + i;
                btnFjernPause.Click += btnFjernPause_Click;
                pnlPauser.Controls.Add(lblPST);
                pnlPauser.Controls.Add(txtPST);
                pnlPauser.Controls.Add(lblPSL);
                pnlPauser.Controls.Add(txtPSL);
                pnlPauser.Controls.Add(btnFjernPause);
                pnlPauser.Controls.Add(new LiteralControl("<br />"));
            }
        }

        protected void btnFjernPause_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            string buttonID = button.ID;
            // Får tak i ID'en til pausen som skal fjernes
            int pauseID = Validator.KonverterTilTall(buttonID[buttonID.Length - 1].ToString());

            TextBox txtPST = pnlPauser.FindControl("txtPauseStart" + pauseID) as TextBox;
            TextBox txtPSL = pnlPauser.FindControl("txtPauseSlutt" + pauseID) as TextBox;
            Label lblPST = pnlPauser.FindControl("lblPST" + pauseID) as Label;
            Label lblPSL = pnlPauser.FindControl("lblPSL" + pauseID) as Label;
            txtPST.Text = "";
            txtPSL.Text = "";
            txtPST.Visible = false;
            txtPSL.Visible = false;
            lblPST.Visible = false;
            lblPSL.Visible = false;
            button.Visible = false;
            txtPST.Height = 0;
            txtPSL.Height = 0;
            lblPST.Height = 0;
            lblPSL.Height = 0;
            button.Height = 0;
            
        }

        protected void btnLagre_Click(object sender, EventArgs e)
        {
            bool isConfirmNeeded = false;
            string confirmMessage = string.Empty;

            // All server side execution goes here and set isConfirmNeeded to true,
            // and create the confirmMessage text, if user confirmation is needed.

            DateTime startTid;
            DateTime sluttTid;
            if (txtStart.Text != string.Empty && txtSlutt.Text != string.Empty
                && txtDato.Text != string.Empty)
            {
                DateTime dato = DateTime.Parse(txtDato.Text);
                DateTime.TryParse(txtStart.Text, out startTid);
                DateTime.TryParse(txtSlutt.Text, out sluttTid);

                if (startTid != DateTime.MinValue && sluttTid != DateTime.MinValue)
                {


                    if (DateTime.Compare(startTid, sluttTid) < 0)
                    {
                        TimeSpan pauser = new TimeSpan();
                        bruktTid = sluttTid - startTid;
                        bool innenforOkt = true;
                        

                        for (int i = 0; i < pauseTeller; i++)
                        {
                            TextBox txtPST = pnlPauser.FindControl("txtPauseStart" + i) as TextBox;
                            TextBox txtPSL = pnlPauser.FindControl("txtPauseSlutt" + i) as TextBox;

                            if (txtPSL != null && txtPST != null)
                            {
                                // Samler opp alle pausetimer og -minutter
                                txtPST.TextMode = TextBoxMode.Time;
                                txtPSL.TextMode = TextBoxMode.Time;
                                DateTime pauseStartTid;
                                DateTime pauseSluttTid;
                                DateTime.TryParse(txtPST.Text, out pauseStartTid);
                                DateTime.TryParse(txtPSL.Text, out pauseSluttTid);

                                if (pauseStartTid != DateTime.MinValue && pauseSluttTid != DateTime.MinValue)
                                {
                                    bool startOK = DateTime.Compare(startTid, pauseStartTid) < 0 && DateTime.Compare(sluttTid, pauseStartTid) > 0;
                                    bool sluttOK = DateTime.Compare(startTid, pauseSluttTid) < 0 && DateTime.Compare(sluttTid, pauseSluttTid) > 0;

                                    innenforOkt = startOK && sluttOK;

                                    if (innenforOkt)
                                    {
                                        pauser += pauseSluttTid - pauseStartTid;
                                        Pause pause = new Pause();
                                        pause.IsFerdig = true;
                                        pause.Start = pauseStartTid;
                                        pause.Stopp = pauseSluttTid;
                                        //pauseListe.Add(pause);
                                        pauseStartListe.Add(pauseStartTid);
                                        pauseStoppListe.Add(pauseSluttTid);
                                        
                                    }
                                }

                            }
                        }

                        if (innenforOkt)
                        {
                            bruktTid -= pauser;
                            ViewState["bruktTid"] = bruktTid;
                            ViewState["startTid"] = startTid;
                            ViewState["sluttTid"] = sluttTid;
                            ViewState["pauseStartListe"] = pauseStartListe;
                            ViewState["pauseStoppListe"] = pauseStoppListe;

                            confirmMessage = "Følgende timetall vil bli registrert: " + bruktTid.Hours;
                            confirmMessage += (bruktTid.Hours == 1 ? " time" : " timer") + " og ";
                            confirmMessage += bruktTid.Minutes + (bruktTid.Minutes == 1 ? " minutt" : " minutter") +  ". Dato: "
                                + dato.ToShortDateString() + " på oppgave " + oppgave.Tittel + ". Godta registrering?";

                            ViewState["info"] = confirmMessage;

                            isConfirmNeeded = true;


                        }
                        else
                        {
                            // Må bruke label for tilbakemelding, ellers mister jeg info
                            VisFeilmelding("Pauser kan ikke være utenfor arbeidsøkten");
                            

                        }
                    }
                    else
                    {
                        VisFeilmelding("Sluttid kan ikke være før starttid");

                    }
                }
                else
                {
                    VisFeilmelding("Vennligst oppgi start- og sluttid");
                }

            }
            else
            {
                VisFeilmelding("Vennligst oppgi start-, sluttid og dato");
                
            }

            // Setter opp script
            if (isConfirmNeeded)
            {
                System.Text.StringBuilder javaScript = new System.Text.StringBuilder();

                javaScript.Append("\n<script type=text/javascript>\n");
                javaScript.Append("<!--\n");

                javaScript.Append("var userConfirmation = window.confirm('" + confirmMessage + "');\n");
                javaScript.Append("__doPostBack('UserConfirmationPostBack', userConfirmation);\n");

                javaScript.Append("// -->\n");
                javaScript.Append("</script>\n");

                ClientScript.RegisterStartupScript(GetType(), "confirmScript", javaScript.ToString());
            }

        }

        private void VisFeilmelding(string melding)
        {
            lblFeilmelding.Text = melding;
            lblFeilmelding.Visible = true;
            lblFeilmelding.ForeColor = Color.Red;
        }

        private void Lagre()
        {
            bruktTid = (TimeSpan)ViewState["bruktTid"];
            DateTime startTid = (DateTime)ViewState["startTid"];
            DateTime sluttTid = (DateTime)ViewState["sluttTid"];
            List<Pause> pauseListe = new List<Pause>();

            if (ViewState["pauseStartListe"] != null)
            {
                pauseStartListe = ViewState["pauseStartListe"] as List<DateTime>;
                pauseStoppListe = ViewState["pauseStoppListe"] as List<DateTime>;
            }

            for (int i = 0; i < pauseStartListe.Count; i++)
            {
                Pause p = new Pause();
                p.Start = pauseStartListe[i];
                p.Stopp = pauseStoppListe[i];
                p.Oppgave_id = oppgave_id;
                p.IsFerdig = true;
                pauseListe.Add(p);
            }
            
            DateTime dato = DateTime.Parse(txtDato.Text);
            bool godkjent = true;
            if (dato > DateTime.Now || dato < DateTime.Now.AddDays(-1))
            { // Sjekker om han er utenfor tillatt intervall {
                int prosjekt_id = Validator.KonverterTilTall(Session["prosjekt_id"].ToString());
                godkjent = false;

            }

            using (var context = new Context())
            {
                oppgave = context.Oppgaver.Where(o => o.Oppgave_id == oppgave_id).FirstOrDefault();
                Bruker bruker = context.Brukere.Where(b => b.Bruker_id == bruker_id).FirstOrDefault();
                var time = new Time
                {
                    Tid = bruktTid,
                    Opprettet = dato,
                    Manuell = true,
                    Aktiv = true,
                    IsFerdig = godkjent,
                    Pause = pauseListe,
                    Bruker = bruker,
                    Oppgave = oppgave,
                    Start = startTid,
                    Stopp = sluttTid
                };

                // Hvis timen ikke er godkjent oppdateres ikke brukt tid på oppgaven
                if (godkjent)
                {
                    oppgave.BruktTid += bruktTid;
                    if (oppgave.RemainingTime >= bruktTid)
                        oppgave.RemainingTime -= bruktTid;
                    else
                        oppgave.RemainingTime = new TimeSpan(0);
                }
                

                oppgave.Status = context.Statuser.Where(s => s.Status_id == 2).FirstOrDefault(); // Setter status til under arbeid
                context.Timer.Add(time);
                context.SaveChanges();
            }
        }
    }
}