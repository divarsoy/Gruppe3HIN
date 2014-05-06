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
    public partial class VisBrukerAdmin : System.Web.UI.Page
    {
        private Bruker bruker;
        private int bruker_id;
        private int innloggetBruker_id;

        protected void Page_PreInit(Object sener, EventArgs e)
        {
            string master = SessionSjekk.findMaster();
            this.MasterPageFile = master;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            SessionSjekk.sjekkForBruker_id();
            innloggetBruker_id = Validator.KonverterTilTall(Session["bruker_id"].ToString());

            if (Request.QueryString["bruker_id"] != null)
            {
                bruker_id = Validator.KonverterTilTall(Request.QueryString["bruker_id"]);
                bruker = Queries.GetBruker(bruker_id);
                if (bruker != null)
                {
                    bool isBruker = bruker_id == innloggetBruker_id;

                    List<Oppgave> oppgaveListe = Queries.GetAlleAktiveOppgaverForBruker(bruker_id);
                    List<Team> teamListe = Queries.GetAlleTeamsEnBrukerErMedI(bruker_id);

                    labelNavn.Text = bruker.ToString();
                    labelInfo.Text = "Brukernavn: " + bruker.Brukernavn;
                    labelInfo.Text += "<br />" + "Epost: <a href=\"mailto:" + bruker.Epost + "\">" + bruker.Epost + "</a>";
                    labelInfo.Text += "<br />Ble med: " + bruker.Opprettet.ToShortDateString();
                    labelInfo.Text += "<br />Sist innlogget: " + bruker.SistInnlogget.GetValueOrDefault().ToString();
                    
                    if (isBruker)
                    {
                        labelOppgaver.Text += "<h2>Statestikk</h2>";
                        labelOppgaver.Text += "<h4>Påbegynte oppgaver:</h4>";

                        List<Time> timeListe = Queries.GetTimerForBruker(bruker_id);
                        
                        

                        foreach (Oppgave oppgave in oppgaveListe)
                        {
                            TimeSpan sum = new TimeSpan();
                            foreach (Time time in timeListe)
                            {
                                if (time.Oppgave_id == oppgave.Oppgave_id)
                                {
                                    sum += time.Tid;
                                }
                            }
                            lbOppgaver.Items.Add(new ListItem(oppgave.Tittel + " Brukt tid: " + sum.ToString(), "VisOppgave.aspx?oppgave_id=" 
                                + oppgave.Oppgave_id));

                        }

                        if (oppgaveListe.Count > 0)
                            lbOppgaver.Visible = true;

                        labelKommentarer.Text += "<br /><h4>Team:</h4>";
                        foreach (Team team in teamListe)
                        {
                            labelKommentarer.Text += "<a href=\"VisTeam.aspx?team_id=" + team.Team_id + "\">" + team.Navn + "</a><br />";
                        }

                        labelLogg.Text += "<h2>Min aktivitet</h2>";                     
                        List<Kommentar> kommentarListe = Queries.GetAlleKommentarTilBruker(bruker_id);
                        labelLogg.Text += "<h4>Mine kommentarer</h4>";
                        for (int i = kommentarListe.Count - 1; i >= 0; i--)
                        {
                            lbKommentarer.Items.Add(new ListItem(kommentarListe[i].Opprettet + ": " + kommentarListe[i].Tekst, "VisOppgave.aspx?oppgave_id="
                                + kommentarListe[i].Oppgave_id));

                        }

                        if (kommentarListe.Count > 0)
                            lbKommentarer.Visible = true;

                        labelHistorikk.Text += "<h4>Historikk</h4>";
                        List<Logg> loggListe = Queries.GetLoggForBruker(bruker_id);
                        if (loggListe.Count > 0)
                        {
                            for (int i = loggListe.Count - 1; i >= 0; i--)
                            {
                                lbLogg.Items.Add(new ListItem(loggListe[i].Opprettet.ToShortDateString() + ": " + loggListe[i].Hendelse));

                            }
                        }
                        else
                        {
                            lbLogg.Items.Add("Ingen hendelser...");
                        }
                        lbLogg.Visible = true;

                        labelFullfort.Text += "<h4>Fullførte oppgaver</h4>";
                        List<Oppgave> ferdigeOppgaver = Queries.GetAlleFerdigeOppgaverForBruker(bruker_id);
                        foreach (Oppgave oppgave in ferdigeOppgaver)
                        {
                            lbFFullfort.Items.Add(new ListItem(oppgave.Tittel + ", brukt tid: " + oppgave.BruktTid + (oppgave.BruktTid.GetValueOrDefault().Hours == 1 ? " time" : " timer")));

                        }

                        if (ferdigeOppgaver.Count > 0)
                            lbFFullfort.Visible = true;

                        labelPrefs.Text += "<h2>Mine instillinger</h2>";

                        BrukerPreferanse brukerPrefs = Queries.GetBrukerPreferanse(bruker_id);

                        if (brukerPrefs != null)
                        {
                            // Leser og setter valgte verdier
                            labelPrefs.Text += " Varsle meg på e-post når jeg blir...<br />";
                            labelPrefs.Text += "<br />" + " lagt til på team: " + (brukerPrefs.EpostTeam ? "Ja" : "Nei");
                            labelPrefs.Text += "<br />" + " tildelt oppgave: " + (brukerPrefs.EpostOppgave ? "Ja" : "Nei");
                            labelPrefs.Text += "<br />" + " nevnt i kommentar: " + (brukerPrefs.EpostKommentar ? "Ja" : "Nei");
                            labelPrefs.Text += "<br />" + " tildelt en tidsfrist: " + (brukerPrefs.EpostTidsfrist ? "Ja" : "Nei");
                            labelPrefs.Text += "<br />";
                            labelPrefs.Text += "<br />" + " Opplæring på hjemmeside: " + (brukerPrefs.Sheperd ? "Ja" : "Nei");
                            buttonPrefs.Visible = true;
                        }
                    }

                    labelInfo.Visible = true;
                    labelFullfort.Visible = true;
                    labelLogg.Visible = true;
                    labelPrefs.Visible = true;
                    labelOppgaver.Visible = true;
                    labelKommentarer.Visible = true;
                }
                else
                {
                    // Midlertidlig feilmelding...
                    labelNavn.Text = "Brukeren finnes ikke..";
                }
            }
            else
            {
                // Midlertidlig feilmelding...
                labelNavn.Text = "Ingen bruker valgt..";
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

            Response.Redirect("/Innstillinger.aspx", true);
        }

        protected void lsbKommentarer_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListBox lsbTest = sender as ListBox;
            string url = lsbTest.SelectedValue;
            Response.Redirect(url, true);
        }
    }
}