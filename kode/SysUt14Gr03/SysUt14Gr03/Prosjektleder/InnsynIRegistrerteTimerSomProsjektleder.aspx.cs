using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SysUt14Gr03.Models;
using SysUt14Gr03.Classes;

namespace SysUt14Gr03.Prosjektleder
{
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
            lblProsjekt.Text = String.Format("<a href='../VisProsjekt?prosjekt_id={0}'>{1} </a>", prosjektId, prosjekt.Navn);

            foreach (Bruker b in brukerePaProsjekt)
            {
                List<Time> timer = Queries.GetTimerForBruker(b.Bruker_id);
                if (!IsPostBack)
                {
                    Table timeTabell = Tabeller.HentTimerForProsjektleder(timer, b, prosjekt);

                    PlaceHolderTable.Controls.Add(timeTabell);
                }
            }
        }
    }
}