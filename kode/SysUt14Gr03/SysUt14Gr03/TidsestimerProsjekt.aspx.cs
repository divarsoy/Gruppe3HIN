using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SysUt14Gr03.Classes;
using SysUt14Gr03.Models;

namespace SysUt14Gr03
{
    public partial class TidsestimerProsjekt : System.Web.UI.Page
    {

        private List<Prosjekt> prosjektListe;

        protected void Page_Load(object sender, EventArgs e)
        {

            prosjektListe = Queries.GetAlleAktiveProsjekter();

            if (!IsPostBack)
            {

                foreach (Prosjekt prosjekt in prosjektListe)
                {
                    lsbProsjekt.Items.Add(new ListItem(prosjekt.Navn, prosjekt.Prosjekt_id.ToString()));
                }


            }
            


        }

        protected void btnDetaljer_Click(object sender, EventArgs e)
        {
            Prosjekt prosjekt = prosjektListe[lsbProsjekt.SelectedIndex];
            txtInfo.Text = prosjekt.Navn + "\n" + prosjekt.SluttDato;
            txtInfo.Visible = true;

        }

        protected void btnFrist_Click(object sender, EventArgs e)
        {
            Feilmelding.Visible = false;
            int index = lsbProsjekt.SelectedIndex;
            // Vis en kalender for å velge dato/tid
            DateTime dato = calCalendar.SelectedDate;
            if (dato != DateTime.MinValue)
            {

                using (var context = new Context())
                {
                    int prosjekt_id = Convert.ToInt32(lsbProsjekt.SelectedValue);

                    Prosjekt prosjekt = context.Prosjekter.FirstOrDefault(p => p.Prosjekt_id == prosjekt_id);

                    prosjekt.SluttDato = dato;
                    context.SaveChanges();

                    FristOK.Text = "Frist satt til " + dato.ToShortDateString() + " på " + prosjektListe[index].Navn;
                    FristOK.Visible = true;

                }
            }
            else
            {
                Feilmelding.Text = "Velg en dato";
                Feilmelding.Visible = true;
            }
        }
    }
}