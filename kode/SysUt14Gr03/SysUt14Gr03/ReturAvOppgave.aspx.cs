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
            SessionSjekk.sjekkForRettighetPaaInnloggetBruker(Konstanter.rettighet.Utvikler);
            bruker_id = Validator.KonverterTilTall(Session["bruker_id"].ToString());

            if (Request.QueryString["oppgave_id"] != null)
            {
                int oppgave_id = Validator.KonverterTilTall(Request.QueryString["oppgave_id"]);
                oppgave = Queries.GetOppgave(oppgave_id);
                lblNavn.Text += ": " + oppgave.Tittel;
            }
            else
            {
                lblFeil.Text = "Ingen oppgave valgt!";
                btnSend.Enabled = false;
            }
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
                Response.Redirect("OversiktOppgaver.aspx", true);
                // Flash en melding
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