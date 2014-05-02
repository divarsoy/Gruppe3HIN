using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SysUt14Gr03.Models
{
    [Table("Logger")]
    public class Logg
    {
        [Key]
        public int Logg_id { get; set; }
        public string Hendelse { get; set; }
        public DateTime Opprettet { get; set; }
        public int bruker_id { get; set; }
        public int? Prosjekt_id { get; set; }

        public virtual Bruker Bruker { get; set; }
        
    }
}