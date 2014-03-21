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
    public partial class KommentarPaOppgave : System.Web.UI.Page
    {
        private List<Oppgave> oppgaveListe;
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
                oppgaveListe = Queries.GetAlleAktiveOppgaverForBruker(bruker_id);

                foreach (Oppgave oppgave in oppgaveListe)
                {
                    ddlOppgave.Items.Add(new ListItem(oppgave.Tittel, oppgave.Oppgave_id.ToString()));
                }
            }
        }

        protected void btnKommentar_Click(object sender, EventArgs e)
        {
            // Her lagres kommentaren
            if (txtKommentar.Text != string.Empty)
            {
                using (var context = new Context())
                {
                    int oppgave_id = Convert.ToInt32(ddlOppgave.SelectedValue);

                    var kommentar = new Kommentar
                    {

                        Tekst = txtKommentar.Text,
                        Aktiv = true,
                        Opprettet = DateTime.Now,
                        Bruker_id = bruker_id,
                        Oppgave_id = oppgave_id
                    };

                    context.Kommentarer.Add(kommentar);
                    context.SaveChanges();

                    lblFeil.Text = "Kommentar lagret";
                    lblFeil.ForeColor = Color.Green;
                    lblFeil.Visible = true;

                }

            }
            else
            {
                lblFeil.Text = "Skriv inn kommentar";
                lblFeil.ForeColor = Color.Red;
                lblFeil.Visible = true;
            }
        }
    }
}