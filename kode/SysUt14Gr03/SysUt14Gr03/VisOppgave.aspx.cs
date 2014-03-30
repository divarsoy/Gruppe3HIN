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
    public partial class VisOppgave : System.Web.UI.Page
    {
        private int bruker_id;
        private int oppgave_id;
        private Oppgave oppgave;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["loggedIn"] == null)
            {
                //   Response.Redirect("Login.aspx", true);
                bruker_id = 2;
            }
            else
            {
                bruker_id = Validator.KonverterTilTall(Session["bruker_id"].ToString());
                if (bruker_id == -1)
                    Response.Redirect("Login.aspx", true);
            }

            if (Request.QueryString["oppgave_id"] != null)
            {
                oppgave_id = Validator.KonverterTilTall(Request.QueryString["oppgave_id"]);
                oppgave = Queries.GetOppgave(oppgave_id);
                if (oppgave != null)
                {
                    lblNavn.Text = oppgave.Tittel;
                    lblInfo.Text = "User story: " + oppgave.UserStory;
                    lblInfo.Text += "<br />" + "Krav: " + oppgave.Krav;
                    lblInfo.Text += "<br />" + "Estimat: " + oppgave.Estimat + " timer";
                    lblInfo.Text += "<br />" + "Resterende tid: " + oppgave.RemainingTime.ToString() + " timer";
                    lblInfo.Text += "<br />" + "Brukt tid: " + oppgave.BruktTid + " timer";
                    lblInfo.Text += "<br />" + "Tidsfrist: " + oppgave.Tidsfrist == null ? "Nei" : oppgave.Tidsfrist.ToString();
                    lblInfo.Text += "<br />" + "Dato opprettet: " + oppgave.Opprettet;
                    lblInfo.Text += "<br />" + "Dato endret: " + oppgave.Oppdatert == null ? "Ikke endret" : oppgave.Oppdatert.ToString();
                    //txtInfo.Wrap = true;
                    //txtInfo.Text = info;
                    lblInfo.Visible = true;

                    List<Kommentar> kommentarListe = Queries.GetAlleKommentarerTilOppgave(oppgave_id);
                    lblKommentarteller.Text = "Kommentarer (" + kommentarListe.Count + ")";
                    txtKommentar.Visible = true;
                    btnKommentar.Visible = true;
                    lblKommentarteller.Visible = true;
                    if (kommentarListe.Count > 0)
                    {
                        lblKommentarer.Text = "";
                        for (int i = kommentarListe.Count - 1; i >= 0; i--)
                        {
                            lblKommentarer.Text += "<hr />";
                            lblKommentarer.Text += "<br /><a href=\"VisBruker.aspx?bruker_id=" + kommentarListe[i].Bruker.Bruker_id + "\">" + kommentarListe[i].Bruker.IM + "</a>";
                            lblKommentarer.Text += "<br />" + kommentarListe[i].Opprettet.ToString();
                            lblKommentarer.Text += "<br />" + kommentarListe[i].Tekst;
                        }
                        lblKommentarer.Visible = true;
                    }    
                }
                else
                {
                    // Midlertidlig feilmelding...
                    lblNavn.Text = "Oppgaven finnes ikke..";
                }

            }
            else
            {
                // Midlertidlig feilmelding...
                lblNavn.Text = "Ingen oppgave valgt..";
            }
          

            if (!IsPostBack)
            {
                

            }

        }

        protected void btnKommentar_Click(object sender, EventArgs e)
        {
            // Her lagres kommentaren
            if (txtKommentar.Text != string.Empty)
            {
                using (var context = new Context())
                {
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

                    // Gi tilbakemelding
                }

            }
        }
    }
}