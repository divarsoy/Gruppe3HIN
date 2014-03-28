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
    public partial class Timeregistrering : System.Web.UI.Page
    {
        private List<Oppgave> oppgaver;
        private int brukerID;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["loggedIn"] == null)
                Response.Redirect("Login.aspx", true);

            brukerID = 2; //Classes.Validator.KonverterTilTall(Request.QueryString["oppgave_id"]);
            oppgaver = Queries.GetAlleAktiveOppgaverForBruker(brukerID);
        }
        public void btnStart_Click(object sender, EventArgs e)
        { 

        }

        protected void btnPause_Click(object sender, EventArgs e)
        {

        }

        protected void btnStop_Click(object sender, EventArgs e)
        {

        }
    }
}