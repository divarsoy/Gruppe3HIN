using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SysUt14Gr03.Models
{
    public class Notifikasjon
    {
        [Key]
        public int Notifikasjon_id { get; set; }
        public string Melding { get; set; }
        public int NotifikasjonsType_id { get; set; }
        public int Bruker_id { get; set; }
        public bool Vist { get; set; }

        public virtual Bruker bruker { get; set; }
        public virtual NotifikasjonsType notifikasjonsType { get; set; }
    }
}