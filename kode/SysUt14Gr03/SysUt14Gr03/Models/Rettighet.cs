using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SysUt14Gr03.Models
{
    [Table("Rettigheter")]
    public class Rettighet
    {
        [Key]
        public int Rettighet_id { get; set; }
        public string RettighetNavn { get; set; }

        public virtual List<Bruker> Brukere { get; set; }
    }
}