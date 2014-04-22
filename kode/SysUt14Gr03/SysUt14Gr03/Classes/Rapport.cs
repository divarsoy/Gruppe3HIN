using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SysUt14Gr03.Classes;
using SysUt14Gr03.Models;

namespace SysUt14Gr03.Classes
{
    public class Rapport
    {

        public static int TEAMRAPPORT = 0;
        public static int PROSJEKTRAPPORT = 1;
        public static int INDIVIDRAPPORT = 2;

        private Team team;
        private Prosjekt prosjekt;
        private Bruker bruker;
        private string info;
        private TimeSpan bruktTidProsjekt;
        private TimeSpan estimertTidProsjekt;
        private TimeSpan varighetProsjekt;
        private TimeSpan sumTimerForBruker;
        private int antallFerdigeOppgaver;
        private int antallDeltakerePaTeam;

        /// <summary>
        /// Lag en rapport
        /// </summary>
        /// <param name="type">Type rapport</param>
        /// <param name="_id">Id til det som skal med</param>
        public Rapport(int type, int _id)
        {
            info = "";
            bruktTidProsjekt = new TimeSpan();
            estimertTidProsjekt = new TimeSpan();
            varighetProsjekt = new TimeSpan();
            sumTimerForBruker = new TimeSpan();
            antallFerdigeOppgaver = 0;
            antallDeltakerePaTeam = 0;

            switch (type) {
                case 0:
                    {
                        team = Queries.GetTeamById(_id);
                        if (team != null)
                        {
                            info += "Navn på team: " + team.Navn;
                            info += "<br />Opprettet: " + team.Opprettet.ToShortDateString();
                            // Teamleder?
                            info += "<br /><h3>Deltakere:</h3>";
                            foreach (Bruker b in team.Brukere)
                                info += "<br /><t />" + b.ToString();

                            info += "<br /><h3>Prosjekter:</h3>";
                            foreach (Prosjekt p in team.Prosjekter)
                                info += "<br /><t />" + p.Navn;
                            antallDeltakerePaTeam = team.Brukere.Count;
                        }
                    }
                    break;
                case 1:
                    {
                        prosjekt = Queries.GetProsjekt(_id);
                        if (prosjekt != null)
                        {
                            info += "Navn på prosjekt: " + prosjekt.Navn;
                            info += "<br />Opprettet: " + prosjekt.Opprettet.ToShortDateString();
                            DateTime start = (DateTime)prosjekt.StartDato;
                            DateTime slutt = (DateTime)prosjekt.SluttDato;
                            info += "<br />Startdato: " + start.ToShortDateString();
                            info += "<br />Sluttdato: " + slutt.ToShortDateString();
                            Bruker prosjektLeder = Queries.GetBruker(prosjekt.Bruker_id);
                            info += "<br />Prosjektleder: " + prosjektLeder.ToString();

                            info += "<br /><h3>Team:</h3>";
                            info += "<br /><t />" + prosjekt.Team.Navn;

                            info += "<br /><h3>Oppgaver:</h3>";
                            foreach (Oppgave o in prosjekt.Oppgaver)
                            {
                                info += "<br /><tb />Navn: " + o.Tittel;
                                info += "<br /><t />ID: " + o.Oppgave_id;
                                info += "<br /><t />Opprettet: " + o.Opprettet.ToShortDateString();
                                info += "<br /><t />User story: " + o.UserStory;
                                info += "<br /><t />Krav: " + o.Krav;
                                int avhengigOppgave = Validator.SjekkAvhengighet(o.Oppgave_id);
                                info += "<br />" + "Avhengighet: " + (avhengigOppgave == -1 ? "Nei" : "Ja");
                                info += "<br /><t />Estimat: " + o.Estimat;
                                info += "<br /><t />Brukt tid: " + o.BruktTid;
                                Status status = Queries.GetStatus(o.Status_id);
                                info += "<br /><t />Status: " + status.Navn;
                                estimertTidProsjekt.Add((TimeSpan) o.Estimat);
                                List<Time> timeListe = Queries.GetTimerForOppgave(o.Oppgave_id);
                                foreach (Time t in timeListe)
                                    bruktTidProsjekt += t.Tid;
                            }

                        }

                    }
                    break;
                case 2:
                    {
                        bruker = Queries.GetBruker(_id);
                        if (bruker != null)
                        {
                            info += "Navn på bruker: " + bruker.ToString();
                            info += "<br />Internt brukernavn: " + bruker.Brukernavn;
                            info += "<br />Lagt til: " + bruker.Opprettet.ToShortDateString();
                            info += "<br />E-post: " + bruker.Epost;

                            info += "<br /><h3>Prosjekter:</h3>";
                            List<Prosjekt> prosjektListe = Queries.GetAlleAktiveProsjekterForBruker(_id);
                            foreach (Prosjekt p in prosjektListe)
                                info += "<br /><t />" + p.Navn;

                            info += "<br /><h3>Team:</h3>";
                            foreach (Team t in bruker.Teams)
                                info += "<br /><t />" + t.Navn;

                            info += "<br /><h3>Oppgaver:</h3>";

                            List<Time> timeListe = Queries.GetTimerForBruker(_id);
                            if (timeListe != null)
                            {
                                foreach (Oppgave oppgave in bruker.Oppgaver)
                                {
                                    foreach (Time time in timeListe)
                                    {
                                        if (time.Oppgave_id == oppgave.Oppgave_id)
                                        {
                                            sumTimerForBruker += time.Tid;
                                        }
                                    }
                                    info += "<br /><t />" + oppgave.Tittel + "Brukt tid: " + sumTimerForBruker.ToString();

                                }
                            }

                            List<Oppgave> ferdigeOppgaver = Queries.GetAlleFerdigeOppgaverForBruker(_id);
                            antallFerdigeOppgaver = ferdigeOppgaver.Count;

                            info += "<br /><h3>Hendelser:</h3>";
                            List<Logg> loggListe = Queries.GetLoggForBruker(_id);
                            foreach (Logg l in loggListe)
                                info += "<br /><t />" + l.Hendelse;
                            
                        }
                    }
                    break;
                default:
                    {
                        info += "Feil valg, bruk Rapport.<noke>";
                    }
                    break;
            }
                
            
        }

        public TimeSpan GetBruktTidPaProsjekt()
        {
            if (bruktTidProsjekt != null)
                return bruktTidProsjekt;
            else
                return new TimeSpan(0);
        }

        public TimeSpan GetEstimatForProsjekt()
        {
            return estimertTidProsjekt;
        }

        public TimeSpan GetProsjektvarighet()
        {
            if (prosjekt != null)
            {
                DateTime start = (DateTime)prosjekt.StartDato;
                DateTime slutt = (DateTime)prosjekt.SluttDato;
                return slutt - start;
            }
            else
            {
                return new TimeSpan(0);
            }
                
        }

        public TimeSpan GetSumTimerForBruker()
        {
            return sumTimerForBruker;
        }

        public int GetAntallFerdigeOppgaverForBruker()
        {
            return antallFerdigeOppgaver;
        }

        public int GetAntallTeammedlemmer()
        {
            return antallDeltakerePaTeam;
        }

        public override string ToString()
        {
            return info;
        }

    }
}