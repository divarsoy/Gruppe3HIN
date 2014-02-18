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
        public bool aktiv { get; set; }
        public DateTime opprettet { get; set; }
        public int bruker_id { get; set; }
        public int oppgave_id {get; set; }

        public virtual Bruker Bruker { get; set; }
        public virtual Oppgave Oppgave {get; set; }
    }
}