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
    public partial class PameldingTilOppgave : System.Web.UI.Page
    {

        private List<Oppgave> oppgaveListe;
        private List<Bruker> brukerListe;
        private int bruker_id;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["loggedIn"] == null)
            {
            //    Response.Redirect("Login.aspx", true);
                bruker_id = 2;
            }
            else
            {
                bruker_id = Validator.KonverterTilTall(Session["bruker_id"].ToString());
                if (bruker_id == -1)
                    Response.Redirect("Login.aspx", true);
            }

            oppgaveListe = Queries.GetAlleAktiveOppgaver();
            brukerListe = Queries.GetAlleAktiveBrukere();

            if (!IsPostBack)
            {

                foreach (Oppgave oppgave in oppgaveListe)
                {
                    ddlOppgaver.Items.Add(new ListItem(oppgave.Tittel, oppgave.Oppgave_id.ToString()));
                }

                // Sjekk om prosjektleder
                ddlBrukere.Visible = true;
                foreach (Bruker bruker in brukerListe)
                {
                    ddlBrukere.Items.Add(new ListItem(bruker.ToString(), bruker.Bruker_id.ToString()));
                }         

            }

        }

        protected void btnLeggTil_Click(object sender, EventArgs e)
        {
            // Legger til bruker på oppgaven
            using (var context = new Context())
            {
                int oppgave_id = Convert.ToInt32(ddlOppgaver.SelectedValue);

                Bruker bruker = context.Brukere.FirstOrDefault(b => b.Bruker_id == bruker_id);
                Oppgave oppgave = context.Oppgaver.FirstOrDefault(o => o.Oppgave_id == oppgave_id);
                Status status = context.Statuser.FirstOrDefault(s => s.Status_id == 2);

                List<Bruker> tmpBruker = oppgave.Brukere;
                // Sjekker om bruker allerede er lagt til
                if (!tmpBruker.Contains(bruker))
                {
                    tmpBruker.Add(bruker);
                    oppgave.Brukere = tmpBruker;
                    oppgave.Status = status;
                    context.SaveChanges();

                    lblMelding.Text = "Bruker " + bruker.ToString() + " lagt til på " + oppgave.Tittel;
                    lblMelding.ForeColor = Color.Green;

                    // Sjekk om det er prosjektleder eller bruker selv som har lagt seg til
                    Varsel.SendVarsel(bruker.Bruker_id, Varsel.OPPGAVEVARSEL, lblMelding.Text);
                }
                else
                {
                    lblMelding.Text = "Bruker " + bruker.ToString() + " er allerede lagt til på " + oppgave.Tittel;
                    lblMelding.ForeColor = Color.Red;
                }

                lblMelding.Visible = true;
                

            }
        }
    }
}