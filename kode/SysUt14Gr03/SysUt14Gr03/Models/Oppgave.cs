using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SysUt14Gr03.Models
{
    public class Oppgave
    {
        [Key]
        public int Oppgave_id { get; set; }
        public string Tittel { get; set; }
        public string userStory { get; set; }
        public string krav { get; set; }
        public float? Estimat { get; set; }
        public float? BruktTid { get; set; }
        public DateTime? Tidsfrist { get; set; }
        public bool Aktiv { get; set; }
        public DateTime Opprettet { get; set; }

        public int Prosjekt_id { get; set; }
        public int? OppgaveGruppe_id { get; set; }
        public int? Prioritering_id { get; set; }
        public int Status_id { get; set; }

        public virtual Prosjekt Prosjekt { get; set; }
        public virtual OppgaveGruppe OppgaveGruppe { get; set; }
        public virtual Prioritering Prioritering { get; set; }
        public virtual Status Status { get; set; }
        public virtual List<Kommentar> Kommentarer { get; set; }
        public virtual List<Bruker> Brukere { get; set; }


    }
}