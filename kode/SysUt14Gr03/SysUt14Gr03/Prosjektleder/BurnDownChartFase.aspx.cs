using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SysUt14Gr03.Classes;

namespace SysUt14Gr03.Prosjektleder
{
    public partial class BurnDownChartFase : System.Web.UI.Page
    {
        protected void Page_PreInit(Object sener, EventArgs e)
        {
            string master = SessionSjekk.findMaster();
            this.MasterPageFile = master;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            Table tabell = Tabeller.BurndownChartForFase(10);
            PlaceHolderTable.Controls.Add(tabell);
        }
    }
}