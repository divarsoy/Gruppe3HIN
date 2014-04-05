using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SysUt14Gr03.Models
{
    public class Time
    {
        [Key]
        public int Time_id { get; set; }
        public TimeSpan Tid { get; set; }
        public DateTime Opprettet { get; set; }
        public Boolean Aktiv { get; set; }

        public int Oppgave_id { get; set; }
        public int Bruker_id { get; set; }

        public virtual Bruker Bruker { get; set; }
        public virtual Oppgave Oppgave { get; set; }
    }
}