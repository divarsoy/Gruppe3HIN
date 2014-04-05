using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SysUt14Gr03.Models
{
    public class Team
    {
        [Key]
        public int Team_id { get; set; }
        public string Navn { get; set; }
        public bool Aktiv { get; set; }
        public DateTime Opprettet { get; set; }

        public virtual List<Bruker> Brukere { get; set; }
        public virtual List<Prosjekt> Prosjekter { get; set; }
    }
}