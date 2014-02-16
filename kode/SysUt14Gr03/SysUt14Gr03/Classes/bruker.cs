using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SysUt14Gr03
{
    public class Bruker
    {
        public int ID { get; set; }
        public string Etternavn { get; set; }
        public string Fornavn { get; set; }

        public string Navn()
        {
            return Fornavn + " " + Etternavn;
        }
    }
}