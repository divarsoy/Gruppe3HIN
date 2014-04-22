using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SysUt14Gr03.Models;
using SysUt14Gr03.Classes;
using SysUt14Gr03.Migrations;

namespace SysUt14Gr03.Classes
{
    public class OppretteLogg
    {
        public static void opprettLoggForBruker(string hendelse, DateTime opprettet, int brukerId)
        {
            using (var context = new Context())
            {
                Logg nyLogg = new Logg
                {
                    Hendelse = hendelse,
                    Opprettet = opprettet,
                    bruker_id = brukerId
                };

                context.Logger.Add(nyLogg);
                context.SaveChanges();
            }
        }
    }
}