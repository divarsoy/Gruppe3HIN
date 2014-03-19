using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SysUt14Gr03.Classes;
using SysUt14Gr03.Models;

namespace SysUt14Gr03
{
    public partial class CancelOppg : System.Web.UI.Page
    {
        private List<Bruker> brukerListe;
        private List<Prosjekt> prosjektListe;
        private List<Prioritering> pri;
        private List<Status> visStatus;
        private List<int> valgtBrukerid = new List<int>();

        private int oppgaveID;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                brukerListe = Queries.GetAlleAktiveBrukere();
                prosjektListe = Queries.GetAlleAktiveProsjekter();
                pri = Queries.GetAllePrioriteringer();
                visStatus = Queries.GetAlleStatuser();
                tbOppgave_id.Text = Request.QueryString["oppgave_id"];
                oppgaveID = Classes.Validator.KonverterTilTall(tbOppgave_id.Text);
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
        private void EndreOppg()
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

                    var oppgave = context.Oppgaver.First(id => id.Oppgave_id = oppgaveID);
                    oppgave.Krav = tbKrav.Text;
                    oppgave.Oppdatert = DateTime.Now;
                    oppgave.Prioritering_id = priorietring_id;
                    oppgave.Prosjekt_id = prosjekt_id;
                    oppgave.Status_id = status_id;
                    oppgave.Tittel = tbTittel.Text;
                    oppgave.UserStory = tbBeskrivelse.Text;
                    oppgave.Aktiv = cbAktiv.Checked;
                    oppgave.Brukere = selectedBruker;
                    oppgave.Estimat = estimering;
                    oppgave.Tidsfrist;
                    oppgave.RemainingTime;
                    
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

        protected void btnEndre_Click(object sender, EventArgs e)
        {
            EndreOppg();
        }

        protected void btnBrukere_Click(object sender, EventArgs e)
        {
            ListItem selectedUser = ddlBrukere.SelectedItem;
            selectedUser.Selected = false;
            if (lbBrukere.Items.Contains(selectedUser))
            {
                lblFeil.Visible = true;
                lblFeil.ForeColor = Color.Red;
                lblFeil.Text = selectedUser + " er allerede valgt!";
            }
            else
            {
                lblFeil.Visible = false;
                lbBrukere.Items.Add(selectedUser);
            }
        }
    }
}