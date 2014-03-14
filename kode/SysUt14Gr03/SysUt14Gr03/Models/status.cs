using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SysUt14Gr03.Models
{
    public class Status
    {
        [Key]
        public int Status_id { get; set; }
        public string Navn { get; set; }

        public virtual List<Oppgave> Oppgaver { get; set; }
    }
}