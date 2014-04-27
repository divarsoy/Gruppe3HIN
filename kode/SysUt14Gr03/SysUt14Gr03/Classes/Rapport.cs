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
        private string prosjektRapportForBruker;
        private TimeSpan bruktTidProsjekt;
        private TimeSpan estimertTidProsjekt;
        private TimeSpan sumTimerForBruker;
        private TimeSpan sumFullforteTimerForBruker;
        private int antallFerdigeOppgaver;
        private int antallOppgaver;
        private int antallDeltakerePaTeam;
        private int antallFaser;

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
                            List<Fase> faseListe = Queries.GetFaseForProsjekt(_id);
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

                            info += "<br /><h3>Faser:</h3>";

                            antallFaser = faseListe.Count;

                            foreach (Fase f in faseListe)
                            {
                                info += "<br /><tb />Navn: " + f.Navn;
                                info += "<br /><tb />Faseleder: " + f.Bruker.ToString();
                                info += "<br /><tb />Opprettet: " + f.Opprettet.ToShortDateString();
                                info += "<br /><tb />Startdato: " + f.Start.ToShortDateString();
                                info += "<br /><tb />Sluttdato: " + f.Stopp.ToShortDateString();
                                foreach (Oppgave o in f.Oppgaver)
                                {
                                    info += "<br /><tb /><h4>Navn: " + o.Tittel + "</h4>";
                                    info += "<br /><t />ID: " + o.RefOppgaveId;
                                    info += "<br /><t />Opprettet: " + o.Opprettet.ToShortDateString();
                                    info += "<br /><t />User story: " + o.UserStory;
                                    info += "<br /><t />Krav: " + o.Krav;
                                    int avhengigOppgave = Validator.SjekkAvhengighet(o.Oppgave_id);
                                    info += "<br />" + "Avhengighet: " + (avhengigOppgave == -1 ? "Nei" : "Ja");
                                    info += "<br /><t />Estimat: " + o.Estimat;

                                    int test1 = o.Oppgave_id;
                                    List<Time> timeListe = Queries.GetTimerForOppgave(o.Oppgave_id);
                                    TimeSpan bruktTid = new TimeSpan(0);
                                    foreach (Time t in timeListe)
                                    {
                                        bruktTidProsjekt += t.Tid;
                                        bruktTid += t.Tid;

                                    }
                                        
                                    info += "<br /><t />Brukt tid: " + bruktTid.ToString();
                                    Status status = Queries.GetStatus(o.Status_id);
                                    info += "<br /><t />Status: " + status.Navn;
                                    estimertTidProsjekt += (TimeSpan)o.Estimat;
                                    
                                }
                                info += "<hr />";

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
                            {
                                info += "<br /><t /><h4>" + p.Navn + "</h4>";
                                prosjektRapportForBruker += "<br />Navn: " + p.Navn;
                                // vi tar det sia
                                prosjektRapportForBruker += "<br />" + "Min rolle: " + (_id == p.Bruker_id ? "Prosjektleder" : "Utvikler");
                                // if faseleder prosjektRapportForBruker = faseleder
                            }      

                            info += "<br /><h3>Team:</h3>";
                            foreach (Team t in bruker.Teams)
                                info += "<br /><t />" + t.Navn;

                            info += "<br /><h3>Oppgaver:</h3>";

                            List<Time> timeListe = Queries.GetTimerForBruker(_id);
                            if (timeListe != null)
                            {
                                List<Oppgave> oppgaverTilBruker = Queries.GetAlleAktiveOppgaverForBruker(_id);
                                antallOppgaver = oppgaverTilBruker.Count;

                                foreach (Oppgave oppgave in oppgaverTilBruker)
                                {
                                    foreach (Time time in timeListe)
                                    {
                                        if (time.Oppgave_id == oppgave.Oppgave_id)
                                        {
                                            sumTimerForBruker += time.Tid;
                                        }
                                    }
                                    info += "<br /><t /><h4>" + oppgave.Tittel + " Brukt tid: " + sumTimerForBruker.ToString() + "</h4>";
                                    prosjektRapportForBruker += "<br /><t />" + oppgave.Tittel + " Brukt tid: " + sumTimerForBruker.ToString();
                                    info += "<br />Prosjekt: " + oppgave.Prosjekt.Navn;
                                    info += "<br />Fase: " + oppgave.Fase.Navn;

                                }
                            }

                            List<Oppgave> ferdigeOppgaver = Queries.GetAlleFerdigeOppgaverForBruker(_id);
                            antallFerdigeOppgaver = ferdigeOppgaver.Count;
                            foreach (Oppgave o in ferdigeOppgaver)
                                sumFullforteTimerForBruker += (TimeSpan) o.BruktTid;

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

        public int getAntallFaser()
        {
            return antallFaser;
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

        public string VisProsjektRapportForBruker()
        {

            return prosjektRapportForBruker;
        }

        public TimeSpan GetSumTimerForBruker()
        {
            return sumTimerForBruker;
        }

        public TimeSpan GetSumFullforteTimerForBruker()
        {
            return sumFullforteTimerForBruker;
        }

        public int GetAntallFerdigeOppgaverForBruker()
        {
            return antallFerdigeOppgaver;
        }

        public int GetAntallOppgaverForBruker()
        {
            return antallOppgaver;
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