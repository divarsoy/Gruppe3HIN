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
    public partial class EstimerResttidPaOppgave : Page
    {
        private List<Oppgave> oppgaveListe;

        protected void Page_Load(object sender, EventArgs e)
        {
            oppgaveListe = Queries.GetAlleAktiveOppgaver();

            if (!IsPostBack)
            {               

                foreach (Oppgave oppgave in oppgaveListe)
                {
                    ddlOppgaver.Items.Add(new ListItem(oppgave.Tittel, oppgave.Oppgave_id.ToString()));
                }
            }
        }

        protected void btnLagre_Click(object sender, EventArgs e)
        {
            string tid = txtTimer.Text;
            // Setter resterende tid
            if (tid != string.Empty)
            {
                using (var context = new Context())
                {
                    int oppgave_id = Convert.ToInt32(ddlOppgaver.SelectedValue);

                    Oppgave oppgave = context.Oppgaver.FirstOrDefault(o => o.Oppgave_id == oppgave_id);

                    oppgave.RemainingTime = Convert.ToInt32(tid);
                    context.SaveChanges();

                    // Bruker gramatisk korrekt ord
                    string timer = Convert.ToInt32(tid) == 1 ? " time på " : " timer på ";

                    lblFeil.Text = "Resterende tid satt til " + tid + timer + ddlOppgaver.SelectedItem;
                    lblFeil.ForeColor = Color.Green;
                    lblFeil.Visible = true;

                }

            }
            else
            {
                lblFeil.Text = "Skriv inn tid";
                lblFeil.ForeColor = Color.Red;
                lblFeil.Visible = true;
            }
        }

        protected void btnInfo_Click(object sender, EventArgs e)
        {
            lblEstimat.Visible = true;
            lblBrukt.Visible = true;
            lblRest.Visible = true;
            lblEstimat.Text = "Estimat: " + oppgaveListe[ddlOppgaver.SelectedIndex].Estimat.ToString();
            lblBrukt.Text = "Brukt: " + oppgaveListe[ddlOppgaver.SelectedIndex].BruktTid.ToString();
            lblRest.Text = "Resterende tid: " + oppgaveListe[ddlOppgaver.SelectedIndex].RemainingTime.ToString();
        }

    }
}
