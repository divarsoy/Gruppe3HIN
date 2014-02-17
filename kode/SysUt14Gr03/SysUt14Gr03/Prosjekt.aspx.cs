using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SysUt14Gr03
{
    public partial class Prosjekt : System.Web.UI.Page
    {
        Prosjekt prosjekt;
        protected void Page_Load(object sender, EventArgs e)
        {
            prosjekt = new Prosjekt();
            //prosjekt.Navn = "Første prosjektet";
        }
    }
}