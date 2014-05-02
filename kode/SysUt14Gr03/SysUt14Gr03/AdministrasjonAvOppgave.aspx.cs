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
        private String oppgaveTittel;

        protected void Page_Load()
        {
            if (Session["loggedIn"] == null)
                     Response.Redirect("Login.aspx", true);                

            
            if (!Page.IsPostBack)
            {
                visStatus = Queries.GetAlleStatuser();
                brukerListe = Queries.GetAlleAktiveBrukere();
                prosjektListe = Queries.GetAlleAktiveProsjekter();
                pri = Queries.GetAllePrioriteringer();
                oppgaveID = Classes.Validator.KonverterTilTall(Request.QueryString["oppgave_id"]);
                endres = Queries.GetOppgave(oppgaveID);
                oppgaveTittel = endres.Tittel.ToString();

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
                for (int i = 0; i < endres.Brukere.Count; i++)
                {
                    Bruker bruker = endres.Brukere[i];
                    lbBrukere.Items.Add(new ListItem(endres.Brukere[i].ToString(), bruker.Bruker_id.ToString()));
                }

                cbAktiv.Checked = endres.Aktiv;
                tbBeskrivelse.Text = endres.UserStory;
                TbEstimering.Text = endres.Estimat.ToString();
                tbKrav.Text = endres.Krav;
                tbBruktTid.Text = endres.BruktTid.ToString();
                tbRemainingTime.Text = endres.RemainingTime.ToString();
                tbTidsfristStart.Text = endres.Opprettet.ToString();
                tbTidsfristSlutt.Text = endres.Tidsfrist.ToString();
                tbID.Text = endres.RefOppgaveId;
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
                    oppgaveID = Classes.Validator.KonverterTilTall(Request.QueryString["oppgave_id"]);
                    endres = context.Oppgaver
                                  .Include("Brukere")
                                  .Include("Prioritering")
                                  .Include("Status")
                                  .Include("Prosjekt")
                                  .Where(oppgave => oppgave.Oppgave_id == oppgaveID)
                                  .Where(oppgave => oppgave.Aktiv == true)
                                  .FirstOrDefault();

                    int priorietring_id = Convert.ToInt32(ddlPrioritet.SelectedValue);
                    int prosjekt_id = Convert.ToInt32(ddlProsjekt.SelectedValue);
                    TimeSpan estimering = TimeSpan.Parse(TbEstimering.Text);
                    int status_id = Convert.ToInt32(ddlStatus.SelectedValue);
                    Prioritering pri = context.Prioriteringer.FirstOrDefault(id => id.Prioritering_id == priorietring_id);
                    Prosjekt pro = context.Prosjekter.FirstOrDefault(id => id.Prosjekt_id == prosjekt_id);
                    Status sta = context.Statuser.FirstOrDefault(id => id.Status_id == status_id);

                    foreach (ListItem s in lbBrukere.Items)
                    {
                        int navn = Convert.ToInt32(s.Value);
                        Bruker bruk = context.Brukere.Where(b => b.Bruker_id == navn).First();
                        selectedBruker.Add(bruk);
                    }

                    if (endres != null)
                    {
                        endres.Krav = tbKrav.Text;
                        endres.Oppdatert = DateTime.Now;
                        endres.Prioritering = pri;
                        endres.Prosjekt = pro;
                        endres.Status = sta;
                        endres.Tittel = tbTittel.Text;
                        endres.RefOppgaveId = tbID.Text;
                        endres.UserStory = tbBeskrivelse.Text;
                        endres.Aktiv = cbAktiv.Checked;
                        endres.Brukere = selectedBruker;
                        endres.Estimat = estimering;
                        endres.BruktTid = TimeSpan.Parse(tbBruktTid.Text);
                        endres.RemainingTime = TimeSpan.Parse(tbRemainingTime.Text);
                        endres.Tidsfrist = Convert.ToDateTime(tbTidsfristSlutt.Text);
                    }

                    try {
                        context.SaveChanges();
                        //legger til logg for endring og arkivering av oppgave
                        String hendelse;
                        if (!endres.Aktiv)
                        {
                            hendelse = "Oppgave " + oppgaveTittel + "ble endret";
                            if (oppgaveTittel != endres.Tittel)
                                hendelse = hendelse + ". Nytt navn på oppgaven er " + endres.Tittel;
                        }
                        else
                            hendelse = "Oppgave " + oppgaveTittel + "ble arkivert";

                        OppretteLogg.opprettLoggForBruker(hendelse, DateTime.Now, (int)Session["bruker_id"]);
                        Response.Redirect("FerdigstillelsAvOppgave.aspx");
                    }
                    catch (System.Data.Entity.Infrastructure.DbUpdateException ex) {
                        lblCheck.Visible = true;
                        lblCheck.ForeColor = Color.Red;
                        lblCheck.Text = ex.Message + "\n" + ex.InnerException.Message;
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
            if (lbBrukere.Items.Count > 0 && lbBrukere.SelectedIndex >= 0)
            {
                for (int i = 0; i < lbBrukere.Items.Count; i++)
                {
                    if (lbBrukere.Items[i].Selected)
                    {
                        lbBrukere.Items.Remove(lbBrukere.Items[i]);
                        i--;
                    }
                }
            }
        }

        protected void btnSlutt_Click(object sender, EventArgs e)
        {
            tbTidsfristSlutt.Text = cal.SelectedDate.ToShortDateString();
        }

        protected void tbBruktTid_TextChanged(object sender, EventArgs e)
        {
            oppgaveID = Classes.Validator.KonverterTilTall(Request.QueryString["oppgave_id"]);
            endres = Queries.GetOppgave(oppgaveID);
            if (endres.BruktTid != null)
            {
                float est = float.Parse(TbEstimering.Text);
                float bru = float.Parse(tbBruktTid.Text);
                float sum = est - bru;
                tbRemainingTime.Text = sum.ToString();
            }
        }
    }
}