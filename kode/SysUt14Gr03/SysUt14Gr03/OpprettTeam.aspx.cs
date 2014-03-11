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
    public partial class OpprettTeam : System.Web.UI.Page
    {
        private List<Bruker> brukerListe;
        private string teamNavn;
        private sendEmail sendMsg;

        protected void Page_Load(object sender, EventArgs e)
        {
            TeamOK.Visible = false;
            NoUsersSelected.Visible = false;
           
            brukerListe = Queries.GetAlleAktiveBrukere();

            if (cblBrukere.Items.Count == 0)
            {
                for (int i = 0; i < brukerListe.Count(); i++)
                {
                    Bruker bruker = brukerListe[i];
                    cblBrukere.Items.Add(bruker.Etternavn + ", " + bruker.Fornavn);
                }
            }
            
        }

        protected void btnOK_Click(object sender, EventArgs e)
        {
            // http://stackoverflow.com/questions/18924147/how-to-get-values-of-selected-items-in-checkboxlist-with-foreach-in-asp-net-c

            // Hvor mange brukere er valgt
            //int numberSelected = 0;
            List<Bruker> selectedUsers = new List<Bruker>();
            List<Bruker> selectedBrukers = new List<Bruker>();

            // Legger til valgte brukere
            for (int i = 0; i < cblBrukere.Items.Count; i++)
            {
                if (cblBrukere.Items[i].Selected)
                {
                    selectedUsers.Add(brukerListe[i]);
                    //numberSelected++;
                }
            }

            teamNavn = txtTeamNavn.Text;

            using (var context = new Context())
            {

                for (int i = 0; i < selectedUsers.Count; i++)
                {
                    int id = selectedUsers[i].Bruker_id;
                    Bruker bruker = context.Brukere.Where(b => b.Bruker_id == id).First();
                    selectedBrukers.Add(bruker);
                }

                if (teamNavn != string.Empty && selectedUsers.Count > 0)
                {
                    TeamOK.Visible = true;
                    // Legger teamet inn i databasen:
                    
                        var nyttTeam = new Team
                        {
                            Navn = teamNavn,
                            Aktiv = true,
                            Opprettet = DateTime.Now,
                            Brukere = selectedBrukers
                        };

                        context.Teams.Add(nyttTeam);
                        context.SaveChanges();
                        this.sendEpost();
                    
                }
                else
                {
                    NoUsersSelected.Visible = true;
                }
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            // Popup spør om bekreftelse
            // Går tilbake til forrige side
        }

        public void sendEpost()
        {
            string message = "Du har blitt medlem av det nye Teamet: " + teamNavn;
            string subject = "Medlem av nytt team";

            sendMsg = new sendEmail(); //Frederik pls
            sendMsg.sendEpost(null, message, subject, null, brukerListe, null);
        }
    }
}