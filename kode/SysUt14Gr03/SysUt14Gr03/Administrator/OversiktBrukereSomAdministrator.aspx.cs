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
    public partial class OversiktBrukereSomAdministrator : System.Web.UI.Page
    {
        private int brukerid;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["loggedIn"] == null)
            {
                Response.Redirect("~/Login.aspx", true);
            }
            else
            {
                brukerid = Validator.KonverterTilTall(Session["bruker_id"].ToString());
            }
            if (!Validator.SjekkRettighet(brukerid, Konstanter.rettighet.Administrator))
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
            else {

                if (!IsPostBack)
                {
                    List<Bruker> brukerListe = Queries.GetAlleBrukere();
                    PlaceHolderBrukere.Controls.Add(Tabeller.HentBrukereTabellForAdministrator(brukerListe));                    
                }

            }
        }
    }
}
