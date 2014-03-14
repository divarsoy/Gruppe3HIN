using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SysUt14Gr03.Models
{
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