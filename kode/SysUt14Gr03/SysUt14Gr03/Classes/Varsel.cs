using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SysUt14Gr03.Models;

namespace SysUt14Gr03.Classes
{
    public static class Varsel
    {
        // Hvilken type varsel
        public static int TEAMVARSEL = 0;
        public static int PROSJEKTVARSEL = 1;
        public static int OPPGAVEVARSEL = 2;
        public static int KOMMENTARVARSEL = 3;
        public static int TIDSFRISTVARSEL = 4;
        public static int RAPPORTVARSEL = 5;
        // Index samsvarer med type varsel
        public static string[] VARSELTITTEL =  {"team",
                                                "prosjekt",
                                                "oppgave",
                                                "kommentar",
                                                "tidsfrist",
                                                "rapport"
                                              };
        public static string[] VARSELTEKST =  { "Du har blitt lagt til i et team",
                                                "Du har blitt lagt til i et prosjekt",
                                                "Du har blitt tildelt en oppgave",
                                                "Du har blitt nevnt i en kommentar",
                                                "Tidsfrist på oppgave",
                                                "Du har blitt nevnt i en rapport"
                                              };

        /// <summary>
        /// Sender varsel til bruker med bruker_id, send med varseltype
        /// som sjekkes mot epostpreferanser.
        /// </summary>
        /// <param name="bruker_id">ID til mottaker</param>
        /// <param name="varsel">Hvilken type varsel, bruk Varsel.VARSELTYPE</param>
        static public void SendVarsel(int bruker_id, int varsel)
        {
            SendVarsel(bruker_id, varsel, "");
            
        }

        /// <summary>
        /// Sender varsel til bruker med bruker_id, send med varseltype
        /// som sjekkes mot epostpreferanser og melding.
        /// </summary>
        /// <param name="bruker_id">ID til mottaker</param>
        /// <param name="varsel">Hvilken type varsel, bruk Varsel.VARSELTYPE</param>
        /// <param name="melding">Meldingstekst</param>
        static public void SendVarsel(int bruker_id, int varsel, string melding)
        {
            SendVarsel(bruker_id, varsel, "", melding);

        }

        /// <summary>
        /// Sender varsel til bruker med bruker_id, send med varseltype
        /// som sjekkes mot epostpreferanser, melding og meldingstittel.
        /// </summary>
        /// <param name="bruker_id">ID til mottaker</param>
        /// <param name="varsel">Hvilken type varsel, bruk Varsel.VARSELTYPE</param>
        /// <param name="melding">Meldingstekst</param>
        /// <param name="melding">Tittel, vises som epostemne.</param>
        static public void SendVarsel(int bruker_id, int varsel, string tittel, string melding)
        {
            SendVarsel(bruker_id, varsel, tittel, melding, 1);
        }

        /// <summary>
        /// Sender varsel til bruker med bruker_id, send med varseltype
        /// som sjekkes mot epostpreferanser, melding, meldingstittel
        /// og en oppgave_id. Brukes kun ved invitasjon.
        /// </summary>
        /// <param name="bruker_id">ID til mottaker</param>
        /// <param name="varsel">Hvilken type varsel, bruk Varsel.VARSELTYPE</param>
        /// <param name="melding">Meldingstekst</param>
        /// <param name="melding">Tittel, vises som epostemne.</param>
        /// <param name="notifikasjonstype">hvilken type notifikasjon(se database)</param>
        static public void SendVarsel(int bruker_id, int varsel, string tittel, string melding, int notifikasjonstype)
        {
            SendVarsel(bruker_id, varsel, tittel, melding, -1, notifikasjonstype);
        }

