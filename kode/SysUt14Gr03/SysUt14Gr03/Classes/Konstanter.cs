using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SysUt14Gr03.Classes
{
    /// <summary>
    /// Konstanter klassen setter opp parametre som går igjen over hele prosjektet
    /// Slik som prosjektleder, administrator og utvikler
    /// </summary>
    public class Konstanter
    {
        public enum rettighet { Administrator, Prosjektleder, Utvikler };
        public enum notifikasjonsTyper { success, info, warning, danger };
        public const int SALTSIZE = 40;
        public const string FELLES_TEST_PASSORD = "appelsinFarge5";
    }
}