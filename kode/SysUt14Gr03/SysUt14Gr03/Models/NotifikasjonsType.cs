using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SysUt14Gr03.Models
{
    public class NotifikasjonsType
    {
        [Key]
        public int NotifikasjonsType_id { get; set; }
        public string Type { get; set; }
    }
}