using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SysUt14Gr03.Classes;
using SysUt14Gr03.Models;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace SysUt14Gr03
{
    public partial class Login : Page
    {
        private List<Bruker> brukerList;

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void LoginButton_Click(object sender, EventArgs e)
        {
            
            var brukerListR = Queries.GetBruker(UserName.Text);
            
            if (brukerListR != null)
            {
                brukerList = brukerListR.ToList<Bruker>();
                Bruker bruker = brukerList[0];
                string oppgittPassord = Password.Text;
                string riktigPassord = brukerList[0].Passord;
                var manager = new UserManager();
                ApplicationUser user = manager.Find(UserName.Text, Password.Text);
                user.Id = "" + bruker.Bruker_id;

                if (string.Compare(oppgittPassord, riktigPassord, false) == 0)
                {
                    // Logg inn bruker
                    IdentityHelper.SignIn(manager, user, RememberMe.Checked);
                    Response.Redirect("brukere.aspx", true);
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