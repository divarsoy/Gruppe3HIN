using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SysUt14Gr03.Models
{
    public class Gruppe
    {
        [Key]
        public int Gruppe_id { get; set; }
        public string Navn { get; set; }
        public bool Aktiv { get; set; }
        public DateTime Opprettet { get; set; }

        public virtual List<Prosjekt> Prosjekter { get; set; }
        public virtual List<Team> Teams { get; set; }
    }
}