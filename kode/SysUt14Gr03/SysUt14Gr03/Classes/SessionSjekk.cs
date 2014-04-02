using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SysUt14Gr03.Classes;

namespace SysUt14Gr03.Classes
{
    public class SessionSjekk : System.Web.HttpApplication
    {
        static public void sjekkForBruker_id (){
            HttpContext http = HttpContext.Current;
            if (http.Session["bruker_id"] == null)
            { 
                http.Session["flashMelding"] = "Du må logge deg inn";
                http.Session["flashStatus"] = Konstanter.notifikasjonsTyper.danger.ToString();
                http.Response.Redirect("~/login", true);
            }
        }
        static public void sjekkForRettighetPaaInnloggetBruker(Konstanter.rettighet rettighet)
        {
            HttpContext http = HttpContext.Current;
            sjekkForBruker_id();
            int bruker_id = Validator.KonverterTilTall(http.Session["bruker_id"].ToString());

            if (!Validator.SjekkRettighet(bruker_id, rettighet))
            {
                http.Session["bruker_id"] = null;
                http.Session["bruker"] = null;
                http.Session["fornavn"] = null;
                http.Session["brukernavn"] = null;
                http.Session["loggedIn"] = null;
                http.Session["flashMelding"] = "Du har ikke korrekte rettighet for aksessere siden du prøvde å nå";
                http.Session["flashStatus"] = Konstanter.notifikasjonsTyper.danger.ToString();
                http.Response.Redirect(("~/Login.aspx"), true);
            }
        }
        static public void sjekkForProsjekt_id()
        {
            HttpContext http = HttpContext.Current;
            sjekkForBruker_id();
            if (http.Session["prosjekt_id"] == null)
            {
                http.Session["flashMelding"] = "Du må velge et prosjekt!";
                http.Session["flashStatus"] = Konstanter.notifikasjonsTyper.danger.ToString();
                http.Response.Redirect(http.Request.UrlReferrer.ToString(), true);
            }
        }

        /*
        void Application_AcquireRequestState(object sender, EventArgs e)
        {            

        }
         * */
    }
}