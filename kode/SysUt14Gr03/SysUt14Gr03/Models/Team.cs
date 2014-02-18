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

        public virtual ICollection<Bruker> Brukere { get; set; }
        public virtual ICollection<Prosjekt> Prosjekter { get; set; }
        public virtual ICollection<Gruppe> Grupper { get; set; }
    }
}