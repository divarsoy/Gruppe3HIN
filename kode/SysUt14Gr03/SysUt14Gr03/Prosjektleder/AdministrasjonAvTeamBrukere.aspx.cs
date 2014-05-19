using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SysUt14Gr03.Classes;
using SysUt14Gr03.Models;

/// <summary>
/// Tilgjengelig for prosjekt leder. Får opp to checkbox lister med brukere. Den til venstre er bruker liste som allerede er i teamet, den lista til høyre 
/// er en liste over alle brukere som eksistere i databasen minus brukere som allerede er i teamet. Så er det muligheter for å legge til og fjerne brukere
/// for teamet og endre navnet til teamet. 
/// </summary>

namespace SysUt14Gr03
{
    public partial class AdministrasjonAvTeamBrukere : System.Web.UI.Page
    {
        static int teamId = -1;
        static List<Bruker> team_brukerListe;
        static List<Bruker> brukerListe;
        static Team tempTeam;

        protected void Page_PreInit(Object sener, EventArgs e)
        {
            string master = SessionSjekk.findMaster();
            this.MasterPageFile = master;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            SessionSjekk.sjekkForRettighetPaaInnloggetBruker(Konstanter.rettighet.Prosjektleder);

            if (!Page.IsPostBack)
            {
                brukerListe = Queries.GetAlleBrukere(); // Henter ut alle brukere (både aktive og ikke aktive, så prosjektleder kan starte et prosjekt umiddelbart)

                if (Request.QueryString["Team_id"] != null)
                {
                    teamId = Validator.KonverterTilTall(Request.QueryString["Team_id"]);
                    team_brukerListe = Queries.GetAlleBrukerePaaTeam(teamId);
                }

                // slå sammen med if over
                if (teamId != -1)
                {
                    tb_TeamNavn.Text = Queries.GetTeamById(teamId).Navn;
                }

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
        }

        protected void bt_endreTeamNavn_Click(object sender, EventArgs e)
        {

            using (var context = new Context())
            {

                Team t = context.Teams.Where(Team => Team.Team_id == teamId).FirstOrDefault();
                //string med navn før endring brukes til å opprette logg
                string navnForEndring = t.Navn;
                t.Navn = tb_TeamNavn.Text;
                context.SaveChanges();
                //Oppretter logg
                string hendelse = "Navn på team " + teamId + " ble endret fra " + navnForEndring + " til " + tb_TeamNavn.Text;
                OppretteLogg.opprettLoggForBruker(hendelse, DateTime.Now, (int)Session["bruker_id"]);
            }
            
        }

        protected void bt_fjerneBruker_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < cbl_TeamBrukere.Items.Count; i++)
            {
                if (cbl_TeamBrukere.Items[i].Selected)
                {
                    tempTeam = Queries.GetTeamById(teamId);
                    Bruker tempBruker = team_brukerListe[i];
                    Queries.UpdateBrukerePaaTeam(tempTeam, tempBruker, 2);

                    //Oppretter logg
                    string hendelse = "Bruker " + tempBruker.Fornavn + " " + tempBruker.Etternavn + " ble fjernet fra team " + tempTeam.Navn;
                    //Oppretter logg for bruker som utfører handling
                    OppretteLogg.opprettLoggForBruker(hendelse, DateTime.Now, (int)Session["bruker_id"]);
                    //Oppretter logg for bruker som blir påvirket av handling
                    OppretteLogg.opprettLoggForBruker(hendelse, DateTime.Now, tempBruker.Bruker_id);
                }
            }
                
                
            Response.Redirect(Request.RawUrl);  
        } 

        protected void bt_leggeTilBruker_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < cbl_brukere.Items.Count; i++)
            {
                if (cbl_brukere.Items[i].Selected)
                {
                    tempTeam = Queries.GetTeamById(teamId);
                    Bruker tempBruker = brukerListe[i];
                    Queries.UpdateBrukerePaaTeam(tempTeam, tempBruker, 1);

                    //Oppretter logg
                    string hendelse = "Bruker " + tempBruker.Fornavn + " " + tempBruker.Etternavn + " ble lagt til i team " + tempTeam.Navn;
                    //Oppretter logg for bruker som utfører handling
                    OppretteLogg.opprettLoggForBruker(hendelse, DateTime.Now, (int)Session["bruker_id"]);
                    //Oppretter logg for bruker som blir påvirket av handling
                    OppretteLogg.opprettLoggForBruker(hendelse, DateTime.Now, tempBruker.Bruker_id);
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