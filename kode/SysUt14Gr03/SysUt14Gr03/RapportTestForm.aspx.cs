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
    public partial class RapportTestForm : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Rapport rapport = new Rapport(Rapport.PROSJEKTRAPPORT, 1);
            lblTest.Text = rapport.ToString();
        }
    }
}