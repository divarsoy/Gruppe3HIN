using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using SysUt14Gr03.Classes;
using SysUt14Gr03.Models;

namespace SysUt14Gr03
{
    public partial class OversiktOppgaver : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            bool queryStatus = false;
            List<Oppgave> query = null;
            int bruker_id = 3;

            // Sjekker om det er lagt ved et Get parameter "prosjekt_id" og lager en spørring basert på prosjekt_id og bruker_id på innlogget bruker
            if (Request.QueryString["prosjekt_id"] != null)
            {
                int prosjekt_id = Validator.KonverterTilTall(Request.QueryString["prosjekt_id"]);
                if (prosjekt_id >= 1)
                {
                    query = Queries.GetAlleAktiveOppgaverForProsjektOgBruker(prosjekt_id, bruker_id);
                    if (query.Count == 0)
                    {
                        lblTilbakemelding.Text = "Du har valgt et ikke gyldig prosjekt";
                    }
                    else
                    {
                        string prosjektNavn = Queries.getProsjekt(prosjekt_id).Navn;
                        string brukerNavn = Queries.GetBruker(bruker_id).ToString();
                        lblTilbakemelding.Text = string.Format("<h3>Prosjekt: {0}</h3><h3>Bruker: {1}</h3>", prosjektNavn, brukerNavn);
                        queryStatus = true;
                    }
                }

                else
                {
                    lblTilbakemelding.Text = "Du har valgt et ikke gyldig prosjekt";
                }
            }
                // Dersom prosjekt ikke er oppgitt lages en spørring basert på bruker_id til innlogget bruker
            else
            {
                query = Queries.GetAlleAktiveOppgaverForBruker(bruker_id);
                string brukerNavn = Queries.GetBruker(bruker_id).ToString();
                lblTilbakemelding.Text = string.Format("<h3>Bruker: {0}</h3>", brukerNavn);
                queryStatus = true;
            }
            // Lager Tabell for å vise oppgaver
            if (!IsPostBack && queryStatus)
            {
                Table oppgaveTable = Tabeller.HentOppgaveTabell(query);
                oppgaveTable.CssClass = "table";
                PlaceHolderTable.Controls.Add(oppgaveTable);
            }
        }
    }
}