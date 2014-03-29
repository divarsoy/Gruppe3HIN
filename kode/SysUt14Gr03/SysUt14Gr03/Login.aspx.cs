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
        private List<Bruker> brukerList;

        protected void Page_Load(object sender, EventArgs e)
        {
            // SysUt14Gr03.Models.Bruker bruker = new SysUt14Gr03.Models.Bruker();

        }

        protected void LoginButton_Click(object sender, EventArgs e)
        {
            
            brukerList = Queries.GetBruker("UserName");
            
            if (brukerList != null)
            {
                //brukerList = brukerListR.ToList<Bruker>();
                Bruker bruker = brukerList[0];
                string oppgittPassord = ("Password");
                string riktigPassord = bruker.Passord;
                var manager = new UserManager();
                // ApplicationUser user = manager.Find(UserName.Text, Password.Text);
                // user.Id = "" + bruker.Bruker_id;

                oppgittPassord = AktiverKonto.MD5Hash(oppgittPassord);

                if (string.Compare(oppgittPassord, riktigPassord, false) == 0)
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
            }
            
            
            // Feil brukernavn eller passord
            InvalidCredentialsMessage.Visible = true;
             
        }
        public int getBrukerID()
        {
            return brukerList[0].Bruker_id;
        }
        public string getBrukerNavn()
        {
            return brukerList[0].Brukernavn;
        }
        public string getEmail()
        {
            return brukerList[0].Epost;
        }
    }
}