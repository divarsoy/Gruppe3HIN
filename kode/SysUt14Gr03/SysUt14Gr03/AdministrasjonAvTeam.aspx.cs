﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SysUt14Gr03.Classes;
using SysUt14Gr03.Models;
using SysUt14Gr03.AdministrasjonAvTeamBrukere;


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
            AdministrasjonAvTeamBrukere.SetValgtTeam(teamListe[cbl_team.SelectedIndex - 1]);

        }

    }
}