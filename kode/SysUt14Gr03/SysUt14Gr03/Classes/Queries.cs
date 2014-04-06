using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using SysUt14Gr03.Models;

namespace SysUt14Gr03.Classes
{
    public class Queries
    {
        static public Bruker GetBruker(int _bruker_id)
        {
            using (var context = new Context())
            {
                var bruker = context.Brukere.Find(_bruker_id);
                return bruker;
            }
        }

        static public Bruker GetBrukerMedRettighet(int _bruker_id, Konstanter.rettighet _rettighet)
        {
            using (var context = new Context())
            {
                string rettighetString = _rettighet.ToString();
                var brukerObjekt = context.Brukere
                            .Include("Rettigheter")
                            .Where(bruker => bruker.Rettigheter.Any(rettighet => rettighet.RettighetNavn == rettighetString))
                            .Where(b => b.Bruker_id == _bruker_id)
                            .FirstOrDefault();
                return brukerObjekt;
            }
        }

        static public Bruker GetBrukerVedEpost(string epost)
        {
            using (var context = new Context())
            {
                return context.Brukere.Where(bruker => bruker.Epost == epost).FirstOrDefault();
            }
        }
        static public List<Bruker> GetProsjektledere(Konstanter.rettighet _rettighet)
        {
            using (var context = new Context())
            {
                string rettighetString = _rettighet.ToString();
                var prosjektLedere = context.Brukere
                            .Include("Rettigheter")
                            .Where(bruker => bruker.Rettigheter.Any(rettighet => rettighet.RettighetNavn == rettighetString))                         
                            .ToList<Bruker>();
                return prosjektLedere;
            }
        }
        static public List<Notifikasjon> GetNotifikasjon(int bruker_id)
        {
            using (var context = new Context())
            {
                List<Notifikasjon> notifikasjonsListe = context.Notifikasjoner
                    .Include("NotifikasjonsType")
                    .Where(notifikasjon => notifikasjon.Bruker_id == bruker_id)
                    .Where(p => p.Vist == false)
                    .ToList();
                return notifikasjonsListe;
            }
        }
        static public NotifikasjonsType GetNotifikasjonsType(int notifikasjonsType_id)
        {
            using (var context = new Context())
            {
                var notifikasjonsType = context.NotifikasjonsType
                    .Find(notifikasjonsType_id);
                    return notifikasjonsType;
            }
        }

        // Henter epostpreferanser til bruker med bruker_id
        static public BrukerPreferanse GetEpostPreferanser(int _bruker_id)
        {
            using (var context = new Context())
            {
                List<BrukerPreferanse> brukPrefs = context.BrukerPreferanser.Where(p => p.Bruker_id == _bruker_id).ToList();
                if (brukPrefs.Count > 0)
                {
                    return brukPrefs[0];
                }
                else
                {
                    return null;
                }
            }
        }

        // Henter brukere som har epost _Epost
        static public List<Bruker> GetBruker(string _Epost)
        {
            using (var context = new Context())
            {
                var IQueryList = context.Brukere.Where(bruker => bruker.Epost == _Epost);
                if (IQueryList.Any())
                {
                    return IQueryList.ToList<Bruker>();
                }
                else
                {
                    return null;
                }
                
            }
        }

        static public Prosjekt GetProsjekt(int prosjekt_id)
        {
            using (var context = new Context())
            {
                Prosjekt prosjekt = context.Prosjekter.Find(prosjekt_id);
                return prosjekt;
            }
        }
        static public List<Prosjekt> GetAlleAktiveProsjekterForProsjektLeder(int bruker_id) 
        {
            using (var context = new Context()) {
                var prosjektListe = context.Prosjekter.Where(prosjekt => prosjekt.Bruker_id == bruker_id).ToList();
                return prosjektListe;
            }
        }

        static public List<Kommentar> GetAlleKommentarerTilOppgave(int oppgave_id)
        {
            using (var context = new Context())
            {
                var kommentarListe = context.Kommentarer.Include("Bruker")
                    .Where(k => k.Oppgave_id == oppgave_id).ToList();
                return kommentarListe;
            }
        }

