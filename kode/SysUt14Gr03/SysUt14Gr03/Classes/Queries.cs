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

        static public List<Bruker> GetAlleBrukerePaaTeam(int valgtTeam_id)
        {
            using (var context = new Context())
            {
                List<Team> teamene = context.Teams.Where(x => x.Team_id == valgtTeam_id).ToList();
                if (teamene.Count > 0)
                {
                    Team team = teamene[0];
                    return team.Brukere;
                }
                else
                {
                    List<Bruker> tomListe = new List<Bruker>();
                    return tomListe;
                }

                
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

        static public List<Bruker> GetAlleBrukereIEtTeam(int _team_id) {
            int team_id = _team_id;
            using (var context = new Context())
            {
                var brukerListe = (from bruker in context.Brukere
                                   where bruker.Teams.Any(team => team.Team_id == team_id)
                                   select bruker).ToList();
                
                return brukerListe;
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



        static public Team GetTeam(int _team_id)
        {
            using (var context = new Context())
            {
                var team = context.Teams.Find(_team_id);
                return team;
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
                List<Team> allSelectedTeams = context.Teams.Where(x => x.Team_id == teamId).ToList();
                Team valgtTeam = allSelectedTeams[0];
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

        static public List<Oppgave> GetAlleAktiveOppgaverDag()
        {
            using (var context = new Context())
            {
                var oppgaveListe = context.Oppgaver
                                  .Include("Brukere")
                                  .ToList();

//                                    .Where(oppgave => oppgave.Aktiv == true)                                    
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
                                   select bruker).ToList();

                return brukerListe;
            }
        }

        static public List<Prosjekt> GetAlleProsjektFraBrukerErMedI(int bruker_id)
        {
            using (var context = new Context())
            {
                var teamListe = (from prosjekt in context.Prosjekter
                                 where prosjekt.Team.Prosjekter.Any(bruker => bruker.Bruker_id == bruker_id)
                                 select prosjekt).ToList<Prosjekt>();
                return teamListe;
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

        /*
        static public List<Rettighet> GetAlleRettigheter()
        {
            using (var context = new Context())
            {
                return context.Rettigheter.ToList<Rettighet>();
            }
        }
        */
        /*
        public static IQueryable<Rettighet> GetAlleRettigheter()
        {
            var context = new Context();
            IQueryable<Rettighet> query = context.Rettigheter;
            return query;
        }
        */

        public static string getProsjektNavn(int prosjekt_id)
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
        public static void UpdateBrukerePaaTeam(Team teamAAOppdatere, Bruker brukerAAOppdatere, int LeggTil1Fjern2)
        {
            using (var context = new Context())
            {
                Team _teamAAOppdatere = context.Teams.FirstOrDefault(Team => Team.Navn == teamAAOppdatere.Navn);
                if (LeggTil1Fjern2 == 1)
                    _teamAAOppdatere.Brukere.Add(brukerAAOppdatere);
                else if (LeggTil1Fjern2 == 2)
                    _teamAAOppdatere.Brukere.Remove(brukerAAOppdatere);

                context.SaveChanges();
            }
            

        }
    }
}