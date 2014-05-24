using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SysUt14Gr03.Classes;
using SysUt14Gr03.Models;

namespace SysUt14Gr03
{
    /// <summary>
    /// Sjekker at man er prosjektleder også henter man ut alle aktive team også henter man ut alle brukerene
    /// per team før man setter alle i en tabell
    /// </summary>

    public partial class OversiktOverTeam : System.Web.UI.Page
    {
        private Table brukerTabell;

        protected void Page_PreInit(Object sener, EventArgs e)
        {
            string master = SessionSjekk.findMaster();
            this.MasterPageFile = master;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            SessionSjekk.sjekkForRettighetPaaInnloggetBruker(Konstanter.rettighet.Prosjektleder);

                if (!IsPostBack)
                {
                    List<Team> teamene = Queries.GetAlleAktiveTeam();

                    foreach (Team team in teamene)
                    {
                        List<Bruker> query = Queries.GetAlleBrukerePaaTeam(team.Team_id);

                        foreach (Bruker bruker in team.Brukere)
                        {
                            brukerTabell = Tabeller.HentBrukerTabellForTeam(query, team);
                        }
                        Label label = new Label();
                        label.Text = "<h3>" + team.Navn + "</h3>";
                        label.Font.Bold = true;
                        PlaceHolderTable.Controls.Add(label);
                        if (brukerTabell != null)
                        {
                            PlaceHolderTable.Controls.Add(brukerTabell);
                        }
                        else
                        {
                            label.Text = "<p>Ingen team er opprettet</p>";
                        }
                    }
            }
        }
    }
}