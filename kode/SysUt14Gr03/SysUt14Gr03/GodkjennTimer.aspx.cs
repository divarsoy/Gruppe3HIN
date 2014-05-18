﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SysUt14Gr03.Models;
using SysUt14Gr03.Classes;
using System.Windows.Forms;

namespace SysUt14Gr03
{
    /// <summary>
    /// Dette er siden som viser alle timeregistreringer i valgt prosjekt som trenger godkjenning.
    /// Faseleder kan trykke på "godkjenn" for å godkjenne timen, eller "korriger" for å
    /// bli sendt videre til en side der han kan korrigere tidsregistreringen.
    /// </summary>
    public partial class GodkjennTimer : System.Web.UI.Page
    {
        private int bruker_id = 1;
        private int prosjekt_id;
        private Bruker bruker;
        private List<Time> timeListe;

        protected void Page_PreInit(Object sener, EventArgs e)
        {
            string master = SessionSjekk.findMaster();
            this.MasterPageFile = master;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            SessionSjekk.sjekkForBruker_id();
            bruker_id = Validator.KonverterTilTall(Session["bruker_id"].ToString());

            SessionSjekk.sjekkForProsjekt_id();
            prosjekt_id = Validator.KonverterTilTall(Session["prosjekt_id"].ToString());

            Prosjekt prosjekt = Queries.GetProsjekt(prosjekt_id);
            lblTittel.Text = "Timer som må godkjennes i prosjekt " + prosjekt.Navn;

            if (bruker_id != -1)
            {

                bruker = Queries.GetBruker(bruker_id);

                List<Oppgave> oppgaveListe = Queries.GetAlleAktiveOppgaverForProsjekt(prosjekt_id);
                timeListe = new List<Time>();
                List<string> infoListe = new List<string>();

                foreach (Oppgave o in oppgaveListe)
                {
                    timeListe.AddRange(Queries.GetTimerForGodkjenning(o.Oppgave_id));
                }

                // Skriver ut alle timene til en gridview
                foreach (Time t in timeListe)
                {
                    infoListe.Add("Bruker: " + t.Bruker.ToString()
                        + " | dato: " + t.Opprettet.ToShortDateString()
                        + " | starttidspunkt: " + ((DateTime)t.Start).ToShortTimeString()
                        + " | sluttidspunkt: " + ((DateTime)t.Stopp).ToShortTimeString()
                        + " | varighet: " + ((int)t.Tid.TotalHours) + "t " + t.Tid.Minutes + "m");
                }

                if (infoListe.Count == 0)
                {
                    lblInfo.Text = "Ingen elementer";
                    lblInfo.Visible = true;
                }

                if (!IsPostBack)
                {
                    BindingSource bindingsource = new BindingSource();
                    bindingsource.DataSource = infoListe;
                    gvwTimer.DataSource = bindingsource;
                    gvwTimer.DataBind();
                }
            }
        }

        protected void gvwTimer_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            // just another day in the life of jimmy nutrin
        }


        // http://msdn.microsoft.com/en-us/library/vstudio/bb907626(v=vs.100).aspx
        // Kalles når man trykker på en knapp i gridview
        protected void gvwTimer_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            int index = Convert.ToInt32(e.CommandArgument);

            if (e.CommandName == "Godkjenn")
            {
                int time_id = timeListe[index].Time_id;
                // Godkjenn timen
                using (var context = new Context())
                {
                    Time time = context.Timer.Where(t => t.Time_id 
                        == time_id).FirstOrDefault();
                    Oppgave oppgave = time.Oppgave;
                    // Oppdaterer oppgavens brukte tid
                    oppgave.BruktTid += time.Tid;
                    if (oppgave.RemainingTime >= time.Tid)
                        oppgave.RemainingTime -= time.Tid;
                    else
                        oppgave.RemainingTime = new TimeSpan(0);

                    time.IsFerdig = true;
                    context.SaveChanges();
                }
                Session["flashMelding"] = "Timeregistrering godkjent";
                Session["flashStatus"] = Konstanter.notifikasjonsTyper.info.ToString();
                Response.Redirect(Request.RawUrl, true);

            }

            if (e.CommandName == "Korriger")
            {
                // Send til KorrigerRegistrerteTimer          
                string url = "~/Prosjektleder/KorrigerRegistrerteTimer?time_id=";
                url += timeListe[index].Time_id;
                Response.Redirect(url, true);

            }
        }
    }
}