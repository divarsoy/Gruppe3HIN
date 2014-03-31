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
   
        private List<Bruker> brukerProsjekt;
        private Table table;
        private int brukerid;

        protected void Page_Load(object sender, EventArgs e)
        {
            /* if (Session["loggedIn"] == null)
             {
                 Response.Redirect("Login.aspx", true);
             }
             else
             {
                 brukerid = Validator.KonverterTilTall(Session["bruker_id"].ToString());
             }
             */
            brukerid = 3;
            if (Validator.SjekkRettighet(brukerid, Konstanter.rettighet.Prosjektleder) && Request.QueryString["prosjekt_id"] != null)
            {

                int prosjekt_id = Validator.KonverterTilTall(Request.QueryString["prosjekt_id"]);
           

                if (!IsPostBack)
                {

                    Prosjekt pro = Queries.GetProsjekt(prosjekt_id);
                    brukerProsjekt = Queries.GetAlleBrukereIEtTeam((int)pro.Team_id);
                    List<Prosjekt> getProsjekt = Queries.GetProsjektLeder(pro.Prosjekt_id);
                    List<Team> getTeam = Queries.GetTeamMedList((int)pro.Team_id);
                    table = Tabeller.HentBrukerTabellIProsjektTeamProsjektLeder(brukerProsjekt, getProsjekt, getTeam);
                    PlaceHolderBrukere.Controls.Add(table);
                    table.CssClass = "table table-hover";


                }

            }

        }
    }
}
    
