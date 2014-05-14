﻿using System;
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

/// <summary>
/// Denne webformen tar for seg det administrering av en oppgave. Her kan du endre alle egenskapene til en oppgave. Det er kune faseeleder og prosjekt leder
/// som har tilgang hit. 
/// </summary>

namespace SysUt14Gr03
{
    public partial class AdministrasjonAvOppgave : System.Web.UI.Page
    {
        private List<Bruker> brukerListe;
        private List<Prosjekt> prosjektListe;
        private List<Prioritering> pri;
        private List<Status> visStatus;
        private List<Oppgave> oppgaver;
        private List<int> valgtBrukerid = new List<int>();
        private Oppgave endres;
        private int oppgaveID;
        private int prosjektID;
        private String oppgaveTittel;

        protected void Page_PreInit(Object sener, EventArgs e)
        {
            string master = SessionSjekk.findMaster();
            this.MasterPageFile = master;
        }

        /// <summary>
        /// Her sjekker den om du er en faseleder eller prosjektleder. Den sjekker om du får inn en oppgave id via URL stringen eller ikke. 
        /// Viss du ikke får det så, så vil den ikke fylle ut noe som helst og alle feltene på denne siden vil være tom. Du må da velge en oppgave
        /// fra drop down lista som er øverst.
        /// Viss du får en oppgave id med i URL stringen vil den da velge den rette oppgaven i drop down listen som er øverst, for så å fylle ut alle
        /// resterende felt på denne siden.
        /// </summary>
        
        protected void Page_Load()
        {
            SessionSjekk.sjekkForBruker_id();
            SessionSjekk.sjekkForProsjekt_id();

            if (!Page.IsPostBack)
            {
                if (SessionSjekk.IsFaseleder())
                {
                    if (Request.QueryString["oppgave_id"] != null)
                    {
                        oppgaveID = Classes.Validator.KonverterTilTall(Request.QueryString["oppgave_id"]);
                        prosjektID = Classes.Validator.KonverterTilTall(Session["prosjekt_id"].ToString());
                        oppgaver = Queries.GetOppgaveIProsjekt(prosjektID);
                        for (int i = 0; i < oppgaver.Count; i++)
                            ddlOppgaverIProsjekt.Items.Add(new ListItem(oppgaver[i].Tittel, oppgaver[i].Oppgave_id.ToString()));
                        ddlOppgaverIProsjekt.SelectedValue = oppgaveID.ToString();
                        if (ddlOppgaverIProsjekt.SelectedValue != null)
                            this.page();
                    }
                    else
                    {
                        prosjektID = Classes.Validator.KonverterTilTall(Session["prosjekt_id"].ToString());
                        oppgaver = Queries.GetOppgaveIProsjekt(prosjektID);
                        for (int i = 0; i < oppgaver.Count; i++)
                            ddlOppgaverIProsjekt.Items.Add(new ListItem(oppgaver[i].Tittel, oppgaver[i].Oppgave_id.ToString()));
                        if (ddlOppgaverIProsjekt.SelectedValue != null)
                            this.page();
                    }
                }
                else
                {
                    SessionSjekk.sjekkForRettighetPaaInnloggetBruker(Konstanter.rettighet.Prosjektleder);
                    if (Request.QueryString["oppgave_id"] != null)
                    {
                        oppgaveID = Classes.Validator.KonverterTilTall(Request.QueryString["oppgave_id"]);
                        prosjektID = Classes.Validator.KonverterTilTall(Session["prosjekt_id"].ToString());
                        oppgaver = Queries.GetOppgaveIProsjekt(prosjektID);
                        for (int i = 0; i < oppgaver.Count; i++)
                            ddlOppgaverIProsjekt.Items.Add(new ListItem(oppgaver[i].Tittel, oppgaver[i].Oppgave_id.ToString()));
                        ddlOppgaverIProsjekt.SelectedValue = oppgaveID.ToString();
                        if (ddlOppgaverIProsjekt.SelectedValue != null)
                            this.page();
                    }
                    else
                    {
                        prosjektID = Classes.Validator.KonverterTilTall(Session["prosjekt_id"].ToString());
                        oppgaver = Queries.GetOppgaveIProsjekt(prosjektID);
                        for (int i = 0; i < oppgaver.Count; i++)
                            ddlOppgaverIProsjekt.Items.Add(new ListItem(oppgaver[i].Tittel, oppgaver[i].Oppgave_id.ToString()));
                        if (ddlOppgaverIProsjekt.SelectedValue != null)
                            this.page();
                    }
                }
            }
        }

        /// <summary>
        /// Denne som fyller ut automatisk om informasjon om oppgaven som brukeren har valgt med rett verdi.
        /// </summary>
        
