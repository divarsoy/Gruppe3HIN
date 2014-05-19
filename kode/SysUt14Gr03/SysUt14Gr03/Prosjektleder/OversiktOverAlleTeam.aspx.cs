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

            List<Team> teamene = Queries.GetAlleAktiveTeam();

            foreach (Team t in teamene)
            {
                List<Bruker> query = Queries.GetAlleBrukerePaaTeam(t.Team_id);

                if (!IsPostBack)
                {
                    
                    foreach (Prosjekt p in t.Prosjekter)
                    {
                        brukerTabell = Tabeller.HentBrukerTabellForTeam(query, t, p.Prosjekt_id);
                        brukerTabell.CssClass = "table";

                    }
                    Label label = new Label();
                    label.Text =  "<h3>" + t.Navn + "</h3>";
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