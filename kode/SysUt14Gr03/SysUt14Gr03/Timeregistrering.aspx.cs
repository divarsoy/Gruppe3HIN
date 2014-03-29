using System;
using System.Collections.Generic;
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
        //private Thread thread;

        protected void Page_Load(object sender, EventArgs e)
        {
            //if (Session["loggedIn"] == null)
            //    Response.Redirect("Login.aspx", true);

            brukerID = 2; //Classes.Validator.KonverterTilTall(Request.QueryString["oppgave_id"]);
            oppgaver = Queries.GetAlleAktiveOppgaverForBruker(brukerID);
            //thread = new Thread(brukerID);
            for (int i = 0; i < oppgaver.Count; i++)
                ddlOppgaver.Items.Add(new ListItem(oppgaver[i].Tittel, oppgaver[i].Oppgave_id.ToString()));
        }
        public void btnStart_Click(object sender, EventArgs e)
        {
            tbTidsregistrert.Text = "Tid takingen startet: {0} " + DateTime.Now;
            //thread.Start();
        }

        protected void btnPause_Click(object sender, EventArgs e)
        {
            tbTidsregistrert.Text = "Tid takingen pauset: {0} " + DateTime.Now;
        }

        protected void btnStop_Click(object sender, EventArgs e)
        {
            tbTidsregistrert.Text = "Tid takingen stoppet: {0} " + DateTime.Now;
        }
    }
}