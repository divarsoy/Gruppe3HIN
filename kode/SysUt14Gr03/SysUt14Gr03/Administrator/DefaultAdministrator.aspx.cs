using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SysUt14Gr03.Classes;

namespace SysUt14Gr03
{
    public partial class DefaultAdministrator : System.Web.UI.Page
    {
        private int bruker_id;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["loggedIn"] == null)
            {
                Response.Redirect("~/Login.aspx", true);
            }
            else
            {
                bruker_id = Validator.KonverterTilTall(Session["bruker_id"].ToString());

                if (!Validator.SjekkRettighet(bruker_id, Konstanter.rettighet.Administrator))
                {
                    Session["bruker_id"] = null;
                    Session["bruker"] = null;
                    Session["fornavn"] = null;
                    Session["brukernavn"] = null;
                    Session["loggedIn"] = null;
                    Session["flashMelding"] = "Du må være logget inn som administrator for aksessere siden du prøvde å nå";
                    Session["flashStatus"] = Konstanter.notifikasjonsTyper.danger.ToString();
                    Response.Redirect(("~/Login.aspx"), true);
                }
            }

        }
    }
}