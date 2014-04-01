using System;
using System.Collections.Generic;
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
        protected void Page_Load(object sender, EventArgs e)
        {



            if (Request.QueryString["prosjekt_id"] != null)
            {
                prosjekt_id = Validator.KonverterTilTall(Request.QueryString["prosjekt_id"]);


                Prosjekt prosjekt = Queries.GetProsjekt(prosjekt_id);
                prosjektNavn.Text = prosjekt.Navn;


                lblInfo.Text += "<br />" + "StartDato: " + String.Format("{0:dd/MM/yyyy}", prosjekt.StartDato);
                lblInfo.Text += "<br />" + "SluttDato: " + String.Format("{0:dd/MM/yyyy}", prosjekt.SluttDato);
                lblInfo.Text += "<br />" + "Opprettet: " + String.Format("{0:dd/MM/yyyy}", prosjekt.Opprettet);
                string navn = Queries.GetBruker(prosjekt.Bruker_id).ToString();
                string teamNavn = Queries.GetTeam((int)prosjekt.Team_id).Navn;
                lblInfo.Text += "<br />Team: <a href=\visTeam?team_id=" + prosjekt.Team_id + "\">" + teamNavn + "</a>";
                lblInfo.Text += "<br />Prosjekleder: <a href=\"visBruker?bruker_id=" + prosjekt.Bruker_id + "\">" + navn + "</a>";
                foreach (Oppgave oppg in prosjekt.Oppgaver)
                {
                    lblInfo.Text += "<br />" + "f" + oppg.Tittel;
                }
            }
        }
    }
}