using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SysUt14Gr03.Models
{
    [Table("NotifikasjonsTyper")]
    public class NotifikasjonsType
    {
        [Key]
        public int NotifikasjonsType_id { get; set; }
        public string Type { get; set; }
    }
}