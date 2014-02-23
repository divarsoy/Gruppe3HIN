using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SysUt14Gr03
{
    public partial class Logon : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void LoginButton_Click(object sender, EventArgs e)
        {
            // Dummybrukere
            // Skal hente detaljer fra database
            string[] users = { "Arne", "Per", "Pål" };
            string[] passwords = { "12345", "qwerty", "password" };
            for (int i = 0; i < users.Length; i++)
            {
                bool validUsername = (string.Compare(UserName.Text, users[i], true) == 0);
                bool validPassword = (string.Compare(Password.Text, passwords[i], false) == 0);
                if (validUsername && validPassword)
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