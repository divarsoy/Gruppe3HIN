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
                    lbTimer.Visible = true;
                    lbTimer.Items.Add(new ListItem(time.Tid + " (t/m/s) : " + oppgListe.Tittel, time.Time_id.ToString()));
                    // lblRegTimer.Text += "<br />" + time.Tid + " (t/m/s) " + oppgListe.Tittel + "\n";
                }
            }
        }

        protected void btnDeaktiver_Click(object sender, EventArgs e)
        {

            using (var context = new Context())
            {
                int time_id = Convert.ToInt32(lbTimer.SelectedValue);

                var timer = (from time in context.Timer
                            where time.Time_id == time_id
                            select time).FirstOrDefault();
                timer.Aktiv = false;
                context.SaveChanges();
            }
            Session["flashMelding"] = "Du har dekativert tiden på oppgaven: ";
            Session["flashStatus"] = Konstanter.notifikasjonsTyper.info.ToString();

        }

        protected void btnEndre_Click(object sender, EventArgs e)
        {
            lbEndre.Items.Clear();
            int oppg_id = Convert.ToInt32(lbTimer.SelectedValue);
            lbEndre.Visible = true;
            Oppgave oppg = Queries.GetOppgave(oppg_id);

            lbEndre.Items.Add(new ListItem(oppg.Tittel + " " + oppg.UserStory, oppg.Oppgave_id.ToString()));
        }

        protected void lbTimer_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}