using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SysUt14Gr03.Classes;
using SysUt14Gr03.Models;
using System.Web.UI.DataVisualization.Charting;

namespace SysUt14Gr03.Prosjektleder
{
    public partial class BurnDownChartFase : System.Web.UI.Page
    {
        Chart chart = new Chart();
        private Fase fase;
    /*    protected void Page_PreInit(Object sener, EventArgs e)
        {
            string master = SessionSjekk.findMaster();
            this.MasterPageFile = master;
        } */

        protected void Page_Load(object sender, EventArgs e)
        {
            ChartPlaceHolder.Controls.Clear();
            SessionSjekk.sjekkForProsjekt_id();
            int prosjekt_id = Validator.KonverterTilTall(Session["prosjekt_id"].ToString());
            if (!IsPostBack)
            {
                List<Fase> faseListe = Queries.GetFaseForProsjekt(prosjekt_id);
                foreach (Fase f in faseListe)
                {
                    ddlfaser.Items.Add(new ListItem(f.Navn, f.Fase_id.ToString()));
                }
            }
            fase = Queries.GetFase(Validator.KonverterTilTall(ddlfaser.SelectedValue));

            Table tabell = Tabeller.BurndownChartForFase(fase.Fase_id);
            chart = BurnDownDiagram.getChartForFase(fase.Fase_id);

            ChartPlaceHolder.Controls.Add(chart);
            PlaceHolderTable.Controls.Add(tabell);
        }
    }
}