using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SysUt14Gr03.Classes;
using SysUt14Gr03.Models;

namespace SysUt14Gr03
{
    public partial class DeaktiveringAvRegistrerteTimer : System.Web.UI.Page
    {
        private int bruker_id;
        private List<Time> timeListe = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                bruker_id = 2;
                timeListe = Queries.GetTimerForBruker((int)bruker_id);
                foreach (Time time in timeListe)
                {
                    Oppgave oppgListe = Queries.GetOppgave(time.Oppgave_id);                  
                    ddlTimer.Items.Add(new ListItem(time.Tid + " (t/m/s) : " + oppgListe.Tittel, time.Time_id.ToString()));
                }
            }
        }

        protected void btnDeaktiver_Click(object sender, EventArgs e)
        {
            int oppg_id;
            using (var context = new Context())
            {
                int time_id = Convert.ToInt32(ddlTimer.SelectedValue);

                var timer = (from time in context.Timer
                            where time.Time_id == time_id
                            select time).FirstOrDefault();
                timer.Aktiv = false;
                oppg_id = timer.Oppgave_id;
                context.SaveChanges();
            }
            string oppgave = Queries.GetOppgave(oppg_id).Tittel;
            Session["flashMelding"] = "Du har dekativert tiden på oppgaven " + oppgave;
            Session["flashStatus"] = Konstanter.notifikasjonsTyper.info.ToString();

        }

        protected void btnEndre_Click(object sender, EventArgs e)
        {
            int oppg_id = Convert.ToInt32(ddlTimer.SelectedValue);
            Oppgave oppgave = Queries.GetOppgaveMedTimer(oppg_id);
            lblInfo.Visible = true;
            lblInfo.Text += "<br />Info om oppgaven";
            lblInfo.Text += "<hr />";        
            lblInfo.Text += "<br />" + oppgave.Tittel;
            lblInfo.Text += "<br />" + oppgave.UserStory;
            lblInfo.Text += "<br />" + oppgave.Krav;
            Oppgave oppg = Queries.GetOppgave(oppgave.Oppgave_id);
            foreach (Time t in oppg.Timer)
            {
                lblInfo.Text += "<br />Tid: " + t.Tid;
                lblInfo.Text += "<br />Start: " + t.Start;
                lblInfo.Text += "<br />Stopp: " + t.Stopp;
                lblInfo.Text += "<br />Ferdig: " + t.IsFerdig;
            }

        }
       


    }
}