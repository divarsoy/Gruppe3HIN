using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SysUt14Gr03.Classes;
using SysUt14Gr03.Models;
using System.Diagnostics;

namespace SysUt14Gr03
{
    public partial class VisBruker : System.Web.UI.Page
    {
        private Bruker bruker;
        private int bruker_id;
        private int innloggetBruker_id;

        protected void Page_Load(object sender, EventArgs e)
        {
            //SessionSjekk.sjekkForRettighetPaaInnloggetBruker(Konstanter.rettighet.Prosjektleder);
            innloggetBruker_id = 2;

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
                    lblInfo.Text += "<br />Ble med: " + bruker.opprettet.ToShortDateString();
                    
                    if (isBruker)
                    {
                        lblOppgaver.Text += "<h2>Statestikk</h2>";
                        lblOppgaver.Text += "<h4>Påbegynte oppgaver:</h4>";
                        // Venter på timeregistrering for mer data..
                        foreach (Oppgave oppgave in oppgaveListe)
                        {
                            lsbOppgaver.Items.Add(new ListItem(oppgave.Tittel + "Brukt tid: " + oppgave.BruktTid, "VisOppgave.aspx?oppgave_id=" 
                                + oppgave.Oppgave_id));

                        }
                        lblKommentarer.Text += "<br /><h4>Team:</h4>";
                        foreach (Team team in teamListe)
                        {
                            lblKommentarer.Text += "<a href=\"VisTeam.aspx?team_id=" + team.Team_id + "\">" + team.Navn + "</a><br />";
                        }

                        lblKommentarer.Text += "<h3>Min aktivitet</h3>";                     
                        List<Kommentar> kommentarListe = Queries.GetAlleKommentarTilBruker(bruker_id);
                        lblKommentarer.Text += "<h4>Mine kommentarer</h4>";
                        foreach (Kommentar kommentar in kommentarListe)
                        {
                            lsbKommentarer.Items.Add(new ListItem(kommentar.Opprettet + ": " + kommentar.Tekst, "VisOppgave.aspx?oppgave_id="
                                + kommentar.Oppgave_id));

                        }

                        lblLogg.Text += "<h3>Historikk</h3>";
                        // Venter på datofelt for mer data..
                        List<Logg> loggListe = Queries.GetLoggForBruker(bruker_id);
                        foreach (Logg logg in loggListe)
                        {
                            lsbLogg.Items.Add(new ListItem(logg.Opprettet.ToShortDateString() + ": " + logg.Hendelse));

                        }

                        lblFullfort.Text += "<h4>Fullførte oppgaver</h4>";
                        List<Oppgave> ferdigeOppgaver = Queries.GetAlleFerdigeOppgaverForBruker(bruker_id);
                        foreach (Oppgave oppgave in ferdigeOppgaver)
                        {
                            lsbFFullfort.Items.Add(new ListItem(oppgave.Tittel + ", brukt tid: " + oppgave.BruktTid + (oppgave.BruktTid == 1 ? " time" : " timer")));

                        }

                        lblPrefs.Text += "<h3>Mine instillinger</h3>";

                        BrukerPreferanse brukerPrefs = Queries.GetEpostPreferanser(bruker_id);

                        if (brukerPrefs != null)
                        {
                            // Setter valgte verdier
                            lblPrefs.Text += "<br /> Varsle meg på e-post når jeg blir...";
                            lblPrefs.Text += "<br />" + " lagt til på team: " + (brukerPrefs.EpostTeam ? "Ja" : "Nei");
                            lblPrefs.Text += "<br />" + " tildelt oppgave: " + (brukerPrefs.EpostOppgave ? "Ja" : "Nei");
                            lblPrefs.Text += "<br />" + " nevnt i kommentar: " + (brukerPrefs.EpostKommentar ? "Ja" : "Nei");
                            lblPrefs.Text += "<br />" + " tildelt en tidsfrist: " + (brukerPrefs.EpostTidsfrist ? "Ja" : "Nei");
                            lblPrefs.Text += "<br />" + " noe med rapporter..: " + (brukerPrefs.EpostRapport ? "Ja" : "Nei");

                        }
                    }
                    
                    lblInfo.Visible = true;
                    lsbFFullfort.Visible = true;
                    lsbKommentarer.Visible = true;
                    lsbLogg.Visible = true;
                    lblFullfort.Visible = true;
                    lblLogg.Visible = true;
                    lblPrefs.Visible = true;
                    lblOppgaver.Visible = true;
                    lsbOppgaver.Visible = true;
                    lblKommentarer.Visible = true;
                    btnPrefs.Visible = true;
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

        protected void lsbOppgaver_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListBox lsbTest = sender as ListBox;
            string url = lsbTest.SelectedValue;
            Response.Redirect(url, true);

        }

        protected void btnPrefs_Click(object sender, EventArgs e)
        {
            Response.Redirect("utvikler/EpostPreferanser.aspx", true);
        }

        protected void lsbKommentarer_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListBox lsbTest = sender as ListBox;
            string url = lsbTest.SelectedValue;
            Response.Redirect(url, true);
        }
    }
}