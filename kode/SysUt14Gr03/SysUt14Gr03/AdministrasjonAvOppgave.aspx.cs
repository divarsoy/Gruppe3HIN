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
    public partial class AdministrasjonAvOppgave : System.Web.UI.Page
    {
        private List<Bruker> brukerListe;
        private List<Prosjekt> prosjektListe;
        private List<Prioritering> pri;
        private List<Status> visStatus;
        private List<int> valgtBrukerid = new List<int>();
        private Oppgave endres;
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
                oppgaveID = 6; //Classes.Validator.KonverterTilTall(tbOppgave_id.Text);
                endres = Queries.GetOppgave(6);

                for (int i = 0; i < visStatus.Count; i++)
                {
                    Status status = visStatus[i];
                    ddlStatus.Items.Add(new ListItem(status.Navn, status.Status_id.ToString()));
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
                cbAktiv.Checked = endres.Aktiv;
                for (int i = 0; i < endres.Brukere.Count; i++)
                {
                    Bruker bruker = endres.Brukere[i];
                    lbBrukere.Items.Add(new ListItem(endres.Brukere[i].ToString(), bruker.Bruker_id.ToString()));
                }
                tbBeskrivelse.Text = endres.UserStory;
                TbEstimering.Text = endres.Estimat.ToString();
                tbKrav.Text = endres.Krav;
                tbOppgave_id.Text = endres.Oppgave_id.ToString();
                tbRemainingTime.Text = endres.RemainingTime.ToString();
                tbTidsfristStart.Text = endres.Opprettet.ToString();
                tbTidsfristSlutt.Text = endres.Tidsfrist.ToString();
                tbTittel.Text = endres.Tittel;
                ddlPrioritet.SelectedIndex = Convert.ToInt32(endres.Prioritering.Navn) - 1;
                ddlProsjekt.SelectedIndex = endres.Prosjekt_id - 1;
                ddlStatus.SelectedIndex = endres.Status_id - 1;
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
                    oppgaveID = 6;
                    var oppgave = context.Oppgaver.First(id => id.Oppgave_id == oppgaveID);
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
                    oppgave.RemainingTime = float.Parse(tbRemainingTime.Text);
                    oppgave.Tidsfrist = Convert.ToDateTime(tbTidsfristSlutt.Text);

                    try
                    {
                        context.SaveChanges();
                        Response.Redirect("AdministrasjonAvOppgave.aspx");
                    }
                    catch (System.Data.Entity.Infrastructure.DbUpdateException ex)
                    {
                        lblFeil.Visible = true;
                        lblFeil.Text = ex.Message;
                    }
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

        protected void btnLeggTilBrukere_Click(object sender, EventArgs e)
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
        protected void btnSlettBrukere_Click(object sender, EventArgs e)
        {
            lblFeil.Visible = false;
            string t43 = lbBrukere.SelectedItem.Text;
            lbBrukere.Items.Remove(t43);
        }

        protected void btnSlutt_Click(object sender, EventArgs e)
        {
            tbTidsfristSlutt.Text = cal.SelectedDate.ToShortDateString();
        }
    }
}