        static public Status GetStatus(int _status_id)
        {
            using (var context = new Context())
            {
                var result = context.Status.Where(status => status.Status_id == _status_id).FirstOrDefault();
                return result;
            }
        }

        static public Prioritering GetPrioritering(int _prioritering_id)
        {
            using (var context = new Context())
            {
                var result = context.Prioriteringer.Where(p => p.Prioritering_id == _prioritering_id).FirstOrDefault();
                return result;
            }
        }

        static public Oppgave GetOppgave(int oppgave_id)
        {
            using (var context = new Context())
            {
                var oppgave = context.Oppgaver
                                  .Include("Brukere")
                                  .Include("Prioritering")
                                  .Include("Status")
                                  .Include("Prosjekt")
                                  .Where(o => o.Oppgave_id == oppgave_id)
                                  .Where(o => o.Aktiv == true)
                                  .FirstOrDefault<Oppgave>();
                return oppgave;
            }
        }

        static public List<Oppgave> GetOppgaverIOppgaveGruppe(int oppgaveGruppe_id)
        {
            using (var context = new Context())
            {
                var oppgaveListe = context.Oppgaver
                                    .Where(o => o.OppgaveGruppe_id == oppgaveGruppe_id)
                                    .ToList();
                return oppgaveListe;
            }
        }

        static public List<Logg> GetLoggForBruker(int bruker_id)
        {
            using (var context = new Context())
            {
                var loggListe = context.Logger
                                    .Where(l => l.bruker_id == bruker_id)
                                    .ToList();
                return loggListe;
            }
        }
        
        static public List<Prosjekt> GetProsjektLeder(int prosjekt_id)
        {
            using (var context = new Context())
            {
                var prosjektListe = (from prosjekt in context.Prosjekter
                                   where prosjekt.Prosjekt_id == prosjekt_id
                                   select prosjekt).ToList<Prosjekt>();
                return prosjektListe;
            
            }
        }
        static public List<Bruker> GetAlleAktiveBrukere()
        {
            using (var context = new Context())
            {
                var brukerListe = (from bruker in context.Brukere
                                   where bruker.Aktiv == true
                                   select bruker).ToList<Bruker>();
                return brukerListe;
            
            }
        }

        static public List<Bruker> GetAlleBrukere()
        {
            using (var context = new Context())
            { 
                var brukerListe = context.Brukere.ToList();
                return brukerListe;
            }
        }
        static public List<Bruker> GetAlleAktiveBrukereID(int bruker_id)
        {
            using (var context = new Context())
            {
                var brukerListe = (from bruker in context.Brukere
                                   where bruker.Bruker_id == bruker_id
                                   select bruker).ToList<Bruker>();
                return brukerListe;

            }
        }
        static public List<Bruker> GetAlleBrukerePaaTeam(int valgtTeam_id)
        {
            using (var context = new Context())
            {
                List<Bruker> brk = context.Brukere.Where(bruker => bruker.Teams.Any(team => team.Team_id == valgtTeam_id)).ToList();
                return brk;
                 
            }
        }


        static public List<Prosjekt> GetAlleAktiveProsjekter()
        {
            using (var context = new Context())
            {
                var prosjektListe = (from prosjekter in context.Prosjekter
                                     where prosjekter.Aktiv == true
                                     select prosjekter).ToList<Prosjekt>();
                return prosjektListe;

            }
        }

        static public List<Oppgave> GetAllePabegynteOppgaverForBruker(int bruker_id)
        {
            using (var context = new Context())
            {
                var oppgaveListe = context.Oppgaver
                                 .Include("Brukere")
                                 .Where(oppgave => oppgave.Brukere.Any(bruker => bruker.Bruker_id == bruker_id))
                                 .Where(oppgave => oppgave.Status_id == 2)
                                 .OrderBy(oppgave => oppgave.Tittel)
                                 .ToList();
                return oppgaveListe;

            }
        }