        /// <summary>
        /// Sender varsel til bruker med bruker_id, send med varseltype
        /// som sjekkes mot epostpreferanser, melding, meldingstittel
        /// og en oppgave_id. Brukes kun ved invitasjon.
        /// </summary>
        /// <param name="bruker_id">ID til mottaker</param>
        /// <param name="varsel">Hvilken type varsel, bruk Varsel.VARSELTYPE</param>
        /// <param name="melding">Meldingstekst</param>
        /// <param name="melding">Tittel, vises som epostemne.</param>
        /// <param name="oppgave_id">oppgave_id til oppgaven mottaker inviteres til</param>
        /// /// <param name="notifikasjonstype">hvilken type notifikasjon(se database)</param>
        static public void SendVarsel(int bruker_id, int varsel, string tittel, string melding, int oppgave_id, int notifikasjonstype)
        {
            // Sjekker brukerpreferanser
            Bruker bruker = Queries.GetBruker(bruker_id);
            BrukerPreferanse brukerPrefs = Queries.GetBrukerPreferanse(bruker_id);
            bool[] selectedItems = new bool[6];
            if (brukerPrefs != null)
            {
                selectedItems[0] = brukerPrefs.EpostTeam;
                selectedItems[1] = brukerPrefs.EpostProsjekt;
                selectedItems[2] = brukerPrefs.EpostOppgave;
                selectedItems[3] = brukerPrefs.EpostKommentar;
                selectedItems[4] = brukerPrefs.EpostTidsfrist;
                selectedItems[5] = brukerPrefs.Sheperd;
            }   

            // Genererer melding + tittel
            melding = melding == "" ? VARSELTEKST[varsel] : melding;
            tittel = tittel == "" ? "Varsel angående " + VARSELTITTEL[varsel] : tittel;
            bool oppgave = oppgave_id > 0;

            if (oppgave)
            {
                Oppgave o = Queries.GetOppgave(oppgave_id);
                melding += Environment.NewLine + "<a href=\"http://malmen.hin.no/SysUt14Gr03/MottaOppgave.aspx?oppgave_id=" 
                    + oppgave_id + "&bruker_id=" + "\">" + o.Tittel + "</a>";
            }

            if (selectedItems[varsel])
            {
                string epost = bruker.Epost;              
                // Sender e-post
                sendEmail sendEmail = new sendEmail();
                sendEmail.sendEpost(epost, melding, tittel, null, null, null);
            }

            // Sender intern varsel
            using (var context = new Context())
            {
                bruker = context.Brukere.FirstOrDefault(b => b.Bruker_id == bruker_id);
                NotifikasjonsType type = context.NotifikasjonsType.FirstOrDefault(nt => nt.NotifikasjonsType_id == notifikasjonstype);
                    // Legger varsel inn i databasen:
                    var nyVarsel = new Notifikasjon
                    {
                        Melding = melding,
                        notifikasjonsType = type,
                        bruker = bruker
                    };

                    context.Notifikasjoner.Add(nyVarsel);
                    context.SaveChanges();
            }
        }

        /// <summary>
        /// Sender bruker en invitasjon til en oppgave
        /// </summary>
        /// <param name="mottaker_id">bruker_id til mottaker</param>
        /// <param name="avsender_id">bruker_id til avsender</param>
        /// <param name="oppgave_id">oppgave_id til oppgaven</param>
        /// <param name="melding">forklarende tekst til mottaker</param>
        static public string SendInvitasjon(int mottaker_id, int avsender_id, int oppgave_id, string melding)
        {
            // Sjekker brukerpreferanser
            Bruker mottaker = Queries.GetBruker(mottaker_id);
            BrukerPreferanse brukerPrefs = Queries.GetBrukerPreferanse(mottaker_id);

            bool oppgaveVarsel = brukerPrefs.EpostOppgave;

            // Genererer melding + tittel
            string tittel = mottaker.ToString() + " ønsker hjelp";

            Oppgave o = Queries.GetOppgave(oppgave_id);
            if (o != null)
            {
                melding += "<a href=\"http://malmen.hin.no/SysUt14Gr03/MottaOppgave.aspx?oppgave_id="
                    + oppgave_id + "&bruker_id=" + avsender_id + "\">" + o.Tittel + "</a>";
            }
                
            if (oppgaveVarsel)
            {
                string epost = mottaker.Epost;
                // Sender e-post
                sendEmail sendEmail = new sendEmail();
                sendEmail.sendEpost(epost, melding, tittel, null, null, null);
            }

            // Sender intern varsel
            using (var context = new Context())
            {
                mottaker = context.Brukere.FirstOrDefault(b => b.Bruker_id == mottaker_id);
                NotifikasjonsType type = context.NotifikasjonsType.FirstOrDefault(nt => nt.NotifikasjonsType_id == 1);
                // Legger varsel inn i databasen:
                var nyVarsel = new Notifikasjon
                {
                    Melding = melding,
                    notifikasjonsType = type,
                    bruker = mottaker
                };

                context.Notifikasjoner.Add(nyVarsel);
                context.SaveChanges();
            }
            // debug
            return melding;
        }
                

