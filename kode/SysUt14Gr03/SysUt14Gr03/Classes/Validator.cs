using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

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
    }
}