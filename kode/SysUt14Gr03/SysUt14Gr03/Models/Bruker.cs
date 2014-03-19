using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SysUt14Gr03.Models
{
    public class Bruker
    {
        [Key]
        public int Bruker_id {get; set;}
        public string Etternavn { get; set; }
        public string Fornavn { get; set; }
        public string Brukernavn { get; set; }
        public string Epost { get; set; }
        public string Passord { get; set; }
        public string IM { get; set; }
        public string Token { get; set; }
        public bool Aktivert { get; set; }
        public bool Aktiv { get; set; }
        public DateTime opprettet { get; set; }

        public virtual List<BrukerPreferanse> BrukerPreferanser { get; set; }
        public virtual List<Rettighet> Rettigheter { get; set; }
        public virtual List<Moete> Moeter { get; set; }
        public virtual List<Kommentar> Kommentarer { get; set; }
        public virtual List<Logg> Logger { get; set; }
        public virtual List<Oppgave> Oppgaver { get; set; }
        public virtual List<Prosjekt> Prosjekter { get; set; }
        public virtual List<Team> Teams { get; set; }

        public override string ToString()
        {
            return Fornavn + " " + Etternavn;
        }
    }
}