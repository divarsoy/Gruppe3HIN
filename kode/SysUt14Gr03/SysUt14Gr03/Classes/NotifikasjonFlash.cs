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
        private Panel panel = new Panel();

        public Panel HentNotifikasjonsPanel(int bruker_id)
        {
            var notifikasjonsListe = Queries.GetNotifikasjon(bruker_id);
            int i = 1;
            foreach (Notifikasjon notifikasjon in notifikasjonsListe)
            {
                Label label = new Label();
                label.Text = String.Format ("<div id='flash' class='flash alert alert-dismissable {0}'>", Queries.GetNotifikasjonsType(notifikasjon.NotifikasjonsType_id).Type);
                LinkButton button = new LinkButton();
                button.CssClass = "close";
                //button.Attributes.Add("data-dismiss", "alert");  Fjernet da den kjører et javascript som overstyrer reload
                button.Attributes.Add("aria-hidden", "true");
                button.Text = "&times;";
                button.Command += new CommandEventHandler(btnNotifikasjon_Click);
                button.CommandArgument = notifikasjon.Notifikasjon_id.ToString();
                button.CommandName = "button" + i;

                //button.Click += new System.EventHandler(btnNotifikasjon_Click);
                //.OnClientClick += new System.EventHandler(btnNotifikasjon_Click);
                Label labelMelding = new Label();
                labelMelding.Text = notifikasjon.Melding + "</div>";
                panel.Controls.Add(label);
                panel.Controls.Add(button);
                panel.Controls.Add(labelMelding);
            }
            return panel;
        }

        protected void btnNotifikasjon_Click(object sender, CommandEventArgs e)
        {
            int notifikasjon_id = Validator.KonverterTilTall(e.CommandArgument.ToString());

            using (Context context = new Context())
            {
                var notifikasjon = context.Notifikasjoner.Find(notifikasjon_id);
                notifikasjon.Vist = true;
                context.SaveChanges();
            }

        }
    }
}