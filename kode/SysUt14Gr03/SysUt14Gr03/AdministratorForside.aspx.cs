using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SysUt14Gr03.Classes;

namespace SysUt14Gr03
{
    public partial class AdministratorForside : System.Web.UI.Page
    {
        protected void Page_PreInit(Object sener, EventArgs e)
        {
            string master = SessionSjekk.findMaster();
            this.MasterPageFile = master;
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void bt_nav_opprettTeam_Click(object sender, EventArgs e)
        {
            Response.Redirect("http://localhost:60154/OpprettTeam.aspx");
 //           Server.Transfer("/OpprettTeam.aspx");
        }

   
    }
}