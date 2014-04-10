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
            //SessionSjekk.sjekkForRettighetPaaInnloggetBruker(Konstanter.rettighet.Utvikler);
            //bruker_id = Validator.KonverterTilTall(Session["bruker_id"].ToString());
            bruker_id = 2;

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

                    // http://www.aspsnippets.com/Articles/Server-Side-Code-Behind-Yes-No-Confirmation-Message-Box-in-ASPNet.aspx
                    string confirmValue = Request.Form["confirm_value"];
                    if (confirmValue == "Yes")
                    {
                        //this.Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('You clicked YES!')", true);
                        Lagre();
                        // Vis flashjævel
                        // TIlbakestill felter
                    }


                    string lag = lagreTime.Value;

                    string info;

                    if (infoField.Value != string.Empty)
                    {
                        info = infoField.Value;
                        //btnLagre.OnClientClick = "return confirm('" + info + "');";
                        String confirm = "<script>document.getElementById(\"lagreTime\").value = confirm('" + info + "')? \"Y\" : \"N\"</script>";
                        // ClientScript.RegisterStartupScript(confirm);
                        //ClientScript.RegisterStartupScript(GetType(), "confirm", confirm);
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

                    // Sjekk faenskapet her oppe
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

        public void VisDialog()
        {
        }

        protected void btnLagre_Click(object sender, EventArgs e)
        {
            // Lagre timer på oppgave
            


        }

        private void Lagre()
        {
            DateTime dato = DateTime.Parse(ddlDag.SelectedValue);
            //bruktTid = (TimeSpan)ViewState["bruktTid"];
            //DateTime startTid = (DateTime)ViewState["startTid"];
            //DateTime sluttTid = (DateTime)ViewState["sluttTid"];

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
                        ViewState["startTid"] = startTid;
                        ViewState["sluttTid"] = sluttTid;

                        //DateTime dato = DateTime.Parse(ddlDag.SelectedValue);

                        //debug
                        string info = "Følgende timetall vil bli registrert: " + bruktTid.Hours + " timer og " + bruktTid.Minutes + " minutter. Dato: "
                            + dato.ToShortDateString() + " på oppgave " + oppgave.Tittel + ". Godta registrering?";
                        //infoField.Value = info;
                        //lblTest.Visible = true;
                        //btnFullfor.Visible = true;
                        //btnLagre.Attributes.Add("onclick", "javascript:return confirm('" + info + "')");
                        //Response.Write("<script type=\"text/javascript\"> function Confirmation(){if(confirm('" + info + "')){return true;}else{return false;}}</script>");


                        //Response.Write("<script>document.getElementById(\"confirm_value\").value = confirm('" + info + "')? \"Yes\" : \"No\"</script>");
                        // Gjør et eller annet
                        // Kall lagre()
                        //Lagre(bruktTid, startTid, sluttTid);


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
                    Bruker = bruker,
                    Oppgave = oppgave,
                    Start = startTid,
                    Stopp = sluttTid
                };

                context.Timer.Add(time);
                context.SaveChanges();
            }
        }

    }
}