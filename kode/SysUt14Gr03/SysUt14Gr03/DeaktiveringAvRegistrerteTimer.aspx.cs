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
                SessionSjekk.sjekkForBruker_id();
                bruker_id = Validator.KonverterTilTall(Session["bruker_id"].ToString());
                timeListe = Queries.GetTimerForBruker((int)bruker_id);
                if (timeListe.Count != 0)
                {
                    lblHeader.Visible = true;
                    lblHeader.Text = "Endre/Deaktivere Registrerte Timer";
                    lblHeader.Text += "<hr />";
                    foreach (Time time in timeListe)
                    {
                        Oppgave oppgListe = Queries.GetOppgave(time.Oppgave_id);
                        ddlTimer.Items.Add(new ListItem(time.Tid + " (t/m/s) : " + oppgListe.Tittel, time.Time_id.ToString()));
                    }
                }
                else
                {
                    lblHeader.Visible = true;
                    lblHeader.Text = "Du har ingen registrerte timer";
                    btnDeaktiver.Visible = false;
                    btnEndre.Visible = false;
                    btnSeOppg.Visible = false;
                    ddlTimer.Visible = false;
                    
                }
            }
        }

        protected void btnDeaktiver_Click(object sender, EventArgs e)
        {
            int oppg_id;
            using (var context = new Context())
            {
                int time_id = Validator.KonverterTilTall(ddlTimer.SelectedValue);

                var timer = (from time in context.Timer
                            where time.Time_id == time_id
                            select time).FirstOrDefault();
                timer.Aktiv = false;
                oppg_id = timer.Oppgave_id;
                context.SaveChanges();
            }
            string oppgave = Queries.GetOppgave(oppg_id).Tittel;
            Session["flashMelding"] = "Du har deaktivert tiden på oppgaven: " + oppgave;
            Session["flashStatus"] = Konstanter.notifikasjonsTyper.info.ToString();
            Response.Redirect(Request.RawUrl);

        }

        protected void btnSeOppg_Click(object sender, EventArgs e)
        {
            lblInfo.Text = "";
            int oppg_id = Validator.KonverterTilTall(ddlTimer.SelectedValue);
            Time oppg = Queries.GetTimer(oppg_id);
            lblInfo.Visible = true;
            lblInfo.Text += "<br />Info om oppgaven";
            lblInfo.Text += "<hr />";
           
                lblInfo.Text += "<br />" + oppg.Oppgave.Tittel;
                lblInfo.Text += "<br />" + oppg.Oppgave.UserStory;
                lblInfo.Text += "<br />" + oppg.Oppgave.Krav;
            
            
                lblInfo.Text += "<br />Tid: " + oppg.Tid;
                lblInfo.Text += "<br />Start: " + oppg.Start;
                lblInfo.Text += "<br />Stopp: " + oppg.Stopp;
                lblInfo.Text += "<br />Ferdig: " + oppg.IsFerdig;
            
        }

        protected void btnEndre_Click(object sender, EventArgs e)
        {
            int time_id = Validator.KonverterTilTall(ddlTimer.SelectedValue);
            Time oppgave = Queries.GetTimer(time_id);

            btnLagre.Visible = true;
            lblSlutt.Visible = true;
            tbSlutt.Visible = true;
            lblStart.Visible = true;
            tbStart.Visible = true;
            lblTid.Visible = true;
            tbTid.Visible = true;

            DateTime stopp = (DateTime)oppgave.Stopp;
            DateTime start = (DateTime)oppgave.Start;
            tbStart.Text = DateTime.Parse(start.ToShortDateString()).ToString("yyyy-MM-dd");
            tbSlutt.Text = DateTime.Parse(stopp.ToShortDateString()).ToString("yyyy-MM-dd");
            tbTid.Text = Convert.ToString(oppgave.Tid);
        }

        protected void btnLagre_Click(object sender, EventArgs e)
        {
            int time_id = Validator.KonverterTilTall(ddlTimer.SelectedValue);
            int oppg_id;
           
                using (var context = new Context())
                {

                    var timer = (from time in context.Timer
                                 where time.Time_id == time_id
                                 select time).FirstOrDefault();
                    DateTime stopp = Convert.ToDateTime(tbSlutt.Text);
                    DateTime start = Convert.ToDateTime(tbStart.Text);
                    oppg_id = timer.Oppgave_id;
                    timer.Stopp = (DateTime)stopp;
                    timer.Start = (DateTime)start;
                    timer.Tid = TimeSpan.Parse(tbTid.Text);
                    context.SaveChanges();
                }
                string oppgave = Queries.GetOppgave(oppg_id).Tittel;
                Session["flashMelding"] = "Du har endret tiden på oppgaven: " + oppgave;
                Session["flashStatus"] = Konstanter.notifikasjonsTyper.info.ToString();
                Response.Redirect(Request.RawUrl);
            
        }
    
        

    }
}