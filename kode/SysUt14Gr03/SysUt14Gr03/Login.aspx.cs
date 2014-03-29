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
    public partial class Login : Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void LoginButton_Click(object sender, EventArgs e)
        {
            string epost = Epost.Text;
            string oppgittPassord = Password.Text;

            Bruker bruker = Queries.GetBrukerVedEpost(epost);

            if (bruker != null)
            {
                string salt = bruker.Salt;
                string hash = bruker.Passord;
                if (Hash.CheckPassord(oppgittPassord, hash, salt))
                {
                    // Logg inn bruker
                    // IdentityHelper.SignIn(manager, user, RememberMe.Checked);

                    // http://stackoverflow.com/questions/3140341/how-to-create-persistent-cookies-in-asp-net
                    HttpCookie persist = new HttpCookie("persist");
                    persist.Values.Add("bruker_id", bruker.Bruker_id.ToString());
                    persist.Expires = DateTime.Now.AddDays(7); // Husker bruker i én uke
                    Response.Cookies.Add(persist);

                    Session["bruker_id"] = bruker.Bruker_id;
                    Session["bruker"] = bruker.ToString();
                    Session["fornavn"] = bruker.Fornavn;
                    Session["brukernavn"] = bruker.Brukernavn;
                    Session["loggedIn"] = true;
                    if (Validator.SjekkRettighet(bruker.Bruker_id, Konstanter.rettighet.Administrator))
                    {
                        Session["rettighet"] = Konstanter.rettighet.Administrator.ToString();
                        Response.Redirect("DefaultAdministrator");
                    }
                    else if (Validator.SjekkRettighet(bruker.Bruker_id, Konstanter.rettighet.Prosjektleder))
                    {
                        Session["rettighet"] = Konstanter.rettighet.Prosjektleder.ToString();
                        Response.Redirect("DefaultProsjektleder");
                    }
                    else if (Validator.SjekkRettighet(bruker.Bruker_id, Konstanter.rettighet.Teamleder))
                    {
                        Session["rettighet"] = Konstanter.rettighet.Teamleder.ToString();
                        Response.Redirect("Default");
                    }
                    else if (Validator.SjekkRettighet(bruker.Bruker_id, Konstanter.rettighet.Utvikler))
                    {
                        Session["rettighet"] = Konstanter.rettighet.Utvikler.ToString();
                        Response.Redirect("Default");
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
    }
}