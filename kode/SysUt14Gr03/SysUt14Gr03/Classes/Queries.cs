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