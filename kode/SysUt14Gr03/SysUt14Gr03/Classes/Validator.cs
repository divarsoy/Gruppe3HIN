using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using SysUt14Gr03;
using SysUt14Gr03.Models;
using SysUt14Gr03.Classes;

namespace SysUt14Gr03.Classes
{
    public static class Validator
    {
        public static int KonverterTilTall (string tekst){
            string kunTall = Regex.Replace(tekst, @"\D", "");
            int resultat;
            bool vellykketKonvertering = int.TryParse(kunTall, out resultat);
            if (vellykketKonvertering)
                return resultat;
            else return -1;
        }

        public static bool isDateTime(string dato)
        {
            DateTime resultat;
            bool vellykketKonvertering = DateTime.TryParse(dato, out resultat);
            return vellykketKonvertering;
        }

        public static bool SjekkRettighet(int bruker_id, Konstanter.rettighet rettighet) {
            Bruker bruker = Queries.GetBrukerMedRettighet(bruker_id, rettighet);
            if (bruker == null)
                return false;
            else
                return true;
        }

        // Sjekk om oppgave er avhengig av andre, returnerer id'en til denne
        public static int SjekkAvhengighet(int oppgave_id)
        {
            Oppgave oppgave = Queries.GetOppgave(oppgave_id);
            int oppgaveGruppeId = 0;
            int result = -1;
            if (oppgave.OppgaveGruppe_id.HasValue)
                oppgaveGruppeId = (int)oppgave.OppgaveGruppe_id;
            List<Oppgave> oppgaverIGruppe = Queries.GetOppgaverIOppgaveGruppe(oppgaveGruppeId);
            foreach (Oppgave o in oppgaverIGruppe)
            {
                // Sjekker prioritet på alle oppgavene i gruppen
                if (o.Prioritering_id < oppgave.Prioritering_id)
                    if (o.Status_id != 3) // Hvis denne oppgaven ikke er ferdig
                        result = o.Oppgave_id;
            }
            return result;
        }
    }
}