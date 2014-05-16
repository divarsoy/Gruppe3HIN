using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SysUt14Gr03.Classes;
using SysUt14Gr03.Models;

/// <summary>
/// Lister ut informasjon som fase leder, start/stopp/opprettet dato. Oppgaver til fasen med informasjon om oppgaven som tidsbruk, status og hvem som har oppgaven.
/// Så liste den også ut totalt tidsbruk på fasen.
/// </summary>

namespace SysUt14Gr03
{
    public partial class visFase : System.Web.UI.Page
    {
        private int fase_id;
        private TimeSpan bruktTid;
        private TimeSpan estimertTid;
        private TimeSpan restTid;

        protected void Page_PreInit(Object sener, EventArgs e)
        {
            string master = SessionSjekk.findMaster();
            this.MasterPageFile = master;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            SessionSjekk.sjekkForBruker_id();

            if (Request.QueryString["fase_id"] != null)
            {
                fase_id = Validator.KonverterTilTall(Request.QueryString["fase_id"].ToString());

                Fase fase = Queries.GetFase(fase_id);
                lblFase.Text = "Fase: " + fase.Navn;
                string navn = Queries.GetBruker(fase.Bruker_id).ToString();
                lblInfo.Visible = true;
                lblInfo.Text += "<br />Faseleder: <a href=\"visBruker?bruker_id=" + fase.Bruker_id + "\">" + navn + "</a>";
                lblInfo.Text += "<br />" + "StartDato: " + String.Format("{0:dd/MM/yyyy}", fase.Start);
                lblInfo.Text += "<br />" + "SluttDato: " + String.Format("{0:dd/MM/yyyy}", fase.Stopp);
                lblInfo.Text += "<br />" + "Opprettet: " + String.Format("{0:dd/MM/yyyy}", fase.Opprettet);

                lblInfo.Text += "<hr />";
                lblInfo.Text += "<br /><h2>Oppgaver</h2>";

                foreach (Oppgave oppg in fase.Oppgaver)
                {
                    lblInfo.Text += "<br />" + "Oppgave-ID: " + oppg.RefOppgaveId;
                    lblInfo.Text += "<br />Oppgave: <a href=\"visOppgave?oppgave_id=" + oppg.Oppgave_id + "\">" + oppg.Tittel + "</a>";
                    lblInfo.Text += "<br />" + "Estimert tid: " + oppg.Estimat;
                    lblInfo.Text += "<br />" + "Brukt tid: " + oppg.BruktTid;
                    lblInfo.Text += "<br />" + "Gjenstående tid: " + oppg.RemainingTime;
                    string status = Queries.GetStatus(oppg.Status_id).Navn;
                    lblInfo.Text += "<br />" + "Status: " + status;
                    Oppgave oppgBrukere = Queries.GetOppgave(oppg.Oppgave_id);
                    lblInfo.Text += "<br />Ansvarlige Brukere";
                    foreach (Bruker bruker in oppgBrukere.Brukere)
                    {
                        lblInfo.Text += "<br /><a href=\"visBruker?bruker_id=" + bruker.Bruker_id + "\">" + bruker.ToString() + "</a>";
                    }
                    lblInfo.Text += "<br />";
                    bruktTid += (TimeSpan)oppg.BruktTid;
                    estimertTid += (TimeSpan)oppg.Estimat;
                    restTid += (TimeSpan)oppg.RemainingTime;
                }
                lblInfo.Text += "<hr />";
                lblInfo.Text += "<br /><h2>Sumerte timer</h2>";
                lblInfo.Text += "<br />" + "Sum brukt tid: " + bruktTid + " timer";
                lblInfo.Text += "<br />" + "Sum estimert tid: " + estimertTid + " timer";
                lblInfo.Text += "<br />" + "Sum resterende tid: " + restTid + " timer";
            }
            else
            {
                Session["flashMelding"] = "Fasen finnes ikke";
                Session["flashStatus"] = Konstanter.notifikasjonsTyper.info.ToString();
            }
        }

    }
}