using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SysUt14Gr03.Models;
using SysUt14Gr03.Classes;
using System.Drawing;

namespace SysUt14Gr03.Prosjektleder
{
    /// <summary>
    /// Dette er klassen som brukes av faseleder/prosjektleder for å korrigere
    /// timer som er registrert manuelt utenfor tillatte tidspunkter.
    /// Den henter time-objektet fra databasen og viser alle tilhørende
    /// pauser, slik at disse kan endres.
    /// </summary>
	public partial class KorrigerRegistrerteTimer : System.Web.UI.Page
	{
        private int pauseTeller; // Holder rede på antall pausefelt
        private int time_id; // ID'en til timen som skal endres
        private TimeSpan bruktTid; // Ny brukt tid etter korrigering
        private TimeSpan bruktTidOrig; // Brukt tid før korrigering
        private Time time; // Timen som skal endres
        private List<Pause> pauseListe; // Listen med pauser
        private List<DateTime> pauseStartListe = new List<DateTime>(); // Liste over pausestarttidspunkter
        private List<DateTime> pauseStoppListe = new List<DateTime>(); // Liste over pausesluttidspunkter

        protected void Page_PreInit(Object sener, EventArgs e)
        {
            string master = SessionSjekk.findMaster();
            this.MasterPageFile = master; // Finner riktig masterfil
        }

        protected void Page_Load(object sender, EventArgs e)
        {

            SessionSjekk.sjekkForBruker_id();

            if (SessionSjekk.IsFaseleder()) {

                if (Request.QueryString["time_id"] != null)
                {

                    time_id = Validator.KonverterTilTall(Request.QueryString["time_id"]);
                    time = Queries.GetTimer(time_id);
                    if (time != null)
                    {
                        pauseTeller = time.Pause.Count;
                        pauseListe = time.Pause;

                        if (!IsPostBack)
                        {

                            // Setter verdier i tekstfeltene
                            txtDato.Text = ((DateTime)time.Opprettet).ToString("yyyy-MM-dd");
                            txtStart.Text = ((DateTime)time.Start).ToShortTimeString();
                            txtSlutt.Text = ((DateTime)time.Stopp).ToShortTimeString();

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
                                Session["flashMelding"] = "Timer korrigert for " + time.Oppgave.Tittel;
                                Session["flashStatus"] = Konstanter.notifikasjonsTyper.success.ToString();
                                Response.Redirect("../GodkjennTimer.aspx", true);

                            }

                            // User said NOT to do it...
                            // You're the worst, you forget things quicker
                            // than they're told to you. It goes in one ear,
                            // and out your arse.

                        }

                        lblTittel.Text = "Korriger timeregistrering på oppgave " + time.Oppgave.Tittel;

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
                        btnGodkjenn.Visible = true;
                        txtDato.Visible = true;
                        txtStart.Visible = true;
                        txtSlutt.Visible = true;

                    }
                    else
                    {
                        lblTittel.Text = "Valgt timeregistrering ble ikke funnet";
                    }

                }
                else
                {
                    lblTittel.Text = "Ingen timeregistrering valgt";
                }
            }
            else
            {
                SessionSjekk.sjekkForRettighetPaaInnloggetBruker(Konstanter.rettighet.Prosjektleder);
            }

        }


        private void RyddOpp()
        {
            txtStart.Text = "";
            txtSlutt.Text = "";


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
                if (i < pauseListe.Count)
                {
                    txtPST.Text = pauseListe[i].Start.ToShortTimeString();
                    txtPSL.Text = ((DateTime)pauseListe[i].Stopp).ToShortTimeString();
                }
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
            // ViewState["pauseteller"] = pauseTeller;

        }

        protected void btnLagre_Click(object sender, EventArgs e)
        {
            CheckInput();
            ViewState["godkjent"] = false;

        }

        private void VisFeilmelding(string melding)
        {
            lblFeilmelding.Text = melding;
            lblFeilmelding.Visible = true;
            lblFeilmelding.ForeColor = Color.Red;
        }

        private void CheckInput()
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
                                else
                                {
                                    pauseStartListe.Add(new DateTime());
                                    pauseStoppListe.Add(new DateTime());
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
                            confirmMessage += bruktTid.Minutes + (bruktTid.Minutes == 1 ? " minutt" : " minutter") + ". Dato: "
                                + dato.ToShortDateString() + " på oppgave " + time.Oppgave.Tittel + ". Godta registrering?";

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

            // Setter opp scriptet som skal vises
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

        private void Lagre()
        {
            bruktTid = (TimeSpan)ViewState["bruktTid"];
            DateTime startTid = (DateTime)ViewState["startTid"];
            DateTime sluttTid = (DateTime)ViewState["sluttTid"];
            DateTime dato = DateTime.Parse(txtDato.Text);
            if (ViewState["pauseStartListe"] != null)
            {
                pauseStartListe = ViewState["pauseStartListe"] as List<DateTime>;
                pauseStoppListe = ViewState["pauseStoppListe"] as List<DateTime>;
            }

            for (int i = 0; i < pauseStartListe.Count; i++)
            {
                if (i < pauseListe.Count)
                {
                    pauseListe[i].Start = pauseStartListe[i];
                    pauseListe[i].Stopp = pauseStoppListe[i];
                }
                else
                {
                    Pause p = new Pause();
                    p.Start = pauseStartListe[i];
                    p.Stopp = pauseStoppListe[i];
                    p.Oppgave_id = time.Oppgave_id;
                    p.IsFerdig = true;
                    pauseListe.Add(p);
                }
                
            }

            
            using (var context = new Context())
            {
                bool godkjent = Convert.ToBoolean(ViewState["godkjent"].ToString());
                Time time = context.Timer.Where(t => t.Time_id == time_id).FirstOrDefault();
                bruktTidOrig = time.Tid;
                Oppgave oppgave = context.Oppgaver.Where(o => o.Oppgave_id == time.Oppgave_id).FirstOrDefault();

                // http://www.codeproject.com/Tips/61339/Replace-an-Entity-in-Entity-Framework-Context

                time.Opprettet = dato;
                time.Tid = bruktTid;
                time.Start = startTid;
                time.Stopp = sluttTid;
                foreach (Pause p in pauseListe)
                {
                    Pause pausex = context.Pauser.Where(c => c.Pause_id == p.Pause_id).FirstOrDefault();
                    if (pausex != null)
                    {
                        if (p.Start != DateTime.MinValue)
                        {
                            pausex.Start = p.Start;
                            pausex.Stopp = p.Stopp; // Oppdaterer eksisterende pauser
                        }
                        else
                        {
                            context.Pauser.Remove(pausex); // Sletter pausen hvis den er tom
                        }
                        
                    }
                    if (p.Pause_id == 0)
                    {
                        time.Pause.Add(p);
                    }
                }

                time.IsFerdig = godkjent;

                oppgave.BruktTid += bruktTid;
                if (oppgave.RemainingTime >= bruktTid)
                    oppgave.RemainingTime -= bruktTid;
                else
                    oppgave.RemainingTime = new TimeSpan(0);

                context.SaveChanges();
            }

        }

        protected void btnGodkjenn_Click(object sender, EventArgs e)
        {
            ViewState["godkjent"] = true;
            CheckInput();
        }

	}

}