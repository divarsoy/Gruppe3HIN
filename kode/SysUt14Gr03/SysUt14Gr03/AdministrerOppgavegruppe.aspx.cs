using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SysUt14Gr03.Models;
using SysUt14Gr03.Classes;

namespace SysUt14Gr03
{
    public partial class AdministrerOppgavegruppe : System.Web.UI.Page
    {
        private int prosjekt_id = 1;
        private List<Oppgave> oppgaveListe;

        protected void Page_PreInit(Object sener, EventArgs e)
        {
            string master = SessionSjekk.findMaster();
            this.MasterPageFile = master;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            oppgaveListe = Queries.GetAlleAktiveOppgaverForProsjekt(prosjekt_id);

            if (!IsPostBack)
            {
                Table oppgavetabell = Tabeller.HentOppgaveTabell(oppgaveListe);
                plhOppgavetabell.Controls.Add(oppgavetabell);
            }
        }
    }
}