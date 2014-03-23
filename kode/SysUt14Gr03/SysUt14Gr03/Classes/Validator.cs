using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using SysUt14Gr03;
using SysUt14Gr03.Models;
using SysUt14Gr03.Classes;

namespace SysUt14Gr03.Classes
{
    public static class Validator
    {
        public static int KonverterTilTall (string tekst){
            string kunTall = Regex.Replace(tekst, @"\D", "");
            int resultat;
            bool vellykketKonvertering = int.TryParse(kunTall, out resultat);
            if (vellykketKonvertering)
                return resultat;
            else return -1;
        }

        public static bool SjekkRettighet(int bruker_id, Konstanter.rettighet rettighet) {
            Bruker bruker = Queries.GetBrukerMedRettighet(bruker_id, rettighet);
            if (bruker == null)
                return false;
            else
                return true;
        }
    }
}