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
        private int pauseTeller = 0;
        private int bruker_id;
        private int oppgave_id;
        private Oppgave oppgave;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["oppgave_id"] != null)
            {
                //oppgave_id = Validator.KonverterTilTall(Request.QueryString["oppgave_id"]);
                //oppgave = Queries.GetOppgave(oppgave_id);

                if (!IsPostBack)
                {
                    DateTime dato = DateTime.Now;
                    ddlDag.Items.Add(new ListItem("I dag (" + dato.ToShortDateString() + ")", dato.ToShortDateString()));
                    ddlDag.Items.Add(new ListItem("I går (" + dato.AddDays(-1).ToShortDateString() + ")", dato.AddDays(-1).ToShortDateString()));
                }


                if (Session["pauseteller"] != null)
                {
                    pauseTeller = Validator.KonverterTilTall(Session["pauseteller"].ToString());
                }

                // Legger til nytt felt for pauser
                // http://www.codeproject.com/Articles/35360/ViewState-in-Dynamic-Control
                pnlPauser.Controls.Clear();

                for (int i = 0; i < pauseTeller; i++)
                {
                    Label lblPST = new Label();
                    lblPST.Text = "Pause start";
                    Label lblPSL = new Label();
                    lblPSL.Text = "Pause slutt";
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
            else
            {
                lblTittel.Text = "Ingen oppgave valgt";
            }

        }

        protected void btnAddPause_Click(object sender, EventArgs e)
        {

            Session["pauseteller"] = ++pauseTeller;

        }

        protected void btnLagre_Click(object sender, EventArgs e)
        {
            // Lagre timer på oppgave
            DateTime startTid;
            DateTime sluttTid;
            DateTime.TryParse(txtStart.Text, out startTid);
            DateTime.TryParse(txtSlutt.Text, out sluttTid);

            if (DateTime.Compare(startTid, sluttTid) < 0)
            {
                TimeSpan pauser = new TimeSpan();
                TimeSpan bruktTid = sluttTid - startTid;
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

                    DateTime dato = DateTime.Parse(ddlDag.SelectedValue);

                    //debug
                    lblTest.Text = "brukt tid: " + bruktTid.Hours + " timer og " + bruktTid.Minutes + " minutter. dato: "
                        + dato.ToShortDateString();
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

    }
}