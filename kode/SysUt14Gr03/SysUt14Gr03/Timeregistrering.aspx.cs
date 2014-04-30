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
            //if (Session["loggedIn"] == null)
            //    Response.Redirect("Login.aspx", true);

            if (!IsPostBack)
            {
                PauseTeller = 0;
                if (Request.QueryString["pause_id"] != null && Request.QueryString["time_id"] != null)
                {
                    pauseID = Classes.Validator.KonverterTilTall(Request.QueryString["pause_id"]);
                    timeID = Classes.Validator.KonverterTilTall(Request.QueryString["time_id"]);
                    this.GetTimers(pauseID, timeID);
                }
                prosjektID = 2;// Classes.Validator.KonverterTilTall(Request.QueryString["prosjekt_id"]);
                brukerID = 4;// Classes.Validator.KonverterTilTall(Request.QueryString["bruker_id"]);
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
                        brukerID = 4;// Classes.Validator.KonverterTilTall(Request.QueryString["bruker_id"]);
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
                        pauseID = Classes.Validator.KonverterTilTall(Request.QueryString["pause_id"]);
                        pause = context2.Pauser.Where(p => p.Pause_id == pauseID).FirstOrDefault();

                        pause.Stopp = StartEtterPause;

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

            if (Request.QueryString["pause_id"] == null)
            {
                using (var context1 = new Context())
                {
                    oppgaveID = Convert.ToInt32(ddlOppgaver.SelectedValue);
                    Oppgave oppgave = context1.Oppgaver.Where(o => o.Oppgave_id == oppgaveID).FirstOrDefault();

                    pause = new Pause()
                    {
                        Start = Pause,
                        Oppgave_id = oppgave.Oppgave_id,
                        Oppgave = oppgave
                    };

                    context1.Pauser.Add(pause);
                    context1.SaveChanges();

                    pause = Queries.GetPauseMedOppgaveID(oppgave.Oppgave_id);
                    Session["pause_id"] = pause.Pause_id;
                }

                tbTidsregistrert.Text += "Tid takingen pauset: " + Pause + "\n";
                btnPause.Enabled = false;
                btnSnart.Enabled = true;
            }
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
                    brukerID = 4;// Classes.Validator.KonverterTilTall(Request.QueryString["bruker_id"]);
                    timeID = Classes.Validator.KonverterTilTall(Request.QueryString["time_id"]);

                    Oppgave oppgave = context3.Oppgaver.Where(o => o.Oppgave_id == oppgaveID).FirstOrDefault();
                    Bruker bruker = context3.Brukere.Where(b => b.Bruker_id == brukerID).FirstOrDefault();
                    timer = context3.Timer.Where(t => t.Time_id == timeID).FirstOrDefault();

                    oppgave.BruktTid = timespan;
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
                    context3.Timer.Add(timer);
                    context3.SaveChanges();
                }
            }
            else
            {
                Session["flashMelding"] = "Du må legge til en kommentar på oppgaven. Kommentere det du har gjort.";
                Session["flashStatus"] = Konstanter.notifikasjonsTyper.info.ToString();
            }
        }
        private void GetTimers(int pauseId, int timeId)
        {
            timer = Queries.GetTimer(timeId);
            pauser = timer.Pause;
            pause = Queries.GetPauseMedPauseID(pauseId);
            ddlOppgaver.SelectedValue = timer.Oppgave.ToString();
            ViewState["Start"] = timer.Start;

            tbTidsregistrert.Text += "Tid takingen startet: " + timer.Start + "\n";
            if (pauser != null)
            {
                for (int i = 0; i < pauser.Count; i++)
                {
                    tbTidsregistrert.Text += "Tid takingen pauset: " + pause.Start + "\n";
                    tbTidsregistrert.Text += "Tid takingen startet etter pause: " + pause.Stopp + "\n";
                }
            }
        }
    }
}