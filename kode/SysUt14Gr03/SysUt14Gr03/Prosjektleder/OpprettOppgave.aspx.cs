﻿using System;
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
        private List<Prioritering> pri;
        private List<Status> visStatus;
        private List<int> valgtBrukerid = new List<int>();
        private int prosjekt_id = -1;
        private DateTime tidsfrist;
        private int bruker_id = -1;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["bruker_id"] != null)
                {
                    bruker_id = Validator.KonverterTilTall(Session["bruker_id"].ToString());
                }
                else
                {
                    Response.Redirect("~/Login.aspx", true);
                }

                if (Request.QueryString["prosjekt_id"] != null)
                {
                prosjekt_id = Validator.KonverterTilTall(Request.QueryString["prosjekt_id"]);
                }
                else if (Session["prosjekt_id"] != null)
                {
                prosjekt_id = Validator.KonverterTilTall(Session["prosjekt_id"].ToString());
                }

                if (prosjekt_id != -1 && bruker_id != -1) {
                    Prosjekt prosjekt = Queries.GetProsjekt(prosjekt_id);

                    // Sjekk om prosjektleder er prosjektleder for valgt prosjekt
                    if (prosjekt.Bruker_id == bruker_id)
                    {

                        brukerListe = Queries.GetAlleAktiveBrukere();
                        string prosjektNavn = Queries.GetProsjekt(prosjekt_id).Navn;
                        lblProsjekt.Text = prosjektNavn;
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
                            bindingSource1.DataSource = context.Oppgaver.Where(o => o.Prosjekt_id == prosjekt_id).ToList<Oppgave>();
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
                    else
                    {
                        Session["flashMelding"] = "Du har valgt et ikke gyldig prosjekt, prøv igjen med et annet prosjekt";
                        Session["flashStatus"] = Konstanter.notifikasjonsTyper.danger.ToString();
                        Response.Redirect("~/Prosjektleder/DefaultProsjektleder", true);
                    }
                }
                else
                {
                    Session["flashMelding"] = "Du må velge et prosjekt!";
                    Session["flashStatus"] = Konstanter.notifikasjonsTyper.danger.ToString();
                    Response.Redirect("~/Prosjektleder/DefaultProsjektleder", true);
                }  
            }
        }
        private void OpprettOppg()
        {
            List<Bruker> selectedBruker = new List<Bruker>();
            if (tbKrav.Text != String.Empty && tbTittel.Text != String.Empty && tbBeskrivelse.Text != String.Empty && TbEstimering.Text != String.Empty && tbFrist.Text != String.Empty)
            {

            
            using (var context = new Context())
            {

                int priorietring_id = Convert.ToInt32(ddlPrioritet.SelectedValue);
                float estimering = Convert.ToInt16(TbEstimering.Text);
                int status_id = Convert.ToInt32(ddlStatus.SelectedValue);
                tidsfrist = Convert.ToDateTime(tbFrist.Text);
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
                    Prioritering_id = priorietring_id,
                    RemainingTime = estimering,
                    Tidsfrist = tidsfrist
                    
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

        protected void btnSett_Click(object sender, EventArgs e)
        {
            tbFrist.Text = cal.SelectedDate.ToShortDateString();
        }


    }
}