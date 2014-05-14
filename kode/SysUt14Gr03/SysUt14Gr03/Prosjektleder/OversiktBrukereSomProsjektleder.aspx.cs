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

        protected void Page_PreInit(Object sener, EventArgs e)
        {
            string master = SessionSjekk.findMaster();
            this.MasterPageFile = master;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            SessionSjekk.sjekkForRettighetPaaInnloggetBruker(Konstanter.rettighet.Prosjektleder);
            SessionSjekk.sjekkForProsjekt_id();
             
            brukerid = Validator.KonverterTilTall(Session["bruker_id"].ToString());
            int prosjekt_id = Validator.KonverterTilTall(Session["prosjekt_id"].ToString());

            if (!IsPostBack)
            {

                Prosjekt pro = Queries.GetProsjekt(prosjekt_id);
                brukerProsjekt = Queries.GetAlleBrukereEtTeam((int)pro.Team_id);
                List<Prosjekt> getProsjekt = Queries.GetProsjektLeder(pro.Prosjekt_id);
                List<Team> getTeam = Queries.GetTeamMedList((int)pro.Team_id);
                table = Tabeller.HentBrukerTabellIProsjektTeamProsjektLeder(brukerProsjekt, getProsjekt, getTeam);
                PlaceHolderBrukere.Controls.Add(table);
                table.CssClass = "table table-hover";
            }
        }
    }
}
    
