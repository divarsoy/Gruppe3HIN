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
    public partial class AdministrasjonAvGruppe : System.Web.UI.Page
    {
        private List<Gruppe> gruppeListe;

        protected void Page_Load(object sender, EventArgs e)
        {
            gruppeListe = Queries.GetAlleAktiveGrupper();
            for (int i = 0; i < gruppeListe.Count; i++)
            {
                lsbGrupper.Items.Add(gruppeListe[i].Navn);
            }
        }

        
        protected void lsbGrupper_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Gjør noe med valgt gruppe
            int index = lsbGrupper.SelectedIndex;
            //gruppeListe = Queries.GetAlleAktiveGrupper();
            List<Team> teamList = gruppeListe[index].Teams.ToList<Team>();
            for (int i = 0; i < teamList.Count; i++)
            {
                lsbTeam.Items.Add(teamList[i].Navn);
            }
            lsbTeam.Visible = true;
        }
         

        
        protected void btnVisTeam_Click(object sender, EventArgs e)
        {
            int index = lsbGrupper.SelectedIndex;
            
            List<Team> teamList = gruppeListe[index].Teams.ToList<Team>();
            for (int i = 0; i < teamList.Count; i++)
            {
                lsbTeam.Items.Add(teamList[i].Navn);
            }
        }
         
    }
}