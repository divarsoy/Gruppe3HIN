using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SysUt14Gr03.Classes;
using SysUt14Gr03.Models;
using System.Web.UI.DataVisualization.Charting;

namespace SysUt14Gr03
{
    public partial class TestFremdriftsdiagrammer : System.Web.UI.Page
    {
        protected void Page_PreInit(Object sener, EventArgs e)
        {
            string master = SessionSjekk.findMaster();
            this.MasterPageFile = master;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            Chart chart = BurnDownDiagram.getChartForFase(10);

            ChartPlaceHolder.Controls.Add(chart);
        }
    }
}