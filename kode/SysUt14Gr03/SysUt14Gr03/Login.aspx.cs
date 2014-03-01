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
    public partial class Login : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void LoginButton_Click(object sender, EventArgs e)
        {
            
            var brukerListR = Queries.GetBruker(UserName.Text);
            
            if (brukerListR != null)
            {
                List<Bruker> brukerList = brukerListR.ToList<Bruker>();
                string oppgittPassord = Password.Text;
                string riktigPassord = brukerList[0].Passord;

                if (string.Compare(oppgittPassord, riktigPassord, false) == 0)
                {
                    // Logg inn bruker
                    Response.Redirect("brukere.aspx", true);
                }
            }
            
            
            // Feil brukernavn eller passord
            InvalidCredentialsMessage.Visible = true;
        }
    }
}