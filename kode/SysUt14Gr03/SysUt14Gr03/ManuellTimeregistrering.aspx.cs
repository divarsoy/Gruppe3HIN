using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SysUt14Gr03.Models;
using SysUt14Gr03.Classes;

namespace SysUt14Gr03
{
    public partial class ManuellTimeregistrering : System.Web.UI.Page
    {
        private int pauseTeller;
        private int bruker_id;
        private int oppgave_id;
        private Oppgave oppgave;
        private TimeSpan bruktTid;

        protected void Page_Load(object sender, EventArgs e)
        {
            SessionSjekk.sjekkForRettighetPaaInnloggetBruker(Konstanter.rettighet.Prosjektleder);
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
                        ddlDag.Items.Add(new ListItem("I dag (" + dato.ToShortDateString() + ")", dato.ToShortDateString()));
                        ddlDag.Items.Add(new ListItem("I går (" + dato.AddDays(-1).ToShortDateString() + ")", dato.AddDays(-1).ToShortDateString()));
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
                    ddlDag.Visible = true;
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

        protected void btnAddPause_Click(object sender, EventArgs e)
        {
            lblTest.Visible = false;
            btnFullfor.Visible = false;

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
                pnlPauser.Controls.Add(lblPST);
                pnlPauser.Controls.Add(txtPST);
                pnlPauser.Controls.Add(lblPSL);
                pnlPauser.Controls.Add(txtPSL);
                pnlPauser.Controls.Add(new LiteralControl("<br />"));
            }
        }

        protected void btnLagre_Click(object sender, EventArgs e)
        {
            // Lagre timer på oppgave
            DateTime startTid;
            DateTime sluttTid;
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

                        DateTime dato = DateTime.Parse(ddlDag.SelectedValue);

                        //debug
                        lblTest.Text = "Følgende timetall vil bli registrert: " + bruktTid.Hours + " timer og " + bruktTid.Minutes + " minutter.<br />Dato: "
                            + dato.ToShortDateString() + " på oppgave " + oppgave.Tittel + ". Godta registrering?";
                        lblTest.Visible = true;
                        btnFullfor.Visible = true;

                    }
                    else
                    {
                        lblTest.Text = "Pauser kan ikke være utenfor arbeidsøkten";
                    }
                }
                else
                {
                    lblTest.Text = "Sluttid kan ikke være før starttid";
                }
            }
            else
            {
                lblTest.Text = "Vennligst oppgi start- og sluttid";
            }


        }

        protected void btnFullfor_Click(object sender, EventArgs e)
        {
            DateTime dato = DateTime.Parse(ddlDag.SelectedValue);
            bruktTid = (TimeSpan)ViewState["bruktTid"];

            using (var context = new Context())
            {
                oppgave = context.Oppgaver.Where(o => o.Oppgave_id == oppgave_id).FirstOrDefault();
                Bruker bruker = context.Brukere.Where(b => b.Bruker_id == bruker_id).FirstOrDefault();
                var time = new Time
                {
                    Tid = bruktTid,
                    Opprettet = dato,
                    Aktiv = true,
                    Bruker = bruker,
                    Oppgave = oppgave
                };

                context.Timer.Add(time);
                context.SaveChanges();
            }
        }

    }
}