        static public List<Oppgave> GetAlleFerdigeOppgaverForBruker(int bruker_id)
        {
            using (var context = new Context())
            {
                var oppgaveListe = context.Oppgaver
                                 .Include("Brukere")
                                 .Where(oppgave => oppgave.Brukere.Any(bruker => bruker.Bruker_id == bruker_id))
                                 .Where(oppgave => oppgave.Status_id == 3)
                                 .OrderBy(oppgave => oppgave.Tittel)
                                 .ToList();
                return oppgaveListe;

            }
        }

        static public List<Prosjekt> GetAlleAktiveProsjekterForBruker(int bruker_id) 
        {
            using (var context = new Context()) 
            {
                var prosjektListe = context.Prosjekter
                                    .Where(prosjekta => prosjekta.Aktiv == true)
                                    .Where(team => team.Team.Brukere.Any(bruker => bruker.Bruker_id == bruker_id))
                                    .ToList();
                return prosjektListe;      
            }
        }
        static public List<Prosjekt> GetAlleProsjekterForLeder(int bruker_id)
        {
            using (var context = new Context())
            {
                var prosjektListe = context.Prosjekter
                                    .Where(prosjekta => prosjekta.Bruker_id == bruker_id)
                                    .ToList();
                return prosjektListe;
            }
        }
        static public List<Prosjekt> GetAlleBrukereProsjektTeam(int team_id)
        {
            using (var context = new Context())
            {
                var prosjektListe = context.Prosjekter
                                    .Where(prosjekta => prosjekta.Team_id == team_id)
                                    .ToList();
                return prosjektListe;
            }
        }
        static public List<Status> GetAlleStatuser()
        {
            using (var context = new Context())
            {
                var status = (from statuser in context.Status
                              select statuser).ToList<Status>();
                return status;
            }
        }
        static public List<Prioritering> GetAllePrioriteringer(){

            using (var context = new Context())
            {
                var priori = (from prioriteringer in context.Prioriteringer
                              select prioriteringer).ToList<Prioritering>();
                return priori;
            }
        }

        /*
         * Ikke klar
        static public List<Team> GetTeamFromGruppe(int _gruppe_id)
        {
            using (var context = new Context())
            {
                var IQueryList = context.Teams.Where(p => p. == _gruppe_id);
                if (IQueryList.Any())
                {
                    return IQueryList.ToList<Bruker>();
                }
                else
                {
                    return null;
                }

            }
        }
         * */

        static public Team GetTeam(int team_id)
        {
            using (var context = new Context())
            {
                var team = context.Teams.Find(team_id);
                return team;
            }
        }

        static public Rettighet GetRettighet(int bruker_id)
        {
            using (var context = new Context())
            {
                var rettighet = context.Rettigheter
                                .Where(brukere => brukere.Brukere.Any(bruker => bruker.Bruker_id == bruker_id))
                                .FirstOrDefault();
                return rettighet;
            }
        }
        static public List<Rettighet> GetAlleRettigheter()
        {
            using (var context = new Context())
            {
                var rettigheter = (from rett in context.Rettigheter
                                   select rett).ToList<Rettighet>();
                return rettigheter;
            }
        }
        static public List<Team> GetAlleAktiveTeam()
        {
            using (var context = new Context())
            {
                var teamListe = (from teams in context.Teams
                                   where teams.Aktiv == true
                                   select teams).ToList<Team>();
                return teamListe;

            }
        }

        static public Team GetTeamByName(string teamName)
        {
            using (var context = new Context())
            {
                List<Team> allSelectedTeams = context.Teams.Where(x => x.Navn == teamName).ToList();
                Team valgtTeam = allSelectedTeams[0];
                return valgtTeam;
            }
        }

        static public Team GetTeamById(int teamId)
        {
            using (var context = new Context())
            {
                Team valgtTeam = context.Teams.Where(Team => Team.Team_id == teamId).FirstOrDefault();
                return valgtTeam;
            }
        }

