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
    public partial class FerdigstillelseAvOppgave : System.Web.UI.Page
    {
        private List<Oppgave> mineOppgaver;
        private int bruker_id;

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

            if (!IsPostBack)
            {
                mineOppgaver = Queries.GetAlleAktiveOppgaverForBruker(bruker_id);
                foreach (Oppgave oppgave in mineOppgaver)
                {
                    lsbOppgaver.Items.Add(new ListItem(oppgave.Tittel, oppgave.Oppgave_id.ToString()));
                }
            }

        }

        protected void btnFerdig_Click(object sender, EventArgs e)
        {
            int valgtOppgaveId = Convert.ToInt32(lsbOppgaver.SelectedValue);
           

            using (var db = new Context())
            {
                Oppgave oppgave = db.Oppgaver.FirstOrDefault(o => o.Oppgave_id == valgtOppgaveId);
                Status status = db.Status.FirstOrDefault(s => s.Status_id == 3); // Ferdig
                oppgave.Status = status;
                oppgave.Aktiv = false;
                oppgave.Oppdatert = DateTime.Now;

                db.SaveChanges();
            }
        }
    }
}