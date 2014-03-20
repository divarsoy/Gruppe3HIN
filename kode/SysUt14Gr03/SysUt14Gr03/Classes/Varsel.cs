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

        static public void SendVarsel(int bruker_id, int varsel)
        {
            SendVarsel(bruker_id, varsel, "");
            
        }

        static public void SendVarsel(int bruker_id, int varsel, string melding)
        {
            SendVarsel(bruker_id, varsel, "", melding);

        }

        static public void SendVarsel(int bruker_id, int varsel, string tittel, string melding)
        {
            // Sjekker brukerpreferanser
            Bruker bruker = Queries.GetBruker(bruker_id);
            BrukerPreferanse brukerPrefs = Queries.GetEpostPreferanser(bruker_id);
            bool[] selectedItems = new bool[6];
            selectedItems[0] = brukerPrefs.EpostTeam;
            selectedItems[1] = brukerPrefs.EpostProsjekt;
            selectedItems[2] = brukerPrefs.EpostOppgave;
            selectedItems[3] = brukerPrefs.EpostKommentar;
            selectedItems[4] = brukerPrefs.EpostTidsfrist;
            selectedItems[5] = brukerPrefs.EpostRapport;

            melding = melding == "" ? VARSELTEKST[varsel] : melding;
            tittel = tittel == "" ? "Varsel angående " + VARSELTEKST[varsel] : tittel;

            if (selectedItems[varsel])
            {
                string epost = bruker.Epost;              
                // Sender e-post
                sendEmail sendEmail = new sendEmail();
                sendEmail.sendEpost(epost, melding, tittel, null, null, null);
            }

            // Sender intern varsel (kommer senere)
        }
    }
}