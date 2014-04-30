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
        private int teamId = -1;
        private Team team;
        private int prosjekt_id;

        // Laster inn riktig masterfil
        protected void Page_PreInit(Object sener, EventArgs e)
        {
            string master = SessionSjekk.findMaster();
            this.MasterPageFile = master;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            SessionSjekk.sjekkForBruker_id();

            if (Request.QueryString["Team_id"] != null)
            {
                teamId = Validator.KonverterTilTall(Request.QueryString["Team_id"]);
                team = Queries.GetTeamById(teamId);
            }

            else if (Session["prosjekt_id"] != null)
            {
                prosjekt_id = Validator.KonverterTilTall(Session["prosjekt_id"].ToString());
                Prosjekt prosjekt = Queries.GetProsjekt(prosjekt_id);
                lblProsjekt.Text = string.Format("Prosjekt: {0}", prosjekt.Navn); 
                team = Queries.GetTeamByProsjekt(prosjekt_id);
                teamId = team.Team_id;
            }

            if (team != null)
            {
                if (!IsPostBack)
                {
                    List<Bruker> query = Queries.GetAlleBrukerePaaTeam(teamId);
                    Table brukerTabell = Tabeller.HentBrukerTabellForTeam(query, team);
                    PlaceHolderTable.Controls.Add(brukerTabell);
                }
            }
            else
            {
                Label teamIkkeValgt = new Label();
                teamIkkeValgt.Text = "Team er ikke valgt";
                PlaceHolderTable.Controls.Add(teamIkkeValgt);
            }
            
        }
    }
}