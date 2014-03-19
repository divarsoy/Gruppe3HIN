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
            
            brukerList = Queries.GetBruker(UserName.Text);
            
            if (brukerList != null)
            {
                //brukerList = brukerListR.ToList<Bruker>();
                Bruker bruker = brukerList[0];
                string oppgittPassord = Password.Text;
                string riktigPassord = bruker.Passord;
                var manager = new UserManager();
                ApplicationUser user = manager.Find(UserName.Text, Password.Text);
                //user.Id = "" + bruker.Bruker_id;

                oppgittPassord = AktiverKonto.MD5Hash(oppgittPassord);

                if (string.Compare(oppgittPassord, riktigPassord, false) == 0)
                {
                    // Logg inn bruker
                    //IdentityHelper.SignIn(manager, user, RememberMe.Checked);
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