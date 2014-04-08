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
                    lblRegTimer.Visible = true;

                    lblRegTimer.Text += "<br />" + time.Tid + " " + oppgListe.Tittel + "\n";
                }
            }
        }

        protected void btnDeaktiver_Click(object sender, EventArgs e)
        {

        }

        protected void btnEndre_Click(object sender, EventArgs e)
        {

        }
    }
}