        private void page()
        {
            visStatus = Queries.GetAlleStatuser();
            brukerListe = Queries.GetAlleAktiveBrukere();
            prosjektListe = Queries.GetAlleAktiveProsjekter();
            pri = Queries.GetAllePrioriteringer();
            oppgaveID = Classes.Validator.KonverterTilTall(ddlOppgaverIProsjekt.SelectedValue);
            endres = Queries.GetOppgave(oppgaveID);
            oppgaveTittel = endres.Tittel.ToString();

            for (int i = 0; i < visStatus.Count; i++)
                ddlStatus.Items.Add(new ListItem(visStatus[i].Navn, visStatus[i].Status_id.ToString()));

            for (int i = 0; i < brukerListe.Count; i++)
                ddlBrukere.Items.Add(new ListItem(brukerListe[i].ToString(), brukerListe[i].Bruker_id.ToString()));

            for (int i = 0; i < prosjektListe.Count; i++)
                ddlProsjekt.Items.Add(new ListItem(prosjektListe[i].Navn, prosjektListe[i].Prosjekt_id.ToString()));

            for (int i = 0; i < pri.Count; i++)
                ddlPrioritet.Items.Add(new ListItem(pri[i].Navn, pri[i].Prioritering_id.ToString()));

            for (int i = 0; i < endres.Brukere.Count; i++)
                lbBrukere.Items.Add(new ListItem(endres.Brukere[i].Brukernavn.ToString(), endres.Brukere[i].Bruker_id.ToString()));

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

        /// <summary>
        /// Her hente man ut all informasjonen som står i de forskjellige feltene og legger dem inn i databasen.
        /// </summary>
        
        private void EndreOppg()
        {
            List<Bruker> selectedBruker = new List<Bruker>();
            if (tbKrav.Text != String.Empty && tbTittel.Text != String.Empty && tbBeskrivelse.Text != String.Empty && TbEstimering.Text != String.Empty)
            {
                using (var context = new Context())
                {
                    oppgaveID = Classes.Validator.KonverterTilTall(ddlOppgaverIProsjekt.SelectedValue);
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
                        Session["flashMelding"] = ex.Message + "\n" + ex.InnerException.Message;
                        Session["flashStatus"] = Konstanter.notifikasjonsTyper.danger;
                    }
                }
            }
            else
            {
                Session["flashMelding"] = "Feltene kan ikke være tomme!";
                Session["flashStatus"] = Konstanter.notifikasjonsTyper.danger;
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
                Session["flashMelding"] = selectedUser + " er allerede valgt!";
                Session["flashStatus"] = Konstanter.notifikasjonsTyper.danger;
            }
            else
            {
                lbBrukere.Items.Add(selectedUser);
            }
        }
        protected void btnSlettBrukere_Click(object sender, EventArgs e)
        {
            lbBrukere.Items.Remove(lbBrukere.SelectedItem);
        }

        protected void btnSlutt_Click(object sender, EventArgs e)
        {
            tbTidsfristSlutt.Text = cal.SelectedDate.ToShortDateString();
        }

        /// <summary>
        /// Lagt til et text change event på brukt tid textboxsen så man skal slippe å fylle ut resterende tid text boksen. 
        /// Også lagt inn en sjekk at verdien i text boksen ikke kan være under 0. 
        /// </summary>
        
        protected void tbBruktTid_TextChanged(object sender, EventArgs e)
        {
            oppgaveID = Classes.Validator.KonverterTilTall(ddlOppgaverIProsjekt.SelectedValue);
            endres = Queries.GetOppgave(oppgaveID);
            if (endres.BruktTid != null)
            {
                TimeSpan est = TimeSpan.Parse(TbEstimering.Text);
                TimeSpan bru = TimeSpan.Parse(tbBruktTid.Text);
                TimeSpan sum = est - bru;
                if (sum > new TimeSpan(0))
                    tbRemainingTime.Text = sum.ToString();
                else
                    tbRemainingTime.Text = new TimeSpan(0).ToString();
            }
        }

        /// <summary>
        /// Lagt inn et selcted index change event på drop down listen for oppgaven. Den som styre hvilken hva som skal bli fylt ut i de resterende feltene 
        /// på siden. Når denne drop down listen blir endret, så blir siden lasta inn på nytt med de resterende felte med verdier fra den nye oppgaven som er valgt.
        /// </summary>
        
        protected void ddlOppgaverIProsjekt_SelectedIndexChanged(object sender, EventArgs e)
        {
            prosjektID = Classes.Validator.KonverterTilTall(Session["prosjekt_id"].ToString());
            oppgaver = Queries.GetOppgaveIProsjekt(prosjektID);
            
            for (int i = 0; i < oppgaver.Count; i++)
                ddlOppgaverIProsjekt.Items.Add(new ListItem(oppgaver[i].Tittel, oppgaver[i].Oppgave_id.ToString()));
            
            this.page();
        }
    }
}