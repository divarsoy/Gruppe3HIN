using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using SysUt14Gr03.Classes;
using SysUt14Gr03.Models;

namespace SysUt14Gr03
{
    /// <summary>
    /// Dette er siden som brukeren logger seg inn på. Brukeren oppgir brukernavn og passord,
    /// og relevant info blir lagret i et Session-objekt. Brukeren får tilbakemelding om
    /// brukernavnet og passordet er ukorrekt. Brukeren kan også lage et nytt passord ved å
    /// trykke på en knapp.
    /// </summary>
    public partial class Login : Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void LoginButton_Click(object sender, EventArgs e)
        {
            string brukernavn = Brukernavn.Text;
            string oppgittPassord = Password.Text;

            // Sjekker om feltene er fylt ut
            if (brukernavn != string.Empty && oppgittPassord != string.Empty)
            {
                Bruker bruker = Queries.GetBrukerVedBrukernavn(brukernavn);

                if (bruker != null)
                {
                    string salt = bruker.Salt;
                    string hash = bruker.Passord;
                    if (Hash.CheckPassord(oppgittPassord, hash, salt)) // Bruker Hash-klassen for å sjekke passord
                    {
                        // Logg inn bruker

                        // http://stackoverflow.com/questions/3140341/how-to-create-persistent-cookies-in-asp-net
                        HttpCookie persist = new HttpCookie("persist");
                        persist.Values.Add("bruker_id", bruker.Bruker_id.ToString());
                        persist.Expires = DateTime.Now.AddDays(7); // Husker bruker i én uke
                        Response.Cookies.Add(persist);

                        Session["bruker_id"] = bruker.Bruker_id;
                        Session["bruker"] = bruker.ToString();
                        Session["fornavn"] = bruker.Fornavn;
                        Session["brukernavn"] = bruker.Brukernavn;
                        Session["loggedIn"] = true; // Lagrer relevant info

                        // Legger tidspunkt for siste logginn i databasen.
                        using (var context = new Context())
                        {
                            Bruker brukerRed = context.Brukere.Where(b => b.Bruker_id == bruker.Bruker_id).FirstOrDefault<Bruker>();
                            brukerRed.SistInnlogget = DateTime.Now;
                            context.SaveChanges();
                        }

                        // Sjekker rettigheter og sender brukeren videre til riktig hovedside
                        if (Validator.SjekkRettighet(bruker.Bruker_id, Konstanter.rettighet.Administrator))
                        {
                            Session["rettighet"] = Konstanter.rettighet.Administrator.ToString();
                            Response.Redirect("~/Administrator/DefaultAdministrator", true);
                        }
                        else if (Validator.SjekkRettighet(bruker.Bruker_id, Konstanter.rettighet.Prosjektleder))
                        {
                            Session["rettighet"] = Konstanter.rettighet.Prosjektleder.ToString();
                            Response.Redirect("~/Prosjektleder/DefaultProsjektleder", true);
                        }
                        else if (Validator.SjekkRettighet(bruker.Bruker_id, Konstanter.rettighet.Utvikler))
                        {
                            Session["rettighet"] = Konstanter.rettighet.Utvikler.ToString();
                            Response.Redirect("~/Utvikler/DefaultUtvikler", true);
                        }
                        else
                        {
                            Session["flashMelding"] = "Du har ingen rettigheter i dette systemet. Ta kontakt med Prosjektleder eller Administrator";
                            Session["flashStatus"] = Konstanter.notifikasjonsTyper.danger.ToString();
                            Session["bruker_id"] = null;
                            Session["loggedIn"] = null;
                            Response.Redirect("Login");
                        }
                    }
                    else
                    {
                        // Feil passord
                        InvalidCredentialsMessage.Visible = true;
                    }
                }
                else
                {
                    // Feil brukernavn
                    InvalidCredentialsMessage.Visible = true;
                }
            }
            else
            {
                InvalidCredentialsMessage.Visible = true;
            }
        }

        // Brukeren kan trykke her hvis de har glemt passordet
        protected void btnGlemtPassord_Click(object sender, EventArgs e)
        {
            Response.Redirect("lostPassword", true);
        }
    }
}