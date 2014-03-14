﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SysUt14Gr03.Classes;
using SysUt14Gr03.Models;

namespace SysUt14Gr03
{
    public partial class AdministrasjonAvTeam : System.Web.UI.Page
    {
        private List<Team> teamListe;

        protected void Page_Load(object sender, EventArgs e)
        {
            teamListe = Queries.GetAlleAktiveTeam();

            if (cbl_team.Items.Count == 0)
            {
                for (int i = 0; i < teamListe.Count(); i++)
                {
                    Team team = teamListe[i];
                    cbl_team.Items.Add(team.Navn);
                }
            }
        }

        protected void bt_endreTeam_Click(object sender, EventArgs e)
        {
            Team valgtTeam = Queries.GetTeamByName(cbl_team.SelectedValue);
            
            AdministrasjonAvTeamBrukere.SetValgtTeam(valgtTeam.Team_id);

            Response.Redirect("http://localhost:60154/AdministrasjonAvTeamBrukere.aspx");
//           Server.Transfer("/AdministrasjonAvTeamBrukere.aspx");
        }

    }
}