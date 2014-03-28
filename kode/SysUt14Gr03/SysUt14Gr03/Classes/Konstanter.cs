using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SysUt14Gr03.Classes
{
    public class Konstanter
    {
        public enum rettighet { Administrator, Prosjektleder, Teamleder, Utvikler };
        public enum notifikasjonsTyper { success, info, warning, danger };
    }
}