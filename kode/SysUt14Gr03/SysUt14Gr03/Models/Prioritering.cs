using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SysUt14Gr03.Models
{
    [Table("Prioriteringer")]
    public class Prioritering
    {
        [Key]
        public int Prioritering_id { get; set; }
        public string Navn { get; set; }

        public virtual List<Oppgave> Oppgaver { get; set; }
    }
}