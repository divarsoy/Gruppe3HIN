using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SysUt14Gr03.Classes;
using SysUt14Gr03.Models;

namespace SysUt14Gr03
{
    public partial class VisProsjekt : System.Web.UI.Page
    {
        private int prosjekt_id;
        private List<Oppgave> oppgaveProsjekt;
        protected void Page_Load(object sender, EventArgs e)
        {

            SessionSjekk.sjekkForBruker_id();

            if (Request.QueryString["prosjekt_id"] != null)
            {
                //NB!!! Sjekker ikke om bruker er med i prosjektet som listes
                prosjekt_id = Validator.KonverterTilTall(Request.QueryString["prosjekt_id"]);
            }
            else
            {
                SessionSjekk.sjekkForProsjekt_id();
                prosjekt_id = Validator.KonverterTilTall(Session["prosjekt_id"].ToString());
            }
            Prosjekt prosjekt = Queries.GetProsjekt(prosjekt_id);
            prosjektNavn.Text = prosjekt.Navn;
            oppgaveProsjekt = Queries.GetAlleAktiveOppgaverForProsjekt(prosjekt.Prosjekt_id);


            lblInfo.Text += "<br />" + "StartDato: " + String.Format("{0:dd/MM/yyyy}", prosjekt.StartDato);
            lblInfo.Text += "<br />" + "SluttDato: " + String.Format("{0:dd/MM/yyyy}", prosjekt.SluttDato);
            lblInfo.Text += "<br />" + "Opprettet: " + String.Format("{0:dd/MM/yyyy}", prosjekt.Opprettet);
            lblInfo.Text += "<hr />";
            string navn = Queries.GetBruker(prosjekt.Bruker_id).ToString();
            string teamNavn = Queries.GetTeam((int)prosjekt.Team_id).Navn;
            lblInfo.Text += "<br />Team: <a href=\"visTeam?team_id=" + prosjekt.Team_id + "\">" + teamNavn + "</a>";
            lblInfo.Text += "<br />Prosjektleder: <a href=\"visBruker?bruker_id=" + prosjekt.Bruker_id + "\">" + navn + "</a>";

            lblInfo.Text += "<hr />";
            lblInfo.Text += "<br />" + "Oppgaver Knyttet til prosjektet";
            for (int i = 0; i < oppgaveProsjekt.Count; i++)
            {
                Oppgave oppg = oppgaveProsjekt[i];
                lblInfo.Text += "<br /><a href=\"visOppgave?oppgave_id=" + oppg.Oppgave_id + "\">" + oppg.Tittel + "</a>";
            }
            // }
            /* else
             {
                 lblInfo.Text = "Prosjekt finnes ikke";
                 lblInfo.ForeColor = Color.Red;
             } */
        }
    }
}