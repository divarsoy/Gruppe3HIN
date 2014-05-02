using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SysUt14Gr03.Models;
using SysUt14Gr03.Classes;
using System.Windows.Forms;

namespace SysUt14Gr03
{
    public partial class GodkjennTimer : System.Web.UI.Page
    {
        private int bruker_id = 1;
        private int prosjekt_id;
        private Bruker bruker;

        protected void Page_Load(object sender, EventArgs e)
        {
            SessionSjekk.sjekkForBruker_id();
            bruker_id = Validator.KonverterTilTall(Session["bruker_id"].ToString());

            SessionSjekk.sjekkForProsjekt_id();
            prosjekt_id = Validator.KonverterTilTall(Session["prosjekt_id"].ToString());

            Prosjekt prosjekt = Queries.GetProsjekt(prosjekt_id);
            lblTittel.Text = "Timer som må godkjennes i prosjekt " + prosjekt.Navn;

            if (bruker_id != -1)
            {

                bruker = Queries.GetBruker(bruker_id);

                List<Oppgave> oppgaveListe = Queries.GetAlleAktiveOppgaverForProsjekt(prosjekt_id);
                List<Time> timeListe = new List<Time>();

                foreach (Oppgave o in oppgaveListe)
                {
                    timeListe.AddRange(Queries.GetTimerForGodkjenning(o.Oppgave_id));
                }

                if (!IsPostBack)
                {
                    BindingSource bindingsource = new BindingSource();
                    bindingsource.DataSource = timeListe;
                    gvwTimer.DataSource = bindingsource;
                    gvwTimer.DataBind();
                }
            }
        }

        protected void gvwTimer_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }
    }
}