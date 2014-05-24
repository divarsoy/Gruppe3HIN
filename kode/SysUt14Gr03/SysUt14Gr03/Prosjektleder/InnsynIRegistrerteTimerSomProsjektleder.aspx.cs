using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SysUt14Gr03.Models;
using SysUt14Gr03.Classes;
using System.Web.UI.HtmlControls;

namespace SysUt14Gr03.Prosjektleder
{
    /// <summary>
    /// Som faseleder eller prosjekt leder skal man kunne ha kontroll at det faktisk blir gjort no. så her kan dem se
    /// alle registrerte timer i prosjektet til alle brukere. satt opp ryddig i tabeller. hver bruker får sin tabell.
    /// </summary>
    public partial class InnsynIRegistrerteTimerSomProsjektleder : System.Web.UI.Page
    {
        protected void Page_PreInit(Object sener, EventArgs e)
        {
            string master = SessionSjekk.findMaster();
            this.MasterPageFile = master;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            SessionSjekk.sjekkForProsjekt_id();
            if (SessionSjekk.IsFaseleder())
            {
                this.page();
            }
            else
            {
                SessionSjekk.sjekkForRettighetPaaInnloggetBruker(Konstanter.rettighet.Prosjektleder);
                this.page();
            }
        }
        private void page()
        {
            int prosjektId = Validator.KonverterTilTall(Session["prosjekt_id"].ToString());

            List<Bruker> brukerePaProsjekt = Queries.GetAlleBrukereIEtProjekt(prosjektId);
            Prosjekt prosjekt = Queries.GetProsjekt(prosjektId);

            foreach (Bruker b in brukerePaProsjekt)
            {
                List<Time> timer = Queries.GetTimerForBruker(b.Bruker_id);
                if (!IsPostBack)
                {
                    string navn = b.Brukernavn;
                    Table timeTabell = Tabeller.HentTimerForProsjektleder(timer, b, prosjekt);
                    var brControl3 = new LiteralControl("<br />");
                    PlaceHolderTable.Controls.Add(brControl3);
                    PlaceHolderTable.Controls.Add(timeTabell);
                }
            }
        }
    }
}