﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SysUt14Gr03.Classes;
using SysUt14Gr03.Models;

namespace SysUt14Gr03
{
    public partial class Timeregistrering : System.Web.UI.Page
    {
        private List<Oppgave> oppgaver;
        private int brukerID;
        private int prosjektID;
        private int oppgaveID;
        private int PauseTeller;

        private TimeSpan bruktTid;
        private DateTime Start;
        private DateTime Stopp;
        private List<DateTime> PauseStartObjekter;
        private List<DateTime> PauseSluttObjekter;
        private List<int> PauseTellerList;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["loggedIn"] == null)
                Response.Redirect("Login.aspx", true);
            SessionSjekk.sjekkForRettighetPaaInnloggetBruker(Konstanter.rettighet.Prosjektleder);

            if (!IsPostBack)
            {
                PauseTeller = 0;
                oppgaveID = Classes.Validator.KonverterTilTall(Request.QueryString["oppgave_id"]);
                prosjektID = Classes.Validator.KonverterTilTall(Request.QueryString["prosjekt_id"]);
                brukerID = Classes.Validator.KonverterTilTall(Request.QueryString["bruker_id"]);
                oppgaver = Queries.GetAlleAktiveOppgaverForProsjektOgBruker(prosjektID, brukerID);
                btnStop.Enabled = false;
                btnPause.Enabled = false;
                btnRegistrer.Enabled = false;

                for (int i = 0; i < oppgaver.Count; i++)
                    ddlOppgaver.Items.Add(new ListItem(oppgaver[i].Tittel, oppgaver[i].Oppgave_id.ToString()));
            }
        }
        public void btnStart_Click(object sender, EventArgs e)
        {
            String registrert = tbTidsregistrert.Text;
            if(String.IsNullOrEmpty(registrert))
            {
                Start = DateTime.Now;
                ViewState["Start"] = Start;
                tbTidsregistrert.Text += "Tid takingen startet: " + Start + "\n";
                btnSnart.Enabled = false;
                btnStop.Enabled = true;
                btnPause.Enabled = true;
            }
            else
            {
                PauseSluttObjekter = new List<DateTime>();
                if(ViewState["StartEtterPause"] != null)
                    PauseSluttObjekter = (List<DateTime>)ViewState["StartEtterPause"];

                DateTime StartEtterPause = DateTime.Now;
                PauseSluttObjekter.Add(StartEtterPause);
                ViewState["StartEtterPause"] = PauseSluttObjekter;
                
                tbTidsregistrert.Text += "Tid takingen startet etter pause: " + StartEtterPause + "\n";
                btnSnart.Enabled = false;
                btnPause.Enabled = true;
                btnStop.Enabled = true;
            }
        }

        protected void btnPause_Click(object sender, EventArgs e)
        {
            PauseTellerList = new List<int>();
            if (ViewState["PauseTeller"] != null)
                PauseTellerList = (List<int>)ViewState["PauseTeller"];
            PauseStartObjekter = new List<DateTime>();
            if (ViewState["Pause"] != null)
                PauseStartObjekter = (List<DateTime>)ViewState["Pause"];

            PauseTeller += 1;
            PauseTellerList.Add(PauseTeller);
            ViewState["PauseTeller"] = PauseTellerList;
            DateTime Pause = DateTime.Now;
            PauseStartObjekter.Add(Pause);
            ViewState["Pause"] = PauseStartObjekter;

            tbTidsregistrert.Text += "Tid takingen pauset: " + Pause + "\n";
            btnPause.Enabled = false;
            btnSnart.Enabled = true;
        }

        protected void btnStop_Click(object sender, EventArgs e)
        {
            Start = (DateTime)ViewState["Start"];
            if (ViewState["PauseTeller"] != null)
            {
                PauseSluttObjekter = (List<DateTime>)ViewState["StartEtterPause"];
                PauseStartObjekter = (List<DateTime>)ViewState["Pause"];
                PauseTellerList = (List<int>)ViewState["PauseTeller"];
            }
            Stopp = DateTime.Now;
            ViewState["Stopp"] = Stopp;
            tbTidsregistrert.Text += "Tid takingen stoppet: " + Stopp + "\n";
            btnPause.Enabled = false;
            btnSnart.Enabled = false;
            btnStop.Enabled = false;
            btnRegistrer.Enabled = true;
            if (Start != DateTime.MinValue && Stopp != DateTime.MinValue)
            {
                TimeSpan pauser = new TimeSpan();
                bruktTid = Stopp - Start;
                if (PauseTellerList != null)
                {
                    for (int i = 0; i < PauseTellerList.Count; i++)
                    {
                        if (PauseStartObjekter[i] != DateTime.MinValue && PauseSluttObjekter[i] != DateTime.MinValue)
                        {
                            pauser += PauseSluttObjekter[i] - PauseStartObjekter[i];
                        }
                    }
                    bruktTid -= pauser;
                }
                ViewState["bruktTid"] = bruktTid;
            }
            tbTidsregistrert.Text += String.Format("Tid brukt totalt på oppgaven: {0:D2}:{1:D2}:{2:D2}", bruktTid.Hours, bruktTid.Minutes, bruktTid.Seconds);
        }
        protected void btnRegistrer_Click(object sender, EventArgs e)
        {
            string kommentar = tbKommentar.Text;
            if(!String.IsNullOrEmpty(kommentar))
            {
                bruktTid = (TimeSpan)ViewState["bruktTid"];
                TimeSpan timespan = new TimeSpan(bruktTid.Hours, bruktTid.Minutes, bruktTid.Seconds);
                Start = (DateTime)ViewState["Start"];
                Stopp = (DateTime)ViewState["Stopp"];
                double time = bruktTid.TotalHours;
                float tid = (float)time;
                float rounded = (float)(Math.Round((double)tid, 2));
                using (var context = new Context())
                {
                    oppgaveID = Classes.Validator.KonverterTilTall(Request.QueryString["oppgave_id"]);
                    brukerID = Classes.Validator.KonverterTilTall(Request.QueryString["bruker_id"]);

                    Oppgave oppgave = context.Oppgaver.Where(o => o.Oppgave_id == oppgaveID).FirstOrDefault();
                    Bruker bruker = context.Brukere.Where(b => b.Bruker_id == brukerID).FirstOrDefault();

                    oppgave.BruktTid = rounded;
                    oppgave.RemainingTime = oppgave.Estimat - tid;
                    oppgave.Oppdatert = DateTime.Now;

                    var Kommentar = new Kommentar
                    {
                        Tekst = kommentar,
                        Aktiv = true,
                        Opprettet = DateTime.Now,
                        Bruker_id = brukerID,
                        Oppgave_id = oppgaveID,
                        Bruker = bruker,
                        Oppgave = oppgave
                    };
                    
                    var times = new Time
                    {
                        Tid = timespan,
                        Opprettet = DateTime.Now,
                        Aktiv = true,
                        Bruker = bruker,
                        Oppgave = oppgave,
                        Start = Start,
                        Stopp = Stopp
                    };

                    context.Kommentarer.Add(Kommentar);
                    context.Timer.Add(times);
                    context.SaveChanges();
                }
            }
            else
            {
                Debug.WriteLine("Du må legge til en kommentar på oppgaven. Kommentere det du har gjort.");
            }
        }
    }
}