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
    public partial class AdministrasjonAvTeamBrukere : System.Web.UI.Page
    {
        static int teamId = -1;
        private List<Bruker> team_brukerListe;
        private List<Bruker> brukerListe;

        public static void SetValgtTeam(int value)
        { 
           teamId = value;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            team_brukerListe = Queries.GetAlleBrukerePaaTeam(teamId);
            brukerListe = Queries.GetAlleAktiveBrukere();


            if (cbl_brukere.Items.Count == 0 && teamId != -1)
            {
                for (int i = 0; i < brukerListe.Count(); i++)
                {
                    Bruker bruker = brukerListe[i];
                    cbl_brukere.Items.Add(bruker.Etternavn + ", " + bruker.Fornavn);
                }
            }

            if (cbl_TeamBrukere.Items.Count == 0 && teamId != -1)
            {
                for (int i = 0; i < team_brukerListe.Count(); i++)
                {
                    Bruker bruker = team_brukerListe[i];
                    cbl_TeamBrukere.Items.Add(bruker.Etternavn + ", " + bruker.Fornavn);
                }
            }

            if (teamId == -1)
            {
                bt_leggeTilBruker.Visible = false;
                bt_fjerneBruker.Visible = false;

            }
            
        }

        protected void bt_fjerneBruker_Click(object sender, EventArgs e)
        {

            for (int i = 0; i < cbl_TeamBrukere.Items.Count; i++)
            {
                if (cbl_TeamBrukere.Items[i].Selected)
                {
                    Queries.UpdateBrukerePaaTeam(Queries.GetTeamById(teamId), team_brukerListe[i], 2);
                }
            }
            Response.Redirect(Request.RawUrl);  
        } 

        protected void bt_leggeTilBruker_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < cbl_TeamBrukere.Items.Count; i++)
            {
                if (cbl_TeamBrukere.Items[i].Selected)
                {
                    Queries.UpdateBrukerePaaTeam(Queries.GetTeamById(teamId), team_brukerListe[i], 1);
                }
            }
            Response.Redirect(Request.RawUrl);
        }

        protected void cbl_TeamBrukere_SelectedIndexChanged(object sender, EventArgs e)
        {
            bt_fjerneBruker.Visible = false;
        }



    }
}