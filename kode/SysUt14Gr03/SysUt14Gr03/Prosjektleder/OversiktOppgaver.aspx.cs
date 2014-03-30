using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using SysUt14Gr03.Classes;
using SysUt14Gr03.Models;

namespace SysUt14Gr03
{
    public partial class OversiktOppgaver : System.Web.UI.Page
    {
        List<Oppgave> query = null;
        int bruker_id;
        int prosjekt_id;
        string prosjektNavn;
            
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                if (Session["bruker_id"] != null)
                {
                    bruker_id = Validator.KonverterTilTall((string)Session["bruker_id"]);
                }
                else
                {
                 //       Response.Redirect("Login.aspx", true);
                    bruker_id = 3;
                }

                // Sjekker om det er lagt ved et Get parameter "prosjekt_id" og lager en spørring basert på prosjekt_id og bruker_id på innlogget bruker
                if (Request.QueryString["prosjekt_id"] != null)
                {
                    prosjekt_id = Validator.KonverterTilTall(Request.QueryString["prosjekt_id"]);
                }

                else if (Session["prosjekt_id"] != null)
                {
                    prosjekt_id = Validator.KonverterTilTall(Session["prosjekt_id"].ToString());
                }


                if (prosjekt_id >= 1 && prosjekt_id != null)
                {
                    Prosjekt prosjekt = Queries.GetProsjekt(prosjekt_id);

                    // Sjekk om prosjektleder er prosjektleder for valgt prosjekt
                    if (prosjekt.Bruker_id == bruker_id)
                    {
                        query = Queries.GetAlleOppgaverForProsjekt(prosjekt_id);

                        if (query.Count > 0)
                        {
                            string prosjektNavn = Queries.GetProsjekt(prosjekt_id).Navn;
                            string brukerNavn = Queries.GetBruker(bruker_id).ToString();
                            lblTilbakemelding.Text = string.Format("<h3>Prosjekt: {0}</h3><h3>Bruker: {1}</h3>", prosjektNavn, brukerNavn);

                            Table oppgaveTable = Tabeller.HentOppgaveTabell(query);
                            oppgaveTable.CssClass = "table";
                            PlaceHolderTable.Controls.Add(oppgaveTable);
                        }
                        else
                        {
                            lblTilbakemelding.Text = string.Format("<h3>Prosjekt: {0}</h3><p>Prosjektet inneholder ingen oppgaver</p>", prosjektNavn);
                        }
                    }
                    else
                    {
                        Session["flashMelding"] = "Du har valgt et ikke gyldig prosjekt, prøv igjen med et annet prosjekt";
                        Session["flashStatus"] = Konstanter.notifikasjonsTyper.danger.ToString();
                        Response.Redirect("~/Prosjektleder/DefaultProsjektleder", true);
                    }
                }
                else
                {
                    Session["flashMelding"] = "Du må velge et prosjekt!";
                    Session["flashStatus"] = Konstanter.notifikasjonsTyper.danger.ToString();
                    Response.Redirect("~/Prosjektleder/DefaultProsjektleder", true);
                }          

            }
        }
    }
}