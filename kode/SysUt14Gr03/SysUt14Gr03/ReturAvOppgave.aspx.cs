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
    /// <summary>
    /// Side som lar en bruker sende melding til faseleder om at han ikke
    /// ønsker å delta på oppgaven
    /// </summary>
    public partial class ReturAvOppgave : System.Web.UI.Page
    {
        private int bruker_id;
        private int faseleder_id = -1; // ja
        private Oppgave oppgave;

        protected void Page_PreInit(Object sener, EventArgs e)
        {
            string master = SessionSjekk.findMaster();
            this.MasterPageFile = master;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            SessionSjekk.sjekkForRettighetPaaInnloggetBruker(Konstanter.rettighet.Utvikler);
            bruker_id = Validator.KonverterTilTall(Session["bruker_id"].ToString());

            if (Request.QueryString["oppgave_id"] != null)
            {
                SessionSjekk.sjekkForProsjekt_id();
                int prosjekt_id = Validator.KonverterTilTall(Session["prosjekt_id"].ToString());

                int oppgave_id = Validator.KonverterTilTall(Request.QueryString["oppgave_id"]);
                oppgave = Queries.GetOppgave(oppgave_id);
                lblNavn.Text += ": " + oppgave.Tittel;

                // Henter automatisk nåværende fasleder
                Bruker faseleder = SessionSjekk.GetFaseleder(prosjekt_id);
                if (faseleder != null)
                    faseleder_id = faseleder.Bruker_id;
            }
            else
            {
                Session["flashMelding"] = "Ingen oppgave valgt!";
                Session["flashStatus"] = Konstanter.notifikasjonsTyper.danger.ToString();
                btnSend.Enabled = false;
            }
        }

        protected void btnSend_Click(object sender, EventArgs e)
        {
            string melding = txtSvar.Text;
            if (melding != string.Empty)
            {   
                // Setter opp meldingen som vises for faseleder, med link for enkel tilgang
                Bruker bruker = Queries.GetBruker(bruker_id);
                melding = "Bruker " + bruker.ToString() + " har returnert <a href=\"http://malmen.hin.no/SysUt14Gr03/VisOppgave.aspx?oppgave_id="
                    + oppgave.RefOppgaveId + " " + oppgave.Oppgave_id + "\">" + oppgave.Tittel + "</a>. Begrunnelse: \"" + melding + "\"";
                string tittel = bruker.ToString() + " har returnert oppgave " + oppgave.Tittel;
                if (faseleder_id != -1)
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