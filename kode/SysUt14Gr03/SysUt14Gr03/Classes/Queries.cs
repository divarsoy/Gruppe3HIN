using System;
using System.Collections.Generic;
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
                var IQueryList = context.Brukere.Where(p => p.Epost == _Epost);
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

      

        static public List<Gruppe> GetAlleAktiveGrupper()
        {
            List<Team> teams;
            using (var context = new Context())
            {
                // teams = context.Teams.Include(x => x.).ToList();
                var gruppeListe = (from grupper in context.Grupper
                                 where grupper.Aktiv == true
                                 select grupper).ToList<Gruppe>();
                return gruppeListe;

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
    }
}