        /// <summary>
        /// Sender varsel til alle brukere i en liste, send med varseltype
        /// som sjekkes mot epostpreferanser.
        /// </summary>
        /// <param name="brukerListe">Liste med mottakere</param>
        /// <param name="varsel">Hvilken type varsel, bruk Varsel.VARSELTYPE</param>
        static public void SendVarsel(List<Bruker> brukerListe, int varsel)
        {
            foreach (Bruker bruker in brukerListe)
            {
                SendVarsel(bruker.Bruker_id, varsel, "");
            }           
        }

        /// <summary>
        /// Sender varsel til alle brukere i en liste, send med varseltype
        /// som sjekkes mot epostpreferanser og meldingstekst.
        /// </summary>
        /// <param name="brukerListe">Liste med mottakere</param>
        /// <param name="varsel">Hvilken type varsel, bruk Varsel.VARSELTYPE</param>
        /// <param name="melding">Meldingstekst</param>
        static public void SendVarsel(List<Bruker> brukerListe, int varsel, string melding)
        {
            foreach (Bruker bruker in brukerListe)
            {
                SendVarsel(bruker.Bruker_id, varsel, "", melding);
            }          
        }

        /// <summary>
        /// Sender varsel til alle brukere i en liste, send med varseltype
        /// som sjekkes mot epostpreferanser, melding og meldingstittel.
        /// </summary>
        /// <param name="brukerListe">Liste med mottakere</param>
        /// <param name="varsel">Hvilken type varsel, bruk Varsel.VARSELTYPE</param>
        /// <param name="melding">Meldingstekst</param>
        /// <param name="melding">Tittel, vises som epostemne.</param>
        static public void SendVarsel(List<Bruker> brukerListe, int varsel, string tittel, string melding)
        {
            foreach (Bruker bruker in brukerListe)
            {
                SendVarsel(bruker.Bruker_id, varsel, tittel, melding, 1);
            }
        }

        /// <summary>
        /// Sender varsel til alle brukere i en liste, send med varseltype
        /// som sjekkes mot epostpreferanser, melding, meldingstittel
        /// og en oppgave_id. Brukes kun ved invitasjon.
        /// </summary>
        /// <param name="brukerListe">Liste med mottakere</param>
        /// <param name="varsel">Hvilken type varsel, bruk Varsel.VARSELTYPE</param>
        /// <param name="melding">Meldingstekst</param>
        /// <param name="melding">Tittel, vises som epostemne.</param>
        /// <param name="oppgave_id">oppgave_id til oppgaven mottaker inviteres til</param>
        /// /// <param name="notifikasjonstype">hvilken type notifikasjon(se database)</param>
        static public void SendVarsel(List<Bruker> brukerListe, int varsel, string tittel, string melding, int notifikasjonstype)
        {
            foreach (Bruker bruker in brukerListe)
            {
                SendVarsel(bruker.Bruker_id, varsel, tittel, melding, -1, notifikasjonstype);
            }
        }

        /// <summary>
        /// Sender varsel til alle brukere i en liste, send med varseltype
        /// som sjekkes mot epostpreferanser, melding, meldingstittel
        /// og en oppgave_id. Brukes kun ved invitasjon.
        /// </summary>
        /// <param name="brukerListe">Liste med mottakere</param>
        /// <param name="varsel">Hvilken type varsel, bruk Varsel.VARSELTYPE</param>
        /// <param name="melding">Meldingstekst</param>
        /// <param name="melding">Tittel, vises som epostemne.</param>
        /// <param name="oppgave_id">oppgave_id til oppgaven mottaker inviteres til</param>
        /// /// <param name="notifikasjonstype">hvilken type notifikasjon(se database)</param>
        static public void SendVarsel(List<Bruker> brukerListe, int varsel, string tittel, string melding, int oppgave_id, int notifikasjonstype)
        {
            foreach (Bruker bruker in brukerListe)
            {
                SendVarsel(bruker.Bruker_id, varsel, tittel, melding, oppgave_id, notifikasjonstype);
            }
        }
    }
}