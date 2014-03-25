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
    public partial class OversiktBrukereSomProsjektleder : System.Web.UI.Page
    {
        private List<Prosjekt> listProsjekt;
        private List<object> bruker;
        private int brukerid;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["loggedIn"] == null)
            {
                Response.Redirect("Login.aspx", true);
            }
            else
            {
                brukerid = Validator.KonverterTilTall(Session["bruker_id"].ToString());

            }

            listProsjekt = Queries.GetAlleAktiveProsjekterForBruker(brukerid);

            for (int i = 0; i < listProsjekt.Count; i++)
            {
                using (var context = new Context())
                {
                Prosjekt prosjekt = listProsjekt[i];
                Team team = context.Teams.Where(t => t.Team_id == prosjekt.Team_id).First();
                bruker = new List<object>();
                bruker.Add(team.Brukere);

                ListBox1.DataSource = bruker;
                ListBox1.DataBind();
                

                }
            }
        }
    }
}