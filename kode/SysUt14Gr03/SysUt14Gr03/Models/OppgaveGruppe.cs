using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SysUt14Gr03.Models
{
    public class OppgaveGruppe
    {
        [Key]
        public int OppgaveGruppe_id { get; set; }
        public string navn { get; set; }

        public virtual ICollection<Oppgave> Oppgaver { get; set; }
    }
}