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
    public partial class OversiktProsjekter : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            bool queryStatus = false;
            List<Prosjekt> query = null;
            int bruker_id = 2;

            query = Queries.GetAlleAktiveProsjekter();



        }
    }
}