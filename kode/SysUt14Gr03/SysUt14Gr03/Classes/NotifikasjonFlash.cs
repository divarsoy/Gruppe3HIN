using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SysUt14Gr03;
using SysUt14Gr03.Models;
using SysUt14Gr03.Classes;
using System.Text;
using System.Web.UI.WebControls;

namespace SysUt14Gr03.Classes
{
    public class NotifikasjonFlash
    {
        static public string HentNotifikasjoner(int bruker_id){
            var notifikasjonsListe = Queries.GetNotifikasjon(bruker_id);
            StringBuilder returnString = new StringBuilder();
            foreach (Notifikasjon notifikasjon in notifikasjonsListe)
            {
                returnString.Append("<div id='flash' class='flash alert alert-dismissable ");
                returnString.Append(Queries.GetNotifikasjonsType(notifikasjon.NotifikasjonsType_id).Type);
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
        static public Panel HentNotifikasjonsPanel(int bruker_id)
        {
            var notifikasjonsListe = Queries.GetNotifikasjon(bruker_id);
            Panel panel = new Panel();
            foreach (Notifikasjon notifikasjon in notifikasjonsListe)
            {
                Label label = new Label();
                label.Text = String.Format ("<div id='flash' class='flash alert alert-dismissable {0}'>", Queries.GetNotifikasjonsType(notifikasjon.NotifikasjonsType_id).Type);
                LinkButton button = new LinkButton();
                button.CssClass = "close";
                button.Attributes.Add("data-dismiss", "alert");
                button.Attributes.Add("aria-hidden", "true");
                button.Text = "&times;";
                Label labelMelding = new Label();
                labelMelding.Text = notifikasjon.Melding + "</div>";
                panel.Controls.Add(label);
                panel.Controls.Add(button);
                panel.Controls.Add(labelMelding);
            }
            return panel;
        }
    }
}