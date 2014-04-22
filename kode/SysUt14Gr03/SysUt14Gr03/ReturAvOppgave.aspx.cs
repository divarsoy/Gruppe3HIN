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
        private int faseleder_id = 1; // kommer senere
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
                // Flash en melding
                Session["flashMelding"] = "Melding sendt til faseleder";
                Session["flashStatus"] = Konstanter.notifikasjonsTyper.info.ToString();
                txtSvar.Text = "";
                Response.Redirect("OversiktOppgaver.aspx", true);
                
            }
            else
            {
                Session["flashMelding"] = "Vennligst skriv inn en begrunnelse";
                Session["flashStatus"] = Konstanter.notifikasjonsTyper.danger.ToString();
            }
        }
    }
}