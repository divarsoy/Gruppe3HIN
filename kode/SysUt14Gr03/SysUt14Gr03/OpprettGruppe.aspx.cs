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
    public partial class OpprettGruppe : System.Web.UI.Page
    {
        private List<Team> teamListe;

        protected void Page_Load(object sender, EventArgs e)
        {
            GruppeOK.Visible = false;
            NoTeamsSelected.Visible = false;

            teamListe = Queries.GetAlleAktiveTeam();

            if (cblTeam.Items.Count == 0)
            {
                for (int i = 0; i < teamListe.Count(); i++)
                {
                    Team team = teamListe[i];
                    cblTeam.Items.Add(team.Navn);
                }
            }

        }

        protected void btnOK_Click(object sender, EventArgs e)
        {
            // http://stackoverflow.com/questions/18924147/how-to-get-values-of-selected-items-in-checkboxlist-with-foreach-in-asp-net-c

            // Hvor mange brukere er valgt
            //int numberSelected = 0;
            List<Team> selectedTeams = new List<Team>();

            // Legger til valgte brukere
            for (int i = 0; i < cblTeam.Items.Count; i++)
            {
                if (cblTeam.Items[i].Selected)
                {
                    selectedTeams.Add(teamListe[i]);
                    //numberSelected++;
                }
            }

            string gruppeNavn = txtGruppeNavn.Text;

            if (gruppeNavn != string.Empty && selectedTeams.Count > 0)
            {
                GruppeOK.Visible = true;
                // Legger teamet inn i databasen:
                using (var db = new Context())
                {
                    var nyGruppe = new Gruppe { Navn = gruppeNavn, Aktiv = true, Opprettet = DateTime.Now, Teams = selectedTeams };
                    db.Grupper.Add(nyGruppe);
                    db.SaveChanges();
                }
            }
            else
            {
                NoTeamsSelected.Visible = true;
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            // Popup spør om bekreftelse
            // Går tilbake til forrige side
        }
    }
}