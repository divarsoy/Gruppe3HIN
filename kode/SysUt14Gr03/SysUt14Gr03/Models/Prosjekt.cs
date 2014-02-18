using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SysUt14Gr03.Models
{
    public class Prosjekt
    {
        [Key]
        public int Prosjekt_id { get; set; }
        public string Navn { get; set; }
        public bool Aktiv { get; set; }
        public DateTime? StartDato { get; set; }
        public DateTime? SluttDato { get; set; }
        public DateTime Opprettet { get; set; }

        public int? Team_id { get; set; }
        public int? Gruppe_id { get; set; }

        [Display(Name = "Prosjektleder")]
        public int Bruker_id { get; set; }

        public virtual Team Team { get; set; }
        public virtual Gruppe Gruppe { get; set; }
        public virtual Bruker Bruker { get; set; }
        public virtual ICollection<Oppgave> Oppgaver { get; set; }
    }
}