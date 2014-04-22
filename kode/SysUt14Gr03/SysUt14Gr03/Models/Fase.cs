using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SysUt14Gr03.Models
{
    [Table("Faser")]
    public class Fase
    {
        [Key]
        public int Fase_id {get; set;}
        public string Navn {get; set;}
        public int Prosjekt_id { get; set; }
        public DateTime Start {get; set;}
        public DateTime Stopp {get; set;}
        public DateTime Opprettet {get; set;}
        public Boolean Aktiv { get; set; }

        [Display(Name = "Faseleder")]
        public int Bruker_id {get; set;}

        public virtual Bruker Bruker { get; set; }
        public virtual Prosjekt Prosjekt { get; set; }
        public virtual List<Oppgave> Oppgaver { get; set; }
    }
}