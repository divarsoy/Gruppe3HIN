using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SysUt14Gr03.Classes;
using SysUt14Gr03.Models;

/// <summary>
/// Klasse for å opprette en oppgave for det valgte prosjektet. 
/// </summary>
namespace SysUt14Gr03
{
    public partial class OpprettOppgave : System.Web.UI.Page
    {
     
        private List<Bruker> brukerListe; //liste med brukere
        private List<Prioritering> pri; //liste med prioriteringer
        private List<Status> visStatus; //Status
        private List<int> valgtBrukerid = new List<int>();
        private int prosjekt_id = -1; //prosjekt_id til valgt prosjekt
        private DateTime tidsfrist;
        private int bruker_id = -1; //bruker id til prosjektleder

        //Sjekker etter rett masterfil
        protected void Page_PreInit(Object sener, EventArgs e)
        {
            string master = SessionSjekk.findMaster();
            this.MasterPageFile = master;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            SessionSjekk.sjekkForBruker_id();
            SessionSjekk.sjekkForProsjekt_id();

            bruker_id = Validator.KonverterTilTall(Session["bruker_id"].ToString());
            prosjekt_id = Validator.KonverterTilTall(Session["prosjekt_id"].ToString());

            if (Validator.SjekkRettighet(bruker_id, Konstanter.rettighet.Prosjektleder) || SessionSjekk.IsFaseleder())
            {
                if (!IsPostBack)
                {
                    if (prosjekt_id != -1 && bruker_id != -1)
                    {
                        Prosjekt prosjekt = Queries.GetProsjekt(prosjekt_id);
                        brukerListe = Queries.GetAlleBrukereIEtProjekt(prosjekt_id);

                        pri = Queries.GetAllePrioriteringer();
                        visStatus = Queries.GetAlleStatuser();
                        List<Fase> faseListe = Queries.GetFaseForProsjekt(prosjekt_id);
                        ddlFaser.Items.Add(new ListItem("Velg Fase", "0"));
                        foreach (Fase fase in faseListe)
                        {
                            ddlFaser.Items.Add(new ListItem(fase.Navn, fase.Fase_id.ToString()));
                        }
                        for (int i = 0; i < visStatus.Count; i++)
                        {
                            Status status = visStatus[i];
                            ddlStatus.Items.Add(new ListItem(status.Navn, status.Status_id.ToString()));
                        }
                        using (var context = new Context())
                        {
                            System.Windows.Forms.BindingSource bindingSource1 = new System.Windows.Forms.BindingSource();
                            bindingSource1.DataSource = context.Oppgaver
                                                        .Where(o => o.Prosjekt_id == prosjekt_id)
                                                        .OrderByDescending(oppgave => oppgave.Opprettet)
                                                        .ToList<Oppgave>();
                            GridViewOppg.DataSource = bindingSource1;
                            GridViewOppg.DataBind();
                        }
                        for (int i = 0; i < brukerListe.Count; i++)
                        {
                            Bruker bruker = brukerListe[i];
                            ddlBrukere.Items.Add(new ListItem(bruker.ToString(), bruker.Bruker_id.ToString()));
                        }
                        for (int i = 0; i < pri.Count; i++)
                        {
                            Prioritering priori = pri[i];
                            ddlPrioritet.Items.Add(new ListItem(priori.Navn, priori.Prioritering_id.ToString()));
                        }
                    }

                }
            }
        }
        private void OpprettOppg()
        {
            List<Bruker> selectedBruker = new List<Bruker>();
            if (tbTittel.Text != String.Empty && tbBeskrivelse.Text != String.Empty && TbEstimering.Text != String.Empty && ddlFaser.SelectedValue != "0")
            {   
            using (var context = new Context())
            {
                string oppgave_navn = tbTittel.Text;
                int priorietring_id = Convert.ToInt32(ddlPrioritet.SelectedValue);
                TimeSpan estimering = new TimeSpan(0,0,0);
                TimeSpan.TryParse(TbEstimering.Text, out estimering);
                int status_id = Convert.ToInt32(ddlStatus.SelectedValue);
                              
                foreach (ListItem s in lbBrukere.Items)
                {
                    int navn = Convert.ToInt32(s.Value);
                    Bruker bruk = context.Brukere.Where(b => b.Bruker_id == navn).First();
                    selectedBruker.Add(bruk);
                }
                prosjekt_id = Validator.KonverterTilTall(Session["prosjekt_id"].ToString());

                var oppgave = new Oppgave
                {
                    Krav = tbKrav.Text,
                    Opprettet = DateTime.Now,
                    Tittel = tbTittel.Text,
                    RefOppgaveId = tbID.Text,
                    Aktiv = true,
                    UserStory = tbBeskrivelse.Text,
                    Estimat = estimering,
                    Fase_id = Validator.KonverterTilTall(ddlFaser.SelectedValue),
                    Status_id = status_id,
                    Brukere = selectedBruker,
                    BruktTid = new TimeSpan(0, 0, 0),
                    Prosjekt_id = prosjekt_id,
                    Prioritering_id = priorietring_id,
                    RemainingTime = estimering,
                };

                if (!string.IsNullOrEmpty(tbFrist.Text)){
                    if (DateTime.TryParse(tbFrist.Text, out tidsfrist))
                    {
                        oppgave.Tidsfrist = tidsfrist;
                    }

                }

                context.Oppgaver.Add(oppgave);
                context.SaveChanges();
                Session["flashMelding"] = "Du har opprettet oppgaven: " + oppgave_navn;
                Session["flashStatus"] = Konstanter.notifikasjonsTyper.info.ToString();

                //Oppretter logg for oppretting av oppgave
                String hendelse = "Oppgave med navn " + oppgave_navn + " ble opprettet";
                OppretteLogg.opprettLoggForBruker(hendelse, DateTime.Now, (int)Session["bruker_id"]);
                Response.Redirect(Request.RawUrl);
            }
            }
            else
            {
                Session["flashMelding"] = "Feltene kan ikke være tomme";
                Session["flashStatus"] = Konstanter.notifikasjonsTyper.info.ToString();
                Response.Redirect(Request.RawUrl);
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

        protected void btnSett_Click(object sender, EventArgs e)
        {
            tbFrist.Text = cal.SelectedDate.ToShortDateString();
        }

        protected void btnFjernBruker_Click(object sender, EventArgs e)
        {
            lbBrukere.Items.Remove(lbBrukere.SelectedItem);
        }
    }
}