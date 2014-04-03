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
    public partial class VisTeam : System.Web.UI.Page
    {
        private int teamId;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["Team_id"] != null)
            {
                teamId = Validator.KonverterTilTall(Request.QueryString["Team_id"]);
                List<Bruker> query = Queries.GetAlleBrukerePaaTeam(teamId);
                Team t = Queries.GetTeamById(teamId);

                if (!IsPostBack)
                {
                    Table brukerTabell = Tabeller.HentBrukerTabellForTeam(query, t);

                    PlaceHolderTable.Controls.Add(brukerTabell);
                }
            }
            else
            {
                Label teamIkkeValgt = new Label();
                teamIkkeValgt.Text = "Team er ikke valgt. Gå tilbake til oversikt over team";
                PlaceHolderTable.Controls.Add(teamIkkeValgt);
            }

            
        }
    }
}