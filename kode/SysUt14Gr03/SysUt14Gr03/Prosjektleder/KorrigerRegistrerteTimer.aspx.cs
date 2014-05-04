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
	public partial class KorrigerRegistrerteTimer : System.Web.UI.Page
	{
        private int pauseTeller;
        private int time_id;
        // private Oppgave oppgave;
        private TimeSpan bruktTid;
        private Time time;
        private List<Pause> pauseListe;

        protected void Page_PreInit(Object sener, EventArgs e)
        {
            string master = SessionSjekk.findMaster();
            this.MasterPageFile = master;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            // SessionSjekk.sjekkForRettighetPaaInnloggetBruker(Konstanter.rettighet.Prosjektleder);
            // bruker_id = Validator.KonverterTilTall(Session["bruker_id"].ToString());
            // bruker_id = 2;

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

                            // skal byttes ut med kalender
                            DateTime dato = DateTime.Now;
                            txtDato.Text = dato.ToShortDateString();
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
                                Session["flashMelding"] = "Timer registrert på " + time.Oppgave.Tittel;
                                Session["flashStatus"] = Konstanter.notifikasjonsTyper.success.ToString();

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
            else
            {
                SessionSjekk.sjekkForRettighetPaaInnloggetBruker(Konstanter.rettighet.Prosjektleder);
            }

        }


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
        }

        protected void btnAddPause_Click(object sender, EventArgs e)
        {
            LeggTilPausefelt();
            pauseTeller++;
            ViewState["pauseteller"] = pauseTeller;

        }

        // Note to self: Husk å rydde opp i dette...
        private void LeggTilPausefelt()
        {
            Label lblPST = new Label();
            lblPST.Text = "Pause start:";
            Label lblPSL = new Label();
            lblPSL.Text = "Pause slutt:";
            TextBox txtPST = new TextBox();
            TextBox txtPSL = new TextBox();
            txtPST.TextMode = TextBoxMode.Time;
            txtPSL.TextMode = TextBoxMode.Time;
            txtPST.ID = "txtPauseStart" + pauseTeller;
            txtPSL.ID = "txtPauseSlutt" + pauseTeller;
            pnlPauser.Controls.Add(lblPST);
            pnlPauser.Controls.Add(txtPST);
            pnlPauser.Controls.Add(lblPSL);
            pnlPauser.Controls.Add(txtPSL);
            pnlPauser.Controls.Add(new LiteralControl("<br />"));
        }

        private void LeggTilPausefelt(int p)
        {
            for (int i = 0; i < p; i++)
            {
                Label lblPST = new Label();
                lblPST.Text = "Pause start:";
                Label lblPSL = new Label();
                lblPSL.Text = "Pause slutt:";
                TextBox txtPST = new TextBox();
                TextBox txtPSL = new TextBox();
                txtPST.TextMode = TextBoxMode.Time;
                txtPSL.TextMode = TextBoxMode.Time;
                txtPST.ID = "txtPauseStart" + i;
                txtPSL.ID = "txtPauseSlutt" + i;
                txtPST.Text = pauseListe[i].Start.ToShortTimeString();
                txtPSL.Text = ((DateTime)pauseListe[i].Stopp).ToShortTimeString();
                pnlPauser.Controls.Add(lblPST);
                pnlPauser.Controls.Add(txtPST);
                pnlPauser.Controls.Add(lblPSL);
                pnlPauser.Controls.Add(txtPSL);
                pnlPauser.Controls.Add(new LiteralControl("<br />"));
            }
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

                            confirmMessage = "Følgende timetall vil bli registrert: " + bruktTid.Hours;
                            confirmMessage += (bruktTid.Hours == 1 ? " time" : " timer") + " og ";
                            confirmMessage += bruktTid.Minutes + (bruktTid.Minutes == 1 ? " minutt" : " minutter") + ". Dato: "
                                + dato.ToShortDateString() + " på oppgave " + time.Oppgave.Tittel + ". Godta registrering?";

                            ViewState["info"] = confirmMessage;

                            isConfirmNeeded = true;


                        }
                        else
                        {
                            Session["flashMelding"] = "Pauser kan ikke være utenfor arbeidsøkten";
                            Session["flashStatus"] = Konstanter.notifikasjonsTyper.danger.ToString();
                        }
                    }
                    else
                    {
                        Session["flashMelding"] = "Sluttid kan ikke være før starttid";
                        Session["flashStatus"] = Konstanter.notifikasjonsTyper.danger.ToString();
                    }
                }
                else
                {
                    Session["flashMelding"] = "Vennligst oppgi start- og sluttid";
                    Session["flashStatus"] = Konstanter.notifikasjonsTyper.danger.ToString();
                }

            }
            else
            {
                Session["flashMelding"] = "Vennligst oppgi start-, sluttid og dato";
                Session["flashStatus"] = Konstanter.notifikasjonsTyper.danger.ToString();
            }

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

            
            using (var context = new Context())
            {
                Time time = context.Timer.Where(t => t.Time_id == time_id).FirstOrDefault();

                time.Tid = bruktTid;
                time.Start = startTid;
                time.Stopp = sluttTid;

                context.SaveChanges();
            }

        }

	}

}