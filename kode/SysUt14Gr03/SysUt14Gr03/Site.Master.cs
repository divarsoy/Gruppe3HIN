using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Security.Principal;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using SysUt14Gr03;
using SysUt14Gr03.Classes;
using SysUt14Gr03.Models;

namespace SysUt14Gr03
{
    public partial class SiteMaster : MasterPage
    {
        private const string AntiXsrfTokenKey = "__AntiXsrfToken";
        private const string AntiXsrfUserNameKey = "__AntiXsrfUserName";
        private string _antiXsrfTokenValue;
        public string antallNotifikasjoner = "";
        private int i = 0;
        private int bruker_id;

        protected void Page_Init(object sender, EventArgs e)
        {
            // The code below helps to protect against XSRF attacks
            var requestCookie = Request.Cookies[AntiXsrfTokenKey];
            Guid requestCookieGuidValue;
            if (requestCookie != null && Guid.TryParse(requestCookie.Value, out requestCookieGuidValue))
            {
                // Use the Anti-XSRF token from the cookie
                _antiXsrfTokenValue = requestCookie.Value;
                Page.ViewStateUserKey = _antiXsrfTokenValue;
            }
            else
            {
                // Generate a new Anti-XSRF token and save to the cookie
                _antiXsrfTokenValue = Guid.NewGuid().ToString("N");
                Page.ViewStateUserKey = _antiXsrfTokenValue;

                var responseCookie = new HttpCookie(AntiXsrfTokenKey)
                {
                    HttpOnly = true,
                    Value = _antiXsrfTokenValue
                };
                if (FormsAuthentication.RequireSSL && Request.IsSecureConnection)
                {
                    responseCookie.Secure = true;
                }
                Response.Cookies.Set(responseCookie);
            }

            if (Session["flashMelding"] != null && Session["flashStatus"] != null)
            {
                HentFlashMelding();
            }

            Page.PreLoad += master_Page_PreLoad;



        }

        protected void master_Page_PreLoad(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Set Anti-XSRF token
                ViewState[AntiXsrfTokenKey] = Page.ViewStateUserKey;
                ViewState[AntiXsrfUserNameKey] = Context.User.Identity.Name ?? String.Empty;
            }
            else
            {
                // Validate the Anti-XSRF token
                if ((string)ViewState[AntiXsrfTokenKey] != _antiXsrfTokenValue
                    || (string)ViewState[AntiXsrfUserNameKey] != (Context.User.Identity.Name ?? String.Empty))
                {
                    throw new InvalidOperationException("Validation of Anti-XSRF token failed.");
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            //if (!IsPostBack)
            //{


            if (Session["bruker_id"] != null)
            {
                bruker_id = Validator.KonverterTilTall(Session["bruker_id"].ToString());
                //Response.Redirect("~/Login");
            }
            else
            {
                bruker_id = 2;
            }



            int antallNotifikasjonerInt = Queries.GetNotifikasjon(bruker_id).Count;
            if (antallNotifikasjonerInt > 0)
                antallNotifikasjoner = String.Format("({0})", antallNotifikasjonerInt.ToString());

            HentNotifikasjonsPanel(bruker_id);
            //}

        }

        protected void Unnamed_LoggingOut(object sender, LoginCancelEventArgs e)
        {
            Context.GetOwinContext().Authentication.SignOut();
        }

        protected void HentFlashMelding()
        {
            Label label = new Label();
            label.Text = String.Format("<div id='flash' class='flash alert alert-dismissable alert-{0}'>", Session["flashStatus"]);
            LinkButton button = new LinkButton();
            button.CssClass = "close";
            button.Attributes.Add("data-dismiss", "alert");
            button.Attributes.Add("aria-hidden", "true");
            button.Text = "&times;";
            Label labelMelding = new Label();
            labelMelding.Text = Session["flashMelding"] + "</div>";
            NotifikasjonsPanel.Controls.Add(label);
            NotifikasjonsPanel.Controls.Add(button);
            NotifikasjonsPanel.Controls.Add(labelMelding);
            Session["flashMelding"] = null;
            Session["flashStatus"] = null;
        }


        protected void HentNotifikasjonsPanel(int bruker_id)
        {
            var notifikasjonsListe = Queries.GetNotifikasjon(bruker_id);

            foreach (Notifikasjon notifikasjon in notifikasjonsListe)
            {
                Label label = new Label();
                label.Text = String.Format("<div id='flash' class='flash alert alert-dismissable {0}'>", Queries.GetNotifikasjonsType(notifikasjon.NotifikasjonsType_id).Type);
                LinkButton button = new LinkButton();
                button.CssClass = "close";
                //button.Attributes.Add("data-dismiss", "alert");  Fjernet da den kjører et javascript som overstyrer reload
                button.Attributes.Add("aria-hidden", "true");
                button.Text = "&times;";
                button.Command += new CommandEventHandler(btnNotifikasjon_Click);
                button.CommandArgument = notifikasjon.Notifikasjon_id.ToString();
                button.CommandName = i.ToString();
                Label labelMelding = new Label();
                labelMelding.Text = notifikasjon.Melding + "</div>";
                NotifikasjonsPanel.Controls.Add(label);
                NotifikasjonsPanel.Controls.Add(button);
                NotifikasjonsPanel.Controls.Add(labelMelding);
                
                i++;
            }
        }

        protected void btnNotifikasjon_Click(object sender, CommandEventArgs e)
        {
            int notifikasjon_id = Validator.KonverterTilTall(e.CommandArgument.ToString());

            using (Context context = new Context())
            {
                var notifikasjon = context.Notifikasjoner.Find(notifikasjon_id);
                notifikasjon.Vist = true;
                context.SaveChanges();
            }
            int index = Validator.KonverterTilTall(e.CommandName);
    //      NotifikasjonsPanel.Controls.RemoveAt(antallNotifikasjoner+2);
    //      NotifikasjonsPanel.Controls.RemoveAt(index+1);
    //      NotifikasjonsPanel.Controls.RemoveAt(index);
            NotifikasjonsPanel.Controls.Clear();
            HentNotifikasjonsPanel(bruker_id);
            int antallNotifikasjonerInt = Queries.GetNotifikasjon(bruker_id).Count;
            if (antallNotifikasjonerInt > 0)
                antallNotifikasjoner = String.Format("({0})", antallNotifikasjonerInt.ToString());
        }

    }

}