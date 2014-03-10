using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SysUt14Gr03.Models
{
    public class Kommentar
    {
        [Key]
        public int Kommentar_id { get; set; }
        public string Tekst { get; set; }
        public bool Aktiv { get; set; }
        public DateTime Opprettet { get; set; }
        public int Bruker_id { get; set; }
        public int Oppgave_id {get; set; }

        public virtual Bruker Bruker { get; set; }
        public virtual Oppgave Oppgave {get; set; }
    }
}