using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SysUt14Gr03.Classes;
using SysUt14Gr03.Models;
using System.Drawing;

namespace SysUt14Gr03
{
    public partial class MottaOppgave : System.Web.UI.Page
    {
        private int avsender_id;
        private int mottaker_id;
        private int oppgave_id;
        private Bruker avsender;
        private Bruker mottaker;
        private Oppgave oppgave;

        protected void Page_PreInit(Object sener, EventArgs e)
        {
            string master = SessionSjekk.findMaster();
            this.MasterPageFile = master;
        }

        protected void Page_Load(object sender, EventArgs e)
        {

            SessionSjekk.sjekkForBruker_id();
            mottaker_id = Validator.KonverterTilTall(Session["bruker_id"].ToString());

            List<Bruker> brukerListe = Queries.GetAlleAktiveBrukere();
            mottaker = Queries.GetBruker(mottaker_id);

            btnGodta.Enabled = false;
            btnAvsla.Enabled = false;

            if (Request.QueryString["oppgave_id"] != null && Request.QueryString["bruker_id"] != null)
            {
                avsender_id = Validator.KonverterTilTall(Request.QueryString["bruker_id"].ToString());
                oppgave_id = Convert.ToInt32(Request.QueryString["oppgave_id"]);
                oppgave = Queries.GetOppgave(oppgave_id);
                avsender = Queries.GetBruker(avsender_id);

                if (oppgave != null && avsender != null)
                {
                    lblMessage.Text = "Bruker " + avsender.IM + " ønsker hjelp på oppgave "
                    + oppgave.Tittel + "." + Environment.NewLine + "Ved å godta invitasjonen blir du lagt til "
                    + "på denne oppgaven.";
                    lblMessage.Visible = true;
                    btnGodta.Enabled = true;
                    btnAvsla.Enabled = true;
                }
                else
                {
                    lblMessage.Text = "Oppgaven finnes ikke";
                    lblMessage.ForeColor = Color.Red;
                    lblMessage.Visible = true;
                }
                
            }

        }

        protected void btnGodta_Click(object sender, EventArgs e)
        {
            using (var context = new Context())
            {
                mottaker = context.Brukere.FirstOrDefault(b => b.Bruker_id == mottaker_id);
                oppgave = context.Oppgaver.FirstOrDefault(o => o.Oppgave_id == oppgave_id);
                List<Bruker> tmpBruker = oppgave.Brukere;
                // Sjekker om bruker allerede er lagt til
                if (!tmpBruker.Contains(mottaker))
                {
                    tmpBruker.Add(mottaker);
                    oppgave.Brukere = tmpBruker;
                    context.SaveChanges();

                    Session["flashMelding"] = "Du har nå blitt lagt til på " + oppgave.Tittel;
                    Session["flashStatus"] = Konstanter.notifikasjonsTyper.info.ToString();

                    // Sender varsel til avsender
                    string melding = "Bruker " + mottaker.ToString() + " har godtatt invitasjonen til oppgave " + oppgave.Tittel;
                    Varsel.SendVarsel(avsender_id, Varsel.OPPGAVEVARSEL, "Aksept av oppgave", melding);
                    
                }
            }
        }

        protected void btnAvsla_Click(object sender, EventArgs e)
        {
            // varlser avsender
            string message = "Bruker " + mottaker.Fornavn + " " + mottaker.Etternavn
                + " ønsker ikke å delta på oppgave " + oppgave.Tittel;

            Varsel.SendVarsel(avsender_id, Varsel.OPPGAVEVARSEL, "Avslag på oppgave", message);

            Session["flashMelding"] = "Du har avslått invitasjonen";
            Session["flashStatus"] = Konstanter.notifikasjonsTyper.info.ToString();
            // sender mottaker tilbake til forsiden
            Response.Redirect("default.aspx", true);
        }
    }
}