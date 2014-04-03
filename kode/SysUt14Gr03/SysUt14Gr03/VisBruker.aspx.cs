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
    public partial class VisBruker : System.Web.UI.Page
    {
        private Bruker bruker;
        private int bruker_id;
        private int innloggetBruker_id;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["loggedIn"] == null)
            {
                //   Response.Redirect("Login.aspx", true);
                innloggetBruker_id = 2;
            }
            else
            {
                innloggetBruker_id = Validator.KonverterTilTall(Session["bruker_id"].ToString());
                if (innloggetBruker_id == -1)
                    Response.Redirect("Login.aspx", true);
            }

            if (Request.QueryString["bruker_id"] != null)
            {
                bruker_id = Validator.KonverterTilTall(Request.QueryString["bruker_id"]);
                bruker = Queries.GetBruker(bruker_id);
                if (bruker != null)
                {
                    bool isBruker = bruker_id == innloggetBruker_id;

                    List<Oppgave> oppgaveListe = Queries.GetAlleAktiveOppgaverForBruker(bruker_id);
                    List<Team> teamListe = Queries.GetAlleTeamsEnBrukerErMedI(bruker_id);

                    lblNavn.Text = bruker.ToString();
                    lblInfo.Text = "Brukernavn: " + bruker.Brukernavn;
                    lblInfo.Text += "<br />" + "Epost: <a href=\"mailto:" + bruker.Epost + "\">" + bruker.Epost + "</a>";
                    
                    if (isBruker)
                    {
                        lblInfo.Text += "<hr />" + "Oppgaver:";
                        foreach (Oppgave oppgave in oppgaveListe)
                        {
                            lblInfo.Text += "<br /><a href=\"VisOppgave.aspx?oppgave_id=" + oppgave.Oppgave_id + "\">" + oppgave.Tittel + "</a>";
                        }
                        lblInfo.Text += "<hr />" + "Team:";
                        foreach (Team team in teamListe)
                        {
                            lblInfo.Text += "<br /><a href=\"VisTeam.aspx?team_id=" + team.Team_id + "\">" + team.Navn + "</a>";
                        }
                    }
                    
                    //lblInfo.Text += "<br />" + "Dato endret: " + oppgave.Oppdatert == null ? "Ikke endret" : oppgave.Oppdatert.ToString();
                    //txtInfo.Wrap = true;
                    //txtInfo.Text = info;
                    lblInfo.Visible = true;

                }
                else
                {
                    // Midlertidlig feilmelding...
                    lblNavn.Text = "Brukeren finnes ikke..";
                }
            }
            else
            {
                // Midlertidlig feilmelding...
                lblNavn.Text = "Ingen bruker valgt..";
            }

        }
    }
}