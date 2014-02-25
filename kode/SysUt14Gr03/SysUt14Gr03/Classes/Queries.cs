using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SysUt14Gr03.Models;

namespace SysUt14Gr03.Classes
{
    static public class Queries
    {
        static public Bruker GetBruker(int _bruker_id)
        {
            using (var context = new Context())
            {
                var bruker = context.Brukere.Find(_bruker_id);
                return bruker;
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
    }
}