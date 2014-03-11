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
    public partial class OpprettOppgave : System.Web.UI.Page
    {
        private List<Bruker> brukerListe;
        private List<Bruker> selectedBruker = new List<Bruker>();
        private List<Prosjekt> prosjektListe;
        private List<Prioritering> pri;
        private List<Oppgave> visOppgaver;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                visOppgaver = Queries.GetAlleAktiveOppgaver();
                brukerListe = Queries.GetAlleAktiveBrukere();
                prosjektListe = Queries.GetAlleAktiveProsjekter();
                pri = Queries.GetAllePrioriteringer();

                for (int i = 0; i < visOppgaver.Count; i++)
                {
                    Oppgave oppg = visOppgaver[i];
                    lbOppgaver.Items.Add(new ListItem(oppg.Tittel, oppg.Oppgave_id.ToString()));
                }
                for (int i = 0; i < brukerListe.Count; i++)
                {
                    Bruker bruker = brukerListe[i];
                    ddlBrukere.Items.Add(new ListItem(bruker.Fornavn, bruker.Bruker_id.ToString()));
                }
                for (int i = 0; i < prosjektListe.Count; i++)
                {
                    Prosjekt prosjekt = prosjektListe[i];

                    ddlProsjekt.Items.Add(new ListItem(prosjekt.Navn, prosjekt.Prosjekt_id.ToString()));
                }
                for (int i = 0; i < pri.Count; i++)
                {
                    Prioritering priori = pri[i];
                    ddlPrioritet.Items.Add(new ListItem(priori.Navn, priori.Prioritering_id.ToString()));
                }
            }
        }
        private void OpprettOppg()
        {
            
            using (var context = new Context())
            {
                int priorietring_id = Convert.ToInt32(ddlPrioritet.SelectedValue);
                int prosjekt_id = Convert.ToInt32(ddlProsjekt.SelectedValue);
                float estimering = Convert.ToInt16(TbEstimering.Text);

                foreach (ListItem s in lbBrukere.Items)
                {
                    string navn = s.Value;
                    Bruker bruk = context.Brukere.Where(b => b.Fornavn == navn).First();
                    selectedBruker.Add(brukerListe[bruk.Bruker_id]);
                }

                var oppgave = new Oppgave
                {
                    Opprettet = DateTime.Now,
                    Tittel = tbTittel.Text,
                    Aktiv = true,
                    UserStory = tbBeskrivelse.Text,
                    Estimat = estimering,
                    
                    Brukere = selectedBruker,
                    Prosjekt_id = prosjekt_id,
                    Prioritering_id = priorietring_id
                };

                context.Oppgaver.Add(oppgave);
                context.SaveChanges();
            }
        }

        protected void btnOpprett_Click(object sender, EventArgs e)
        {
            OpprettOppg();
        }

        protected void btnBrukere_Click(object sender, EventArgs e)
        {
            lbBrukere.Items.Add(ddlBrukere.SelectedItem.ToString());
        }

        protected void btnVelg_Click(object sender, EventArgs e)
        {
            lbAvhengighet.Items.Add(lbOppgaver.SelectedItem.ToString());
        }

        protected void btnFjern_Click(object sender, EventArgs e)
        {
            lbAvhengighet.Items.Remove(lbOppgaver.SelectedItem.ToString());
        }
    }
}