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
    public partial class InvitasjonAvBruker : System.Web.UI.Page
    {
        private List<Bruker> brukerListe;
        protected void Page_Load(object sender, EventArgs e)
        {
            
            if (!IsPostBack)
            {
                brukerListe = Queries.GetAlleAktiveBrukere();
                for (int i = 0; i < brukerListe.Count; i++)
                {
                    Bruker bruker = brukerListe[i];
                    ddlBrukere.Items.Add(new ListItem(bruker.ToString(), bruker.Bruker_id.ToString()));
                }
            }
        }

        protected void btnSendInvitasjon_Click(object sender, EventArgs e)
        {

        }
    }
}