        static public List<Oppgave> GetAlleAktiveOppgaver()
        {
            using (var context = new Context())
            {
                var oppgaveListe = (from oppgaver in context.Oppgaver
                                 where oppgaver.Aktiv == true
                                 select oppgaver).ToList<Oppgave>();
                return oppgaveListe;

            }
        }

        static public List<Oppgave> GetAlleAktiveOppgaverForProsjekt(int _prosjekt_id)
        {
            using (var context = new Context())
            {
                var oppgaveListe = context.Oppgaver
                                  .Include("Brukere")
                                  .Include("Kommentarer")
                                  .Where(oppgave => oppgave.Prosjekt_id == _prosjekt_id)
                                  .Where(oppgave => oppgave.Aktiv == true)
                                  .OrderBy(oppgave => oppgave.Tittel)
                                  .ToList();
                return oppgaveListe;
            }
        }

        static public List<Oppgave> GetAlleAktiveOppgaverForProsjektOgBruker(int _prosjekt_id, int _bruker_id)
        {
            using (var context = new Context())
            {
                var oppgaveListe = context.Oppgaver
                                  .Include("Brukere")
                                  .Include("Kommentarer")
                                  .Where(oppgave => oppgave.Prosjekt_id == _prosjekt_id)
                                  .Where(oppgave => oppgave.Brukere.Any(bruker => bruker.Bruker_id == _bruker_id))
                                  .Where(oppgave => oppgave.Aktiv == true)
                                  .OrderBy(oppgave => oppgave.Tittel)
                                  .ToList();
                return oppgaveListe;
            }
        }

        static public List<Oppgave> GetAlleOppgaverForProsjekt(int _prosjekt_id)
        {
            using (var context = new Context())
            {
                var oppgaveListe = context.Oppgaver
                                  .Include("Brukere")
                                  .Include("Kommentarer")
                                  .Where(oppgave => oppgave.Prosjekt_id == _prosjekt_id)
                                  .OrderBy(oppgave => oppgave.Tittel)
                                  .ToList();
                return oppgaveListe;
            }
        }
        static public List<Oppgave> GetAlleAktiveOppgaverForBruker(int _bruker_id)
        {
            using (var context = new Context())
            {
                var oppgaveListe = context.Oppgaver
                                  .Include("Brukere")
                                  .Include("Kommentarer")
                                  .Where(oppgave => oppgave.Brukere.Any(bruker => bruker.Bruker_id == _bruker_id))
                                  .Where(oppgave => oppgave.Aktiv == true)
                                  .OrderBy(oppgave => oppgave.Tittel)
                                  .ToList();
                return oppgaveListe;
            }
        }

        static public List<Gruppe> GetAlleAktiveGrupper()
        {
            using (var context = new Context())
            {
                // teams = context.Teams.Include(x => x.).ToList();
                var gruppeListe = (from grupper in context.Grupper
                                 where grupper.Aktiv == true
                                 select grupper).ToList<Gruppe>();
                return gruppeListe;

            }
        }

        static public List<Bruker> GetAlleBrukereIEtTeam(int _team_id)
        {
            int team_id = _team_id;
            using (var context = new Context())
            {
                var brukerListe = (from bruker in context.Brukere
                                   where bruker.Teams.Any(team => team.Team_id == team_id)
                                   orderby bruker.Etternavn
                                   select bruker).ToList();

                return brukerListe;
            }
        }
        static public List<Bruker> GetAlleBrukereIEtProjekt(int prosjekt_id)
        {
            using (var context = new Context())
            {
                var brukerListe = context.Brukere
                                    .Include("Teams")
                                    .Include("Prosjekter")
                                    .Where(bruker => bruker.Prosjekter.Any(prosjekt => prosjekt.Prosjekt_id == prosjekt_id))
                                    .OrderBy(bruker => bruker.Etternavn)
                                    .ToList();
                return brukerListe;
            }
        }

        static public List<Prosjekt> GetAlleProsjektEnBrukerErMedI(int bruker_id)
        {
            using (var context = new Context())
            {
                var teamListe = (from prosjekt in context.Prosjekter
                                 where prosjekt.Team.Prosjekter.Any(bruker => bruker.Bruker_id == bruker_id)
                                 select prosjekt).ToList<Prosjekt>();
                return teamListe;
            }
        }

        static public List<Kommentar> GetAlleKommentarTilBruker(int brukder_id)
        {
            using (var context = new Context())
            {
                var komListe = (from kommentar in context.Kommentarer
                                where kommentar.Bruker_id == brukder_id 
                                where kommentar.Aktiv == true
                                select kommentar).ToList<Kommentar>();
                return komListe;
            }
        }

        static public List<Moete> GetAlleMoeterFraBrukerErMedI(int bruker_id)
        {
            using (var context = new Context())
            {
                var moeteListe = (from moeter in context.Moeter
                                 where moeter.Brukere.Any(bruker => bruker.Bruker_id == bruker_id)
                                 select moeter).ToList<Moete>();
                return moeteListe;
            }
        }

        static public List<Team> GetAlleTeamsEnBrukerErMedI(int _bruker_id)
        {
            int bruker_id = _bruker_id;
            using (var context = new Context())
            {
                var teamListe = (from team in context.Teams
                                 where team.Brukere.Any(bruker => bruker.Bruker_id == bruker_id)
                                 select team).ToList();
                return teamListe;
            }
        }
        static public List<Team> GetTeamMedList(int team_id)
        {
            using (var context = new Context())
            {
                var teamListe = (from team in context.Teams
                                 where team.Team_id == team_id
                                 select team).ToList<Team>();
                return teamListe;
            }
        }
        public static string GetProsjektNavn(int prosjekt_id)
        {
            using (SqlCommand command = new SqlCommand())
            {
                string prosjektNavn = string.Empty;
                string query = "SELECT * FROM Prosjekt WHERE Bruker_id = " + prosjekt_id + "'";
                command.Connection = new SqlConnection(ConfigurationManager.ConnectionStrings["sysUt14Gr03"].ConnectionString);
                
                var reader = command.ExecuteReader();
                
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        prosjektNavn = reader.GetString(1);
                    }
                }
                else
                {
                    prosjektNavn = "Fikk ikke hentet ut informasjon fra tabellen Prosjekt";
                }
                reader.Close();
                command.Connection.Close();
                return prosjektNavn;
            }
        }
        public static string getStatusNavn(int status_id)
        {
            using (SqlCommand command = new SqlCommand())
            {
                string statusNavn = string.Empty;
                string query = "SELECT * FROM Status WHERE Bruker_id = " + status_id + "'";
                command.Connection = new SqlConnection(ConfigurationManager.ConnectionStrings["sysUt14Gr03"].ConnectionString);

                var reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        statusNavn = reader.GetString(1);
                    }
                }
                else
                {
                    statusNavn = "Fikk ikke hentet ut informasjon fra tabellen Prosjekt";
                }
                reader.Close();
                command.Connection.Close();
                return statusNavn;
            }
        }

        /* Legger til eller fjerner brukere på et team
        Brukes i AdministrasjonAvTeamBrukere */
        public static void UpdateBrukerePaaTeam(Team teamOppd, Bruker brukerOppd, int LeggTil1Fjern2)
        {
            using (var context = new Context())
            {
                Team _teamOppd = context.Teams.FirstOrDefault(Team => Team.Navn == teamOppd.Navn);
                Bruker _brukerOppd = context.Brukere.Where(Bruker => Bruker.Bruker_id == brukerOppd.Bruker_id).FirstOrDefault();
                if (LeggTil1Fjern2 == 1) {
                    _teamOppd.Brukere.Add(_brukerOppd);
                }
                else if (LeggTil1Fjern2 == 2)
                {
                    _teamOppd.Brukere.Remove(_brukerOppd);
                }

                context.SaveChanges();
            }
        }

        public static void ArkiverTeam(Team t)
        {
            using (var context = new Context())
            {
                Team _t = context.Teams.Where(Team => Team.Team_id == t.Team_id).FirstOrDefault();
                _t.Aktiv = false;
                context.SaveChanges();
            }
        }
    }
}