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
            bool queryStatus = true;
            List<Oppgave> query = null;
            int bruker_id = 2;

            // Sjekker om det er lagt ved et Get parameter "prosjekt_id" og lager en spørring basert på prosjekt_id og bruker_id på innlogget bruker
            if (Request.QueryString["prosjekt_id"] != null)
            {
                int prosjekt_id = Validator.KonverterTilTall(Request.QueryString["prosjekt_id"]);
                if (prosjekt_id > 0)
                {
                    query = Queries.GetAlleAktiveOppgaverForProsjektOgBruker(prosjekt_id, bruker_id);
                    if (query.Count == 0)
                    {
                        lblTilbakemelding.Text = "Du har valgt et ikke gyldig prosjekt";
                        queryStatus = false;
                    }
                }
            }
                // Dersom prosjekt ikke er oppgitt lages en spørring basert på bruker_id til innlogget bruker
            else
            {
                query = Queries.GetAlleAktiveOppgaverForBruker(bruker_id);
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