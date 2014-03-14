using System;
using System.Collections.Generic;
using System.Drawing;
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
        private List<Bruker> brukerListe;
        private List<Team> teamListe;
    
        private int team_id;
        private int bruker_id;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                teamListe = Queries.GetAlleAktiveTeam();
                brukerListe = Queries.GetAlleAktiveBrukere();

                for (int i = 0; i < teamListe.Count(); i++)
                {
                    Team team = teamListe[i];
                    dropTeam.Items.Add(new ListItem(team.Navn, team.Team_id.ToString()));
                }
                for (int i = 0; i < brukerListe.Count(); i++)
                {
                    Bruker bruker = brukerListe[i];
                    ddlBrukere.Items.Add(new ListItem(bruker.ToString(), bruker.Bruker_id.ToString()));
                }
            }
        }

        protected void btnLagre_Click(object sender, EventArgs e)
        {
            opprettProsjekt();
        }
        private void opprettProsjekt()
        {

            if (tbProsjektnavn.Text != String.Empty && tbStart.Text != String.Empty && tbSlutt.Text != String.Empty)
            {
                dtStart = Convert.ToDateTime(tbStart.Text);
                dtSlutt = Convert.ToDateTime(tbSlutt.Text);
                team_id = Convert.ToInt32(dropTeam.SelectedValue);
                bruker_id = Convert.ToInt32(ddlBrukere.SelectedValue);


                using (var context = new Context())
                {
                    var nyttProsjekt = new Prosjekt { Navn = tbProsjektnavn.Text, Bruker_id = bruker_id, Aktiv = true, Opprettet = DateTime.Now, Team_id = team_id, StartDato = dtStart, SluttDato = dtSlutt };
                    context.Prosjekter.Add(nyttProsjekt);
                    context.SaveChanges();
                }


                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Prosjektet ble lagret');", true);

                Response.Redirect("OpprettProsjekt.aspx");
            }
            else
            {
                lblFeil.Visible = true;
                lblFeil.ForeColor = Color.Red;
                lblFeil.Text = "Feltene kan ikke være tomme";
            }
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