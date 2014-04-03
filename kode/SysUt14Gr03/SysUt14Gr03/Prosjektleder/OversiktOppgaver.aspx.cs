using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using SysUt14Gr03.Classes;
using SysUt14Gr03.Models;

namespace SysUt14Gr03
{
    public partial class OversiktOppgaver : System.Web.UI.Page
    {
        private List<Oppgave> query = null;
        private int bruker_id;
        private int prosjekt_id = -1;
        private string prosjektNavn;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                SessionSjekk.sjekkForRettighetPaaInnloggetBruker(Konstanter.rettighet.Prosjektleder);
                SessionSjekk.sjekkForProsjekt_id();

                bruker_id = Validator.KonverterTilTall(Session["bruker_id"].ToString());
                prosjekt_id = Validator.KonverterTilTall(Session["prosjekt_id"].ToString());

                Prosjekt prosjekt = Queries.GetProsjekt(prosjekt_id);

                // Sjekk om prosjektleder er prosjektleder for valgt prosjekt
                if (prosjekt.Bruker_id == bruker_id)
                {
                    query = Queries.GetAlleOppgaverForProsjekt(prosjekt_id);

                    if (query.Count > 0)
                    {
                        prosjektNavn = Queries.GetProsjekt(prosjekt_id).Navn;
                        lblTilbakemelding.Text = string.Format("<h3>Prosjekt: {0}</h3>", prosjektNavn);

                        Table oppgaveTable = Tabeller.HentOppgaveTabell(query);
                        oppgaveTable.CssClass = "table";
                        PlaceHolderTable.Controls.Add(oppgaveTable);
                    }
                    else
                    {
                        lblTilbakemelding.Text = string.Format("<h3>Prosjekt: {0}</h3><p>Prosjektet inneholder ingen oppgaver</p>", prosjektNavn);
                    }
                }



            }
        }
    }
}