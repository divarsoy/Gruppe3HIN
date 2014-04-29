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
    public partial class SprintBacklogFase : System.Web.UI.Page
    {
        private int fase_id = 4;
        protected void Page_Load(object sender, EventArgs e)
        {

            
            //Fase fase = Queries.GetFase(fase_id);
           // lblfasenavn.Text = "<h2>" + fase.Navn + "<h2/>";
          //  Table FaseTabell = Tabeller.HentFaseTabell(fase);
          //  FaseTabell.CssClass = "table";
         //   phFase.Controls.Add(FaseTabell);

        }
    }
}