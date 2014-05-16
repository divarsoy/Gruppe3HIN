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
    /// <summary>
    /// Dette er siden som bruke for å opprette team. Brukeren får presentert en liste
    /// med tilgjengelige utviklere, og velger de som skal med. Brukeren må skrive inn
    /// et navn på teamet for å fortsette
    /// </summary>
    public partial class OpprettTeam : System.Web.UI.Page
    {
        private List<Bruker> brukerListe; // Liste med brukere
        List<Bruker> selectedBrukers; // Brukere som er valgt
        private string teamNavn; // Navnet på teamet
        private List<Prosjekt> prosjekter; // Tilgjengelige prosjekter
        private Prosjekt prosjekt; // Prosjektet de skal med på

        protected void Page_PreInit(Object sener, EventArgs e)
        {
            string master = SessionSjekk.findMaster();
            this.MasterPageFile = master;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            SessionSjekk.sjekkForRettighetPaaInnloggetBruker(Konstanter.rettighet.Prosjektleder);

            brukerListe = Queries.GetAlleAktiveBrukere();
            prosjekter = Queries.GetAlleAktiveProsjekter();

            if (cblBrukere.Items.Count == 0)
            {
                // Legger brukerne til i checkboxlistens
                for (int i = 0; i < brukerListe.Count(); i++)
                {
                    Bruker bruker = brukerListe[i];
                    cblBrukere.Items.Add(bruker.Etternavn + ", " + bruker.Fornavn);
                }
            }
            // Legger prosjektene inn i dropdownliste
            for(int i = 0; i < prosjekter.Count; i++)
                ddlProsjekt.Items.Add(new ListItem(prosjekter[i].Navn, prosjekter[i].Prosjekt_id.ToString()));
        }

        protected void btnOK_Click(object sender, EventArgs e)
        {
            // http://stackoverflow.com/questions/18924147/how-to-get-values-of-selected-items-in-checkboxlist-with-foreach-in-asp-net-c

            // Hvor mange brukere er valgt
            List<Bruker> selectedUsers = new List<Bruker>();
            selectedBrukers = new List<Bruker>();

            // Legger til valgte brukere
            for (int i = 0; i < cblBrukere.Items.Count; i++)
            {
                if (cblBrukere.Items[i].Selected)
                {
                    selectedUsers.Add(brukerListe[i]);
                }
            }

            teamNavn = txtTeamNavn.Text;

            using (var context = new Context())
            {
                // Henter alle valgte brukere
                for (int i = 0; i < selectedUsers.Count; i++)
                {
                    int id = selectedUsers[i].Bruker_id;
                    Bruker bruker = context.Brukere.Where(b => b.Bruker_id == id).First();
                    selectedBrukers.Add(bruker);
                }

                if (teamNavn != string.Empty && selectedUsers.Count > 0)
                {
                    Session["flashMelding"] = "Team opprettet!";
                    Session["flashStatus"] = Konstanter.notifikasjonsTyper.success.ToString();
                    int prosjektID = Convert.ToInt32(ddlProsjekt.SelectedValue);
                    prosjekt = context.Prosjekter.Where(p => p.Prosjekt_id == prosjektID).FirstOrDefault();
                    List<Prosjekt> prosjektList = new List<Prosjekt>();
                    prosjektList.Add(prosjekt);
                    // Legger teamet inn i databasen:
                    var nyttTeam = new Team
                    {
                        Navn = teamNavn,
                        Aktiv = true,
                        Opprettet = DateTime.Now,
                        Brukere = selectedBrukers,
                        Prosjekter = prosjektList
                    };

                    context.Teams.Add(nyttTeam);
                    context.SaveChanges();
                    
                    //Oppretter log for hendelsen
                    OppretteLogg.opprettLoggForBruker("Team " + teamNavn + " ble opprettet.", DateTime.Now, (int)Session["bruker_id"]);
                    Varsel.SendVarsel(selectedBrukers, Varsel.TEAMVARSEL, Varsel.VARSELTITTEL[Varsel.TEAMVARSEL], Varsel.VARSELTEKST[Varsel.TEAMVARSEL] + ": " + teamNavn, 2);

                    Response.Redirect("OverSiktOverAlleTeam.aspx", true);  
                }
                else
                {
                    Session["flashMelding"] = "Ingen brukere valgt eller navn ikke angitt";
                    Session["flashStatus"] = Konstanter.notifikasjonsTyper.danger.ToString();
                    this.failed();
                }
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            // Popup spør om bekreftelse
            // Går tilbake til forrige side
            this.failed();
        }
        private void failed()
        {
            for (int i = 0; i < cblBrukere.Items.Count; i++)
            {
                cblBrukere.Items[i].Selected = false;
            }
            Response.Redirect(Request.RawUrl);
        }
    }
}