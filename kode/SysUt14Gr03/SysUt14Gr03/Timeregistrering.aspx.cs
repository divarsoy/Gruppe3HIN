using System;
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
        private List<Pause> pauser;
        private Pause pause;
        private Time timer;
        private int brukerID;
        private int prosjektID;
        private int oppgaveID;
        private int PauseTeller;
        private int pauseID;
        private int timeID;

        private TimeSpan bruktTid;
        private DateTime Start;
        private DateTime Stopp;
        private List<DateTime> PauseStartObjekter;
        private List<DateTime> PauseSluttObjekter;
        private List<int> PauseTellerList;

        protected void Page_Load(object sender, EventArgs e)
        {
            SessionSjekk.sjekkForProsjekt_id();

            if (!IsPostBack)
            {
                lblTimeregistrering.Text = string.Format("Timeregistrering for prosjekt: {0}", Session["prosjektNavn"]);
                PauseTeller = 0;
                if (Session["time_id"] != null)
                {
                    timeID = Classes.Validator.KonverterTilTall(Session["time_id"].ToString());
                    timer = Queries.GetTimer(timeID);
                    
                    if (timer.IsFerdig != true)
                    {
                        pauser = Queries.GetPauseMedTimeID(timeID);
                        this.GetTimers(pauser, timeID);
                    }
                }
                else
                {
                    btnStop.Enabled = false;
                    btnPause.Enabled = false;
                }

                prosjektID = Classes.Validator.KonverterTilTall(Session["prosjekt_id"].ToString());
                brukerID = Classes.Validator.KonverterTilTall(Session["bruker_id"].ToString());
                oppgaver = Queries.GetAlleAktiveOppgaverForProsjektOgBruker(prosjektID, brukerID);
                btnRegistrer.Enabled = false;

                for (int i = 0; i < oppgaver.Count; i++)
                    ddlOppgaver.Items.Add(new ListItem(oppgaver[i].Tittel, oppgaver[i].Oppgave_id.ToString()));
            }
        }
        public void btnStart_Click(object sender, EventArgs e)
        {
            if (ddlOppgaver.SelectedItem != null)
            {
                String registrert = tbTidsregistrert.Text;
                if (String.IsNullOrEmpty(registrert))
                {
                    Start = DateTime.Now;
                    ViewState["Start"] = Start;
                    tbTidsregistrert.Text += "Tid takingen startet: " + Start + "\n";
                    btnSnart.Enabled = false;
                    btnStop.Enabled = true;
                    btnPause.Enabled = true;

                    using (var context = new Context())
                    {
                        oppgaveID = Convert.ToInt32(ddlOppgaver.SelectedValue);
                        brukerID = Classes.Validator.KonverterTilTall(Session["bruker_id"].ToString());
                        Oppgave oppgave = context.Oppgaver.Where(o => o.Oppgave_id == oppgaveID).FirstOrDefault();
                        Bruker bruker = context.Brukere.Where(b => b.Bruker_id == brukerID).FirstOrDefault();

                        timer = new Time()
                        {
                            Start = Start,
                            Opprettet = DateTime.Now,
                            Aktiv = true,
                            Manuell = false,
                            IsFerdig = false,
                            Oppgave_id = oppgave.Oppgave_id,
                            Oppgave = oppgave,
                            Bruker_id = brukerID,
                            Bruker = bruker
                        };

                        context.Timer.Add(timer);
                        context.SaveChanges();

                        timer = Queries.GetTimerMedOppgaveIDBoolean(oppgave.Oppgave_id, false);
                        Session["time_id"] = timer.Time_id;
                    }
                }
                else
                {
                    PauseSluttObjekter = new List<DateTime>();
                    if (ViewState["StartEtterPause"] != null)
                        PauseSluttObjekter = (List<DateTime>)ViewState["StartEtterPause"];

                    DateTime StartEtterPause = DateTime.Now;
                    PauseSluttObjekter.Add(StartEtterPause);
                    ViewState["StartEtterPause"] = PauseSluttObjekter;

                    tbTidsregistrert.Text += "Tid takingen startet etter pause: " + StartEtterPause + "\n";
                    btnSnart.Enabled = false;
                    btnPause.Enabled = true;
                    btnStop.Enabled = true;

                    using (var context2 = new Context())
                    {
                        pauseID = Classes.Validator.KonverterTilTall(Session["pause_id"].ToString());
                        pause = context2.Pauser.Where(p => p.Pause_id == pauseID).FirstOrDefault();

                        pause.Stopp = StartEtterPause;
                        pause.IsFerdig = true;

                        context2.SaveChanges();
                    }
                }
            }
            else
            {
                Session["flashMelding"] = "Vennligst velg en oppgave";
                Session["flashStatus"] = Konstanter.notifikasjonsTyper.info.ToString();
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
            timeID = Classes.Validator.KonverterTilTall(Session["time_id"].ToString());

            using (var context1 = new Context())
            {
                oppgaveID = Convert.ToInt32(ddlOppgaver.SelectedValue);
                Oppgave oppgave = context1.Oppgaver.Where(o => o.Oppgave_id == oppgaveID).FirstOrDefault();
                timer = context1.Timer.Where(t => t.Time_id == timeID).FirstOrDefault();

                pause = new Pause()
                {
                    Start = Pause,
                    Oppgave_id = oppgave.Oppgave_id,
                    Oppgave = oppgave,
                    IsFerdig = false,
                    Time_id = timeID,
                    Time = timer
                };

                context1.Pauser.Add(pause);
                context1.SaveChanges();

                pause = Queries.GetPauseMedOppgaveID(oppgave.Oppgave_id, false);
                Session["pause_id"] = pause.Pause_id;
            }

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
            if (!String.IsNullOrEmpty(kommentar))
            {
                bruktTid = (TimeSpan)ViewState["bruktTid"];
                TimeSpan timespan = new TimeSpan(bruktTid.Hours, bruktTid.Minutes, bruktTid.Seconds);
                Stopp = (DateTime)ViewState["Stopp"];

                using (var context3 = new Context())
                {
                    oppgaveID = Convert.ToInt32(ddlOppgaver.SelectedValue);
                    brukerID = Classes.Validator.KonverterTilTall(Session["bruker_id"].ToString());
                    timeID = Classes.Validator.KonverterTilTall(Session["time_id"].ToString());

                    Oppgave oppgave = context3.Oppgaver.Where(o => o.Oppgave_id == oppgaveID).FirstOrDefault();
                    Bruker bruker = context3.Brukere.Where(b => b.Bruker_id == brukerID).FirstOrDefault();
                    timer = context3.Timer.Where(t => t.Time_id == timeID).FirstOrDefault();

                    oppgave.BruktTid += timespan;
                    if (oppgave.BruktTid > oppgave.Estimat)
                        oppgave.RemainingTime = new TimeSpan(0);
                    else
                        oppgave.RemainingTime = oppgave.Estimat - oppgave.BruktTid;
                    oppgave.Oppdatert = DateTime.Now;

                    timer.Tid = timespan;
                    timer.IsFerdig = true;
                    timer.Stopp = Stopp;

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

                    context3.Kommentarer.Add(Kommentar);
                    context3.SaveChanges();

                    Session["flashMelding"] = "Takk for din registrering på oppgave: " + oppgave.Oppgave_id;
                    Session["flashStatus"] = Konstanter.notifikasjonsTyper.success.ToString();
                    this.signOut();
                }
            }
            else
            {
                Session["flashMelding"] = "Du må legge til en kommentar på oppgaven. Kommentere det du har gjort.";
                Session["flashStatus"] = Konstanter.notifikasjonsTyper.info.ToString();
                Response.Redirect(Request.Url.ToString());
            }
        }
        private void GetTimers(List<Pause> Pause, int timeId)
        {
            PauseTeller = 0;
            PauseTellerList = new List<int>();
            PauseSluttObjekter = new List<DateTime>();
            PauseStartObjekter = new List<DateTime>();
            timer = Queries.GetTimer(timeId);
            tbTidsregistrert.Text += "Tid takingen startet: " + timer.Start + "\n";
            ddlOppgaver.SelectedValue = timer.Oppgave.ToString();
            ViewState["Start"] = timer.Start;
            for (int i = 0; i < Pause.Count; i++)
            {
                pause = Queries.GetPauseMedPauseID(Pause[i].Pause_id);

                if (pause != null)
                {
                    tbTidsregistrert.Text += "Tid takingen pauset: " + pause.Start + "\n";
                    if (pause.Stopp != null)
                        tbTidsregistrert.Text += "Tid takingen startet etter pause: " + pause.Stopp + "\n";
                }
                PauseTeller += 1;
                PauseTellerList.Add(PauseTeller);
                PauseStartObjekter.Add(pause.Start);
                if (pause.Stopp != null)
                {
                    PauseSluttObjekter.Add((DateTime)pause.Stopp);
                    btnPause.Enabled = true;
                    btnSnart.Enabled = false;
                    btnStop.Enabled = true;
                }
                else
                {
                    btnPause.Enabled = false;
                    btnSnart.Enabled = true;
                    btnStop.Enabled = false;
                }
            }
            ViewState["PauseTeller"] = PauseTellerList;
            ViewState["StartEtterPause"] = PauseSluttObjekter;
            ViewState["Pause"] = PauseStartObjekter;
        }
        private void signOut()
        {
            oppgaver = null;
            pauser = null;
            pause = null;
            timer = null;
            PauseTeller = 0;
            PauseStartObjekter = null;
            PauseSluttObjekter = null;
            PauseTellerList = null;
            tbKommentar.Text = "";
            tbTidsregistrert.Text = "";
            Session["time_id"] = null;
            ViewState["bruktTid"] = null;
            ViewState["Stopp"] = null;
            ViewState["Pause"] = null;
            ViewState["StartEtterPause"] = null;
            ViewState["PauseTeller"] = null;
            ViewState["Start"] = null;
            ViewState["bruktTid"] = null;
            Session["pause_id"] = null;
            Response.Redirect("Utvikler/InnsynIEgneRegistrerteTimerSomBruker.aspx", true);
        }
    }
}