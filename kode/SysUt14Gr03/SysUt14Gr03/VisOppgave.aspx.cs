using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
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
        private List<Kommentar> kommentarListe;

        protected void Page_Load(object sender, EventArgs e)
        {
            //SessionSjekk.sjekkForRettighetPaaInnloggetBruker(Konstanter.rettighet.Prosjektleder);

            //bruker_id = Validator.KonverterTilTall(Session["bruker_id"].ToString());
            bruker_id = 2;


            if (Request.QueryString["oppgave_id"] != null)
            {
                oppgave_id = Validator.KonverterTilTall(Request.QueryString["oppgave_id"]);
                oppgave = Queries.GetOppgave(oppgave_id);
                if (oppgave != null)
                {
                    bool isDeltaker = false;
                    foreach (Bruker bruker in oppgave.Brukere)
                    {
                        if (bruker.Bruker_id == bruker_id)
                            isDeltaker = true;
                    }

                    if (isDeltaker)
                    {
                        btnInviter.Visible = true;
                        btnReturn.Visible = true;
                    }
                    else
                    {
                        btnPameld.Visible = true;
                    }

                    lblNavn.Text = oppgave.Tittel;
                    lblInfo.Text = "User story: " + oppgave.UserStory;
                    lblInfo.Text += "<br />" + "Oppgave-ID: " + oppgave.Oppgave_id;
                    lblInfo.Text += "<br />" + "Krav: " + oppgave.Krav;
                    lblInfo.Text += "<br />" + "Estimat: " + oppgave.Estimat + " timer";
                    lblInfo.Text += "<br />" + "Resterende tid: " + oppgave.RemainingTime.ToString() + " timer";
                    lblInfo.Text += "<br />" + "Brukt tid: " + oppgave.BruktTid.ToString() + " timer";
                    lblInfo.Text += "<br />" + "Tidsfrist: " + oppgave.Tidsfrist == null ? "Nei" : oppgave.Tidsfrist.ToString();
                    lblInfo.Text += "<br />" + "Status: " + oppgave.Status.Navn;
                    lblInfo.Text += "<br />" + "Prioritet: " + oppgave.Prioritering_id;
                    int avhengigOppgave = Validator.SjekkAvhengighet(oppgave_id);
                    lblInfo.Text += "<br />" + "Avhengighet: " + (avhengigOppgave == -1 ? "Nei" : "<a href=\"visOppgave?oppgave_id=" + avhengigOppgave + "\">" + "Ja, vis oppgave" + "</a>");
                    lblInfo.Text += "<br />" + "Dato opprettet: " + oppgave.Opprettet;
                    lblInfo.Text += "<br />" + "Dato endret: " + oppgave.Oppdatert == null ? "Ikke endret" : oppgave.Oppdatert.ToString();
                    lblInfo.Text += "<br />" + "Prosjekt: " + "<a href=\"visProsjekt?Prosjekt_id=" + oppgave.Prosjekt_id + "\">" + oppgave.Prosjekt.Navn + "</a>";
                    lblInfo.Text += "<hr />" + "Deltaker(e) ";
                    foreach (Bruker bruker in oppgave.Brukere)
                    {
                        lblInfo.Text += "<br /><a href=\"VisBruker.aspx?bruker_id=" + bruker.Bruker_id + "\">" + bruker.IM + "</a>";
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
            lblKommentarteller.Text = "Kommentarer (" + kommentarListe.Count + ")";
            txtKommentar.Visible = true;
            btnKommentar.Visible = true;
            lblKommentarteller.Visible = true;
            if (kommentarListe.Count > 0)
            {
                lblKommentarer.Text = "";
                for (int i = kommentarListe.Count - 1; i >= 0; i--)
                {
                    lblKommentarer.Text += "<hr /> <a href=\"VisBruker.aspx?bruker_id=" + kommentarListe[i].Bruker.Bruker_id + "\">" + kommentarListe[i].Bruker.IM + "</a>";
                    lblKommentarer.Text += "<br />" + kommentarListe[i].Opprettet.ToString();
                    lblKommentarer.Text += "<br />" + kommentarListe[i].Tekst;
                    if (sjekkNavn)
                    {
                        if (kommentarListe[i].Tekst.Contains("@"))
                        {
                            MatchCollection match = Regex.Matches(kommentarListe[i].Tekst, @"(?<!\w)@\w+");
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
                                                Varsel.SendVarsel(bruker.Bruker_id, Varsel.KOMMENTARVARSEL, "Kommentar", fornavn + " har nevnt deg i en kommentar");
                                            }
                                            
                                        }
                                    }
                                }
                            }
                            txtKommentar.Text = "";

                        }

                    }
                    
                }
                lblKommentarer.Visible = true;
                
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
    }
}