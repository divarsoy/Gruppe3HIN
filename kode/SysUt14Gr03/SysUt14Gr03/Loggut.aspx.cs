using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SysUt14Gr03.Classes;

namespace SysUt14Gr03
{
    /// <summary>
    /// Når man logger ut så setter man alle session til null så dem ikke enda er aktiv. viss ikke kan andre som 
    /// er med personens pc bare gå rett inn uten å logge seg på. Stort sikkerhets glipp viss ikke session hadde 
    /// blitt stengt. 
    /// </summary>
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