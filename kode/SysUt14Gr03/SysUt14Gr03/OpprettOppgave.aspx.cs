using System;
using System.Collections.Generic;
using System.Drawing;
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
        private List<Prosjekt> prosjektListe;
        private List<Prioritering> pri;
        private List<Status> visStatus;
        private List<int> valgtBrukerid = new List<int>();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
               
                brukerListe = Queries.GetAlleAktiveBrukere();
                prosjektListe = Queries.GetAlleAktiveProsjekter();
                pri = Queries.GetAllePrioriteringer();
                visStatus = Queries.GetAlleStatuser();
                for (int i = 0; i < visStatus.Count; i++)
                {
                    Status status = visStatus[i];
                    ddlStatus.Items.Add(new ListItem(status.Navn, status.Status_id.ToString()));
                }
                using (var context = new Context())
                {
                   System.Windows.Forms.BindingSource bindingSource1 = new System.Windows.Forms.BindingSource();
                   bindingSource1.DataSource = context.Oppgaver.ToList<Oppgave>();
                   GridViewOppg.DataSource = bindingSource1;
                   GridViewOppg.DataBind();
                }
           
                for (int i = 0; i < brukerListe.Count; i++)
                {
                    Bruker bruker = brukerListe[i];
                    ddlBrukere.Items.Add(new ListItem(bruker.ToString(), bruker.Bruker_id.ToString()));
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
            List<Bruker> selectedBruker = new List<Bruker>();
            if (tbKrav.Text != String.Empty && tbTittel.Text != String.Empty && tbBeskrivelse.Text != String.Empty && TbEstimering.Text != String.Empty)
            {

            
            using (var context = new Context())
            {

                int priorietring_id = Convert.ToInt32(ddlPrioritet.SelectedValue);
                int prosjekt_id = Convert.ToInt32(ddlProsjekt.SelectedValue);
                float estimering = Convert.ToInt16(TbEstimering.Text);
                int status_id = Convert.ToInt32(ddlStatus.SelectedValue);

                foreach (ListItem s in lbBrukere.Items)
                {
                    int navn = Convert.ToInt32(s.Value);
                    Bruker bruk = context.Brukere.Where(b => b.Bruker_id == navn).First();
                    selectedBruker.Add(bruk);
                }

                var oppgave = new Oppgave
                {

                    Krav = tbKrav.Text,
                    Opprettet = DateTime.Now,
                    Tittel = tbTittel.Text,
                    Aktiv = true,
                    UserStory = tbBeskrivelse.Text,
                    Estimat = estimering,
                    Status_id = status_id,
                    Brukere = selectedBruker,
                    Prosjekt_id = prosjekt_id,
                    Prioritering_id = priorietring_id
                };

                context.Oppgaver.Add(oppgave);
                context.SaveChanges();
                Response.Redirect("OpprettOppgave.aspx");
            }
            }
            else
            {
                lblCheck.Visible = true;
                lblCheck.ForeColor = Color.Red;
                lblCheck.Text = "Feltene kan ikke være tomme!";
            }
        }

        protected void btnOpprett_Click(object sender, EventArgs e)
        {
            OpprettOppg();
        }

        protected void btnBrukere_Click(object sender, EventArgs e)
        {
            

                ListItem selectedUser = ddlBrukere.SelectedItem;
                selectedUser.Selected = false;
                if (lbBrukere.Items.Contains(selectedUser))
                {
                    lblFeil.Visible = true;
                    lblFeil.ForeColor = Color.Red; 
                    lblFeil.Text =  selectedUser + " er allerede valgt!";
                }
                else
                {
                    lblFeil.Visible = false;
                    lbBrukere.Items.Add(selectedUser);
                }
           
        }


    }
}