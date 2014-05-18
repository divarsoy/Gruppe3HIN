using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SysUt14Gr03.Classes;
using SysUt14Gr03.Models;

/// <summary>
/// Viser all informasjon om en valgt oppgave + hvilke deltagere som er med på den og deres totalt registrert tid,
/// og alle kommentarer til oppgaven + at man kan legge til en kommentar til oppgaven. 
/// </summary>

namespace SysUt14Gr03
{
    public partial class VisOppgave : System.Web.UI.Page
    {
        private int bruker_id;
        private int oppgave_id;
        private Oppgave oppgave;
        private List<Kommentar> kommentarListe;

        protected void Page_PreInit(Object sener, EventArgs e)
        {
            string master = SessionSjekk.findMaster();
            this.MasterPageFile = master;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            SessionSjekk.sjekkForBruker_id();
            bruker_id = Validator.KonverterTilTall(Session["bruker_id"].ToString());

            if (Request.QueryString["oppgave_id"] != null)
            {
                //oppgave_id = 1;
                oppgave_id = Validator.KonverterTilTall(Request.QueryString["oppgave_id"]);
                oppgave = Queries.GetOppgave(oppgave_id);
                if (oppgave != null)
                {
                    bool isFerdig = (oppgave.Status_id == 3);

                    bool isDeltaker = false;
                    foreach (Bruker bruker in oppgave.Brukere)
                    {
                        if (bruker.Bruker_id == bruker_id)
                            isDeltaker = true;
                    }

                    if (isDeltaker && !isFerdig)
                    {
                        btnInviter.Visible = true;
                        btnReturn.Visible = true;
                        btnTimer.Visible = true;
                        btnFullfor.Visible = true;
                    }
                    else
                    {
                        btnPameld.Visible = true;
                    }
                    
                    lblNavn.Text = oppgave.Tittel;
                    lblInfo.Text = "";
                    lblInfo.Text += "<p><b>User story: </b>" + oppgave.UserStory + "</p>\n";
                    lblInfo.Text += "<p><b>" + "Oppgave-ID: </b>" + oppgave.RefOppgaveId + "</p>\n";
                    lblInfo.Text += "<p><b>" + "Krav: </b>" + oppgave.Krav + "</p>\n";
                    lblInfo.Text += "<p><b>" + "Estimat: </b>" + oppgave.Estimat + " timer</p>\n";
                    lblInfo.Text += "<p><b>" + "Resterende tid: </b>" + oppgave.RemainingTime.ToString() + " timer</p>\n";
                    lblInfo.Text += "<p><b>" + "Brukt tid: </b>" + oppgave.BruktTid.ToString() + " timer</p>\n";

                    string frist = "Tidsfrist: </b>";
                    if (oppgave.Tidsfrist == null)
                    {
                        frist += "-";
                    }
                    else
                    {
                        if ((DateTime)oppgave.Tidsfrist < DateTime.Now) 
                        {
                            frist += "<span style=\"color:red\">" + oppgave.Tidsfrist.ToString() + "</span>";
                        }
                        else
                        {
                            frist += oppgave.Tidsfrist.ToString();
                        }
                    }
                    frist += "</p>\n";

                    lblInfo.Text += "<p><b>" + frist;
                    lblInfo.Text += "<p><b>" + "Status: </b>" + oppgave.Status.Navn + "</p>\n";
                    lblInfo.Text += "<p><b>" + "Prioritet: </b>" + oppgave.Prioritering_id + "</p>\n";

                    int avhengigOppgave = Validator.SjekkAvhengighet(oppgave_id);
                    Oppgave avhengig = null;
                    // Gjør noen sjekker ang. avhengighet
                    if (avhengigOppgave != -1)
                    {
                        avhengig = Queries.GetOppgave(avhengigOppgave);
                        btnInviter.Enabled = false;
                        btnInviter.ToolTip = "Oppgaven er avhengig av " + avhengig.Tittel;
                        btnPameld.Enabled = false;
                        btnPameld.ToolTip = "Oppgaven er avhengig av " + avhengig.Tittel;
                        btnTimer.Enabled = false;
                        btnTimer.ToolTip = "Oppgaven er avhengig av " + avhengig.Tittel;
                        btnFullfor.Visible = false;
                        
                    }

                    lblInfo.Text += "<p><b>" + "Avhengighet: </b>" + (avhengigOppgave == -1 ? "Nei</p>\n" : "<a href=\"visOppgave?oppgave_id=" + avhengigOppgave + "\">" + avhengig.Tittel + "</a></p>\n");
                    lblInfo.Text += "<p><b>" + "Dato opprettet: </b>" + oppgave.Opprettet + "</p>\n";
                    lblInfo.Text += "<p><b>" + "Dato endret: </b>" + (oppgave.Oppdatert == null ? "Ikke endret" : oppgave.Oppdatert.ToString());
                    lblInfo.Text += "</p>\n";
                    lblInfo.Text += "<p><b>" + "Prosjekt: </b>" + "<a href=\"visProsjekt?Prosjekt_id=" + oppgave.Prosjekt_id + "\">" + oppgave.Prosjekt.Navn + "</a></p>\n";
                    lblInfo.Text += "<hr />" + "<b>Deltaker(e) </b>";
                    // Innsyn i annen brukers registrerte timer
                    foreach (Bruker bruker in oppgave.Brukere)
                    {
                        TimeSpan sum = new TimeSpan(0);
                        List<Time> timeListe = Queries.GetTimerForOppgaveOgBruker(oppgave_id, bruker.Bruker_id);
                        foreach (Time t in timeListe)
                            sum += t.Tid;

                        lblInfo.Text += "<p><a href=\"VisBruker.aspx?bruker_id=" + bruker.Bruker_id + "\">" + bruker.IM + "</a>";
                        lblInfo.Text += ": " + (int)sum.TotalHours + "t " + sum.Minutes + "m";
                        lblInfo.Text += "</p>\n";
                    }
                    //lblInfo.Text += "<br />" + "Dato endret: " + oppgave.Oppdatert == null ? "Ikke endret" : oppgave.Oppdatert.ToString();
                    //txtInfo.Wrap = true;
                    //txtInfo.Text = info;
                    lblInfo.Visible = true;

                    kommentarListe = Queries.GetAlleKommentarerTilOppgave(oppgave_id);
                    OppdaterKommentarer(false);
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

                    kommentarListe.Add(kommentar);
                    OppdaterKommentarer(true);

                    // Gi tilbakemelding
                }
                // Response.Redirect(Request.RawUrl);

            }
        }

        private void OppdaterKommentarer(bool sjekkNavn)
        {
            kommentarListe = Queries.GetAlleKommentarerTilOppgave(oppgave_id);
            lblKommentarteller.Text = "<h2>Kommentarer (" + kommentarListe.Count + ")</h2>";
            txtKommentar.Visible = true;
            btnKommentar.Visible = true;
            lblKommentarteller.Visible = true;
            if (kommentarListe.Count > 0)
            {
                lblKommentarer.Text = "";
                for (int i = kommentarListe.Count - 1; i >= 0; i--)
                {

                    lblKommentarer.Text += "<p> <a href=\"VisBruker.aspx?bruker_id=" + kommentarListe[i].Bruker.Bruker_id + "\">" + kommentarListe[i].Bruker.Brukernavn + "</a>";
                    lblKommentarer.Text += "<p><b>" + kommentarListe[i].Opprettet.ToString() + "</b></p>\n";
                    lblKommentarer.Text += "<p>" + kommentarListe[i].Tekst + "<p>\n<hr>\n";                 
                }

                lblKommentarer.Visible = true;

                if (sjekkNavn)
                {
                    Kommentar nyKommentar = kommentarListe[kommentarListe.Count - 1];
                    if (nyKommentar.Tekst.Contains("@"))
                    {
                        MatchCollection match = Regex.Matches(nyKommentar.Tekst, @"(?<!\w)@\w+");
                        foreach (Match ord in match)
                        {
                            string[] navn = Regex.Split(ord.Value, @"^@");
                            using (var context = new Context())
                            {
                                foreach (string userName in navn)
                                {
                                    if (userName != String.Empty)
                                    {
                                        Bruker bruker = context.Brukere.Where(b => b.Brukernavn == userName).FirstOrDefault();
                                        if (bruker != null)
                                        {
                                            string fornavn = Queries.GetBruker(bruker_id).ToString();
                                            Varsel.SendVarsel(bruker.Bruker_id, Varsel.KOMMENTARVARSEL, "Kommentar", fornavn + " har nevnt deg i en kommentar på oppgave " + oppgave.Tittel);
                                        }

                                    }
                                }
                            }
                        }
                        txtKommentar.Text = "";

                    }

                } 
                
            } 
        }

        protected void btnPaMeld_Click(object sender, EventArgs e)
        {
            // Bruker kan melde seg på
            using (var context = new Context())
            {
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
                }
            }
            Response.Redirect(Request.RawUrl);
        }

        protected void btnInviter_Click(object sender, EventArgs e)
        {
            // Bruker kan invitere andre brukere
            Response.Redirect("InvitasjonAvBruker.aspx?oppgave_id=" + oppgave_id, true);
        }

        protected void btnReturn_Click(object sender, EventArgs e)
        {
            // Bruker kan returnere oppgaven
            Response.Redirect("ReturAvOppgave.aspx?oppgave_id=" + oppgave_id, true);
        }

        protected void btnTimer_Click(object sender, EventArgs e)
        {
            Response.Redirect("Timeregistrering.aspx?oppgave_id=" + oppgave_id, true);
        }

        protected void btnFullfor_Click(object sender, EventArgs e)
        {
            using (var context = new Context())
            {
                Oppgave oppgave = context.Oppgaver.FirstOrDefault(o => o.Oppgave_id == oppgave_id);
                Status status = context.Statuser.FirstOrDefault(s => s.Status_id == 3);

                oppgave.Status = status;
                context.SaveChanges();
            }
            Response.Redirect("/OversiktOppgaver.aspx?mine=true");
        }
    }
}