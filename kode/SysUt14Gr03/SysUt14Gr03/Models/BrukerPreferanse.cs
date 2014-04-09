using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SysUt14Gr03.Models
{
    [Table("BrukerPreferanser")]
    public class BrukerPreferanse
    {
        [Key]
        public int Preferanse_id { get; set; }
        public bool EpostTeam { get; set; }
        public bool EpostProsjekt { get; set; }
        public bool EpostOppgave { get; set; }
        public bool EpostKommentar { get; set; }
        public bool EpostTidsfrist { get; set; }
        public bool EpostRapport { get; set; }
        public int Bruker_id { get; set; }
        public virtual Bruker Bruker { get; set; }

    }
}