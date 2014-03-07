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

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                brukerListe = Queries.GetAlleAktiveBrukere();
                prosjektListe = Queries.GetAlleAktiveProsjekter();
                pri = Queries.GetAllePrioriteringer();
                for (int i = 0; i < brukerListe.Count; i++)
                {
                    Bruker bruker = brukerListe[i];
                   // ddlBrukere.Items.Add(new ListItem(bruker.Fornavn, bruker.Bruker_id.ToString()));
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

                var oppgave = new Oppgave
                {
                    Opprettet = DateTime.Now,
                    Aktiv = true,
                    userStory = tbBeskrivelse.Text,
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


            // Legger til valgte brukere
            for (int i = 0; i < ddlBrukere.Items.Count; i++)
            {
                if (ddlBrukere.Items[i].Selected)
                {
                    selectedBruker.Add(brukerListe[i]);
                    //numberSelected++;
                }
            }
            OpprettOppg();
        }
    }
}