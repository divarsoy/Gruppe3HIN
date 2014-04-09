using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SysUt14Gr03.Models
{
    [Table("Pauser")]
    public class Pause
    {
        [Key]
        public int Pause_id { get; set; }
        public DateTime Start { get; set; }
        public DateTime? Stopp { get; set; }
        public int Oppgave_id { get; set; }

        public virtual Oppgave Oppgave { get; set; }
    }
}