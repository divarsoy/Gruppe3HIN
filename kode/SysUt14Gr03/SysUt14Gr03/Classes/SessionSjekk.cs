using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SysUt14Gr03.Classes;
using SysUt14Gr03.Models;

namespace SysUt14Gr03.Classes
{
    public class SessionSjekk : System.Web.HttpApplication
    {
        public static void sjekkForBruker_id (){
            HttpContext http = HttpContext.Current;
            if (http.Session["bruker_id"] == null)
            { 
                http.Session["flashMelding"] = "Du må logge deg inn";
                http.Session["flashStatus"] = Konstanter.notifikasjonsTyper.danger.ToString();
                http.Response.Redirect("~/login", true);
            }
        }
        public static void sjekkForRettighetPaaInnloggetBruker(Konstanter.rettighet rettighet)
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
        public static void sjekkForProsjekt_id()
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

        public static string findMaster()
        {
            HttpContext http = HttpContext.Current;
            sjekkForBruker_id();
            int bruker_id = Validator.KonverterTilTall(http.Session["bruker_id"].ToString());
            Rettighet rettighet = Queries.GetRettighet(bruker_id);

            string master = "";


            if (rettighet.RettighetNavn == Konstanter.rettighet.Administrator.ToString())
                master = "~/Site.SysAdm.Master";
            else if (rettighet.RettighetNavn == Konstanter.rettighet.Prosjektleder.ToString())
                master = "~/Site.Prosjektleder.master";
            else if (rettighet.RettighetNavn == Konstanter.rettighet.Utvikler.ToString())
                master = "~/Site.Utvikler.Master";
            if (http.Session["prosjekt_id"] != null)
                if (IsFaseleder())
                    return "~/Site.Faseleder.Master";

            return master;
        }

        public static bool IsFaseleder(int bruker_id, int prosjekt_id)
        {
            var faser = Queries.GetAlleAktiveFaserForBrukerOgProsjekt(bruker_id, prosjekt_id);

            if (faser.Count > 0)
            {
                foreach (Fase fase in faser)
                {
                    if (DateTime.Now >= fase.Start && DateTime.Now <= fase.Stopp)
                        return true;
                }
            }
            return false;
        }

        public static bool IsFaseleder(int prosjekt_id)
        {
            HttpContext http = HttpContext.Current;

            if (http.Session["bruker_id"] != null)
            {
                int bruker_id = Validator.KonverterTilTall(http.Session["bruker_id"].ToString());
                return IsFaseleder(bruker_id, prosjekt_id);
            }
            else
            {
                throw new NullReferenceException("Session objektet for bruker_id er ikke satt");
            }
        }

        public static bool IsFaseleder()
        {
            HttpContext http = HttpContext.Current;

            if (http.Session["bruker_id"] != null)
            {
                if (http.Session["prosjekt_id"] != null)
                {
                    int bruker_id = Validator.KonverterTilTall(http.Session["bruker_id"].ToString());
                    int prosjekt_id = Validator.KonverterTilTall(http.Session["prosjekt_id"].ToString());
                    return IsFaseleder(bruker_id, prosjekt_id);
                }
                else
                    throw new NullReferenceException("Session objektet for prosjekt_id er ikke satt");
            }
            else
                throw new NullReferenceException("Session objektet for bruker_id er ikke satt");
        }
        public static Bruker GetFaseleder(int prosjekt_id)
        {
            var faser = Queries.GetFaseForProsjekt(prosjekt_id);
            var brukere = Queries.GetAlleAktiveBrukere();

            if (brukere.Count > 0)
            {
                if (faser.Count > 0)
                {
                    foreach (Bruker b in brukere)
                    {
                        if (IsFaseleder(b.Bruker_id, prosjekt_id))
                            return b;
                    }
                }
            }
            return null;
        }
    }
}