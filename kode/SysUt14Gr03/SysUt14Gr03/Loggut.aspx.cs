using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SysUt14Gr03.Classes;

namespace SysUt14Gr03
{
    public partial class Loggut : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["bruker_id"] = null;
            Session["bruker"] = null;
            Session["fornavn"] = null;
            Session["brukernavn"] = null;
            Session["loggedIn"] = null;
            Session["prosjekt_id"] = null;
            Session["flashMelding"] = "Du er nå logget ut av systemet";
            Session["flashStatus"] = Konstanter.notifikasjonsTyper.info.ToString();
            Response.Redirect("~/Default", true);
        }
    }
}