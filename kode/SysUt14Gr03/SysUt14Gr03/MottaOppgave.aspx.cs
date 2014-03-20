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
        private int avsender_id = 2;
        private int mottaker_id = 6;
        private int oppgave_id = 4;
        private Bruker avsender;
        private Bruker mottaker;
        private Oppgave oppgave;

        protected void Page_Load(object sender, EventArgs e)
        {
            List<Bruker> brukerListe = Queries.GetAlleAktiveBrukere();
            mottaker = Queries.GetBruker(mottaker_id);
            avsender = Queries.GetBruker(avsender_id);

            // Tilfeldig oppgave, proof of consept
            oppgave = Queries.GetOppgave(oppgave_id);

            lblMessage.Text = "Bruker " + avsender.IM + " ønsker hjelp på oppgave "
                + oppgave.Tittel + ". Ved å godta invitasjonen blir du lagt til "
                + "på denne oppgaven.";
            lblMessage.Visible = true;

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

                    
                    lblMessage.Text = "Du har nå blitt lagt til på " + oppgave.Tittel;
                    lblMessage.ForeColor = Color.Green;
                    lblMessage.Visible = true;
                    
                }
            }
        }

        protected void btnAvsla_Click(object sender, EventArgs e)
        {
            // varlser avsender
            string message = "Bruker " + mottaker.Fornavn + " " + mottaker.Etternavn
                + " ønsker ikke å delta på oppgave " + oppgave.Tittel;

            Varsel.SendVarsel(avsender_id, Varsel.OPPGAVEVARSEL, "Avslag på oppgave", message);

            // sender mottaker tilbake til forsiden
            Response.Redirect("default.aspx", true);
        }
    }
}