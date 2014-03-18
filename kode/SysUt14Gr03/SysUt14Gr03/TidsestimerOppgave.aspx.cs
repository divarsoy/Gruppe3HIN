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
    public partial class TidsestimerOppgave : System.Web.UI.Page
    {
        private List<Oppgave> oppgaveListe;

        protected void Page_Load(object sender, EventArgs e)
        {
            oppgaveListe = Queries.GetAlleAktiveOppgaver();

            if (!IsPostBack)
            {
                foreach (Oppgave oppgave in oppgaveListe)
                {
                    lsbOppgaver.Items.Add(new ListItem(oppgave.Tittel, oppgave.Oppgave_id.ToString()));
                }
            }
        }

        protected void btnLagreEstimat_Click(object sender, EventArgs e)
        {
            Feilmelding.Visible = false;
            int index = lsbOppgaver.SelectedIndex;
            // Vis en kalender for å velge dato/tid
            if (txtEstimat.Text != string.Empty)
            {
                int estimat = Convert.ToInt32(txtEstimat.Text);

                using (var context = new Context())
                {
                    int oppgave_id = Convert.ToInt32(lsbOppgaver.SelectedValue);

                    Oppgave oppgave = context.Oppgaver.FirstOrDefault(o => o.Oppgave_id == oppgave_id);

                    oppgave.Estimat = estimat;
                    context.SaveChanges();

                    Feilmelding.ForeColor = Color.Green;
                    Feilmelding.Text = "Estimat satt til " + estimat + " timer på " + oppgaveListe[index].Tittel;
                    Feilmelding.Visible = true;

                }
            }
            else
            {
                Feilmelding.ForeColor = Color.Red;
                Feilmelding.Text = "Velg en dato";
                Feilmelding.Visible = true;
            }
        }

        protected void btnVisEstimat_Click(object sender, EventArgs e)
        {
            txtEstimat.Text = oppgaveListe[lsbOppgaver.SelectedIndex].Estimat.ToString();
        }
    }
}