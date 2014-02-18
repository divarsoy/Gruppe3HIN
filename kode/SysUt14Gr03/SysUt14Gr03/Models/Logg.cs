using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SysUt14Gr03.Models
{
    public class Logg
    {
        [Key]
        public int Logg_id { get; set; }
        public string Hendelse { get; set; }
        public DateTime Opprettet { get; set; }
        public int bruker_id { get; set; }

        public virtual Bruker Bruker { get; set; }
    }
}