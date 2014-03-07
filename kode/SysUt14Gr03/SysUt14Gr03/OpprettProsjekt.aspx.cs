using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SysUt14Gr03.Classes;
using SysUt14Gr03.Models;

namespace SysUt14Gr03
{
    public partial class OpprettProsjekt : System.Web.UI.Page
    {
        private DateTime dtStart;
        private DateTime dtSlutt;
        private List<Gruppe> gruppeListe;
        private List<Team> teamListe;
        private int gruppe_id;
        private int team_id;
        private int bruker_id;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                teamListe = Queries.GetAlleAktiveTeam();
                gruppeListe = Queries.GetAlleAktiveGrupper();


                using (var context = new Context())
                {
                    System.Windows.Forms.BindingSource bindingSource1 = new System.Windows.Forms.BindingSource();
                    bindingSource1.DataSource = context.Prosjekter.ToList<Prosjekt>();
                    gridViewProsjekt.DataSource = bindingSource1;
                    gridViewProsjekt.DataBind();
                }

                for (int i = 0; i < teamListe.Count(); i++)
                {
                    Team team = teamListe[i];
                    dropTeam.Items.Add(new ListItem(team.Navn, team.Team_id.ToString()));
                }
                for (int i = 0; i < gruppeListe.Count; i++)
                {
                    Gruppe gruppe = gruppeListe[i];
                    dropGruppe.Items.Add(new ListItem(gruppe.Navn, gruppe.Gruppe_id.ToString()));
                }
            }
        }

        protected void btnLagre_Click(object sender, EventArgs e)
        {
            opprettProsjekt();
        }
        private void opprettProsjekt()
        {

            dtStart = Convert.ToDateTime(tbStart.Text);
            dtSlutt = Convert.ToDateTime(tbSlutt.Text);
            team_id = Convert.ToInt32(dropTeam.SelectedValue);
            gruppe_id = Convert.ToInt32(dropGruppe.SelectedValue);
            string fornavn = tbProsjektleder.Text;
            using (var context = new Context())
            {
                Bruker bruker = context.Brukere.Where(b => b.Fornavn == fornavn).First();
                bruker_id = bruker.Bruker_id;
            }
            using (var context = new Context())
            {
                var nyttProsjekt = new Prosjekt { Navn = tbProsjektnavn.Text, Bruker_id = bruker_id, Aktiv = true, Opprettet = DateTime.Now, Gruppe_id = gruppe_id, Team_id = team_id, StartDato = dtStart, SluttDato = dtSlutt };
                context.Prosjekter.Add(nyttProsjekt);
                context.SaveChanges();
            }


            ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Prosjektet ble lagret');", true);
            Response.Redirect("OpprettProsjekt.aspx");

        }

        protected void btnStart_Click(object sender, EventArgs e)
        {
            tbStart.Text = cal.SelectedDate.ToShortDateString();

        }

        protected void btnSlutt_Click(object sender, EventArgs e)
        {
            tbSlutt.Text = cal.SelectedDate.ToShortDateString();
        }
    }
}