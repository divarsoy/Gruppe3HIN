using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SysUt14Gr03.Models
{
    [Table("Moeter")]
    public class Moete
    {
        [Key]
        public int Moete_id { get; set; }
        public DateTime Tidspunkt { get; set; }
        public string Tittel { get; set; }
        public string Tekst { get; set; }
        public DateTime Opprettet { get; set; }

        public virtual List<Bruker> Brukere { get; set; }
    }
}