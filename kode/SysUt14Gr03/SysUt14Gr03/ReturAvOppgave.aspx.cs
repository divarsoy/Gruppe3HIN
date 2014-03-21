using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SysUt14Gr03.Models;
using SysUt14Gr03.Classes;
using System.Drawing;

namespace SysUt14Gr03
{
    public partial class ReturAvOppgave : System.Web.UI.Page
    {
        private int bruker_id;
        private int faseleder_id = 1;
        private List<Oppgave> oppgaveListe;
        private Oppgave oppgave;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["loggedIn"] == null)
            {
                Response.Redirect("default.aspx", true);
            }
            else
            {
                bruker_id = Convert.ToInt32(Session["bruker_id"]);
            }

            oppgaveListe = Queries.GetAlleAktiveOppgaverForBruker(bruker_id);
            oppgave = oppgaveListe[0];
        }

        protected void btnSend_Click(object sender, EventArgs e)
        {
            string melding = txtSvar.Text;
            if (melding != string.Empty)
            {    
                Bruker bruker = Queries.GetBruker(bruker_id);
                string tittel = bruker.ToString() + " har returnert oppgave " + oppgave.Tittel;
                Varsel.SendVarsel(faseleder_id, Varsel.OPPGAVEVARSEL, tittel, melding);
                lblFeil.Text = "Melding sendt til faseleder";
                lblFeil.ForeColor = Color.Green;
                lblFeil.Visible = true;
                txtSvar.Text = "";
            }
            else
            {
                lblFeil.Text = "Vennligst skriv inn en begrunnelse";
                lblFeil.ForeColor = Color.Red;
                lblFeil.Visible = true;
            }
        }
    }
}