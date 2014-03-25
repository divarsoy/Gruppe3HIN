using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SysUt14Gr03;
using SysUt14Gr03.Models;
using SysUt14Gr03.Classes;
using System.Text;
namespace SysUt14Gr03.Classes
{
    public class NotifikasjonFlash
    {
        static public string HentNotifikasjoner(int bruker_id){
            var notifikasjonsListe = Queries.GetNotifikasjon(bruker_id);
            StringBuilder returnString = new StringBuilder();
            foreach (Notifikasjon notifikasjon in notifikasjonsListe)
            {
                returnString.Append("<div id='flash' class='flash alert alert-dismissable '");
                returnString.Append(notifikasjon);
                returnString.Append("'>");
                returnString.Append(Environment.NewLine);
                returnString.Append("<button type='button' class='close' data-dismiss='alert' aria-hidden='true'>&times;</button>");
                returnString.Append(Environment.NewLine);
                returnString.Append(notifikasjon.Melding);
                returnString.Append("</div>");
                returnString.Append(Environment.NewLine);
            }
            return returnString.ToString();
        }
    }
}