using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using SysUt14Gr03.Models;

namespace SysUt14Gr03.Classes
{
    public class DataTabeller
    {

        public static DataTable OversiktBrukere(List<Bruker> query)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("bruker_id", typeof(System.Int32)));
            dt.Columns.Add(new DataColumn("Fornavn", typeof(System.String)));
            dt.Columns.Add(new DataColumn("Etternavn", typeof(System.String)));

            foreach (Bruker bruker in query)
            {
                DataRow row = dt.NewRow();
                row["bruker_id"] = bruker.Bruker_id;
                row["Fornavn"] = bruker.Fornavn;
                row["Etternavn"] = bruker.Etternavn;

                dt.Rows.Add(row);

            }
            return dt;
        }
        public static DataTable SprintBacklogFase(Fase fase)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("Fase", typeof(System.String)));
            dt.Columns.Add(new DataColumn("Oppgave-ID", typeof(System.String)));
            dt.Columns.Add(new DataColumn("Oppgave", typeof(System.String)));
            dt.Columns.Add(new DataColumn("Estimert tid", typeof(System.TimeSpan)));
            dt.Columns.Add(new DataColumn("Brukt tid", typeof(System.TimeSpan)));
            dt.Columns.Add(new DataColumn("Gjenstående tid", typeof(System.TimeSpan)));
            dt.Columns.Add(new DataColumn("Ansvarlig Bruker", typeof(System.String)));
            dt.Columns.Add(new DataColumn("Status", typeof(System.String)));

            TimeSpan bruktTid = new TimeSpan(0);
            TimeSpan estimertTid = new TimeSpan(0);
            TimeSpan gjenstaendeTid = new TimeSpan(0);
         
            DataRow sumRow = dt.NewRow();

            foreach (Oppgave o in fase.Oppgaver)
            {
                DataRow row = dt.NewRow();
                row["fase"] = fase.Navn;
                row["Oppgave-ID"] = o.RefOppgaveId;
                row["Oppgave"] = o.Tittel;
                row["Estimert tid"] = o.Estimat;
                row["Brukt tid"] = o.BruktTid;
                row["Gjenstående tid"] = o.RemainingTime;
                row["Ansvarlig Bruker"] = fase.Bruker.ToString();
                string status = Queries.GetStatus(o.Status_id).Navn;
                row["Status"] = status;               
                bruktTid += (TimeSpan)o.BruktTid;    
                estimertTid += (TimeSpan)o.Estimat;   
                gjenstaendeTid += (TimeSpan)o.RemainingTime;
                dt.Rows.Add(row);
            }

            sumRow["Oppgave"] = "Sum: ";
            sumRow["Estimert tid"] = estimertTid;
            sumRow["Brukt tid"] = bruktTid;
            sumRow["Gjenstående tid"] = gjenstaendeTid;
            dt.Rows.Add(sumRow);

            return dt;

            
        }
        public static DataTable ProductBacklogProsjekt(Prosjekt prosjekt)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("Oppgave-Id", typeof(System.String)));
            dt.Columns.Add(new DataColumn("Oppgavenavn", typeof(System.String)));
            dt.Columns.Add(new DataColumn("Fase", typeof(System.String)));
            dt.Columns.Add(new DataColumn("Status", typeof(System.String)));

            foreach (Oppgave o in prosjekt.Oppgaver)
            {
                DataRow row = dt.NewRow();
                row["Oppgave-Id"] = o.RefOppgaveId;
                row["Oppgavenavn"] = o.Tittel;
                row["Fase"] = o.Fase.Navn;
                string status = Queries.GetStatus(o.Status_id).Navn;
                row["Status"] = status;

                dt.Rows.Add(row);
            }
            return dt;
        }
        public static DataTable OversiktLoggAdministrator(List<Logg> query)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("Logg id", typeof(System.Int32)));
            dt.Columns.Add(new DataColumn("Hendelse", typeof(System.String)));
            dt.Columns.Add(new DataColumn("Dato opprettet", typeof(System.DateTime)));
            dt.Columns.Add(new DataColumn("Bruker id", typeof(System.Int32)));

            foreach (Logg bruker in query)
            {
                DataRow row = dt.NewRow();
                row["Logg id"] = bruker.Logg_id;
                row["Hendelse"] = bruker.Hendelse;
                row["Dato opprettet"] = bruker.Opprettet;
                row["Bruker id"] = bruker.bruker_id;

                dt.Rows.Add(row);
            }
            return dt;

        }

        public static DataTable GetRapport(int type, int _id)
        {
            string info = "";
            TimeSpan bruktTidProsjekt = new TimeSpan();
            TimeSpan estimertTidProsjekt = new TimeSpan();
            TimeSpan sumTimerForBruker = new TimeSpan();
            int antallFerdigeOppgaver = 0;
            int antallDeltakerePaTeam = 0;

            DataTable dt = new DataTable();

            switch (type)
            {
                case 0:
                    {
                        
                        dt.Columns.Add(new DataColumn("Team id", typeof(System.String)));
                        dt.Columns.Add(new DataColumn("Navn på team", typeof(System.String)));
                        dt.Columns.Add(new DataColumn("Dato opprettet", typeof(System.String)));


                        Team team = Queries.GetTeamById(_id);
                        if (team != null)
                        {
                            info += "Navn på team: " + team.Navn;
                            info += "<br />Opprettet: " + team.Opprettet.ToShortDateString();
                            DataRow rowMain = dt.NewRow();
                            rowMain["Team id"] = team.Team_id.ToString();
                            rowMain["Navn på team"] = team.Navn;
                            rowMain["Dato opprettet"] = team.Opprettet.ToShortDateString();

                            dt.Rows.Add(rowMain);
                            // Teamleder?
                            info += "<br /><h3>Deltakere:</h3>";
                            foreach (Bruker b in team.Brukere)
                                info += "<br /><t />" + b.ToString();

                            //dt.Columns.Add(new DataColumn("Bruker id", typeof(System.Int32)));
                            //dt.Columns.Add(new DataColumn("Navn", typeof(System.String)));

                            info += "<br /><h3>Prosjekter:</h3>";
                            //dt.Columns.Add(new DataColumn("Prosjekt id", typeof(System.Int32)));
                            //dt.Columns.Add(new DataColumn("Prosjektnavn", typeof(System.String)));
                            //dt.Columns.Add(new DataColumn("Faseleder", typeof(System.String)));

                            DataRow rowLuft1 = dt.NewRow();
                            rowLuft1["Team id"] = "";
                            dt.Rows.Add(rowLuft1);

                            DataRow rowProsjekt = dt.NewRow();
                            rowProsjekt["Team id"] = "Prosjekt id";
                            rowProsjekt["Navn på team"] = "Prosjektnavn";
                            rowProsjekt["Dato opprettet"] = "Faseleder";
                            dt.Rows.Add(rowProsjekt);

                            foreach (Prosjekt p in team.Prosjekter)
                            {
                                info += "<br /><t />" + p.Navn;
                                Bruker faseleder = SessionSjekk.GetFaseleder(p.Prosjekt_id);

                                DataRow prosjektRow = dt.NewRow();
                                prosjektRow["Team id"] = p.Prosjekt_id.ToString();
                                prosjektRow["Navn på team"] = p.Navn;
                                if (faseleder != null)
                                    prosjektRow["Dato opprettet"] = SessionSjekk.GetFaseleder(p.Prosjekt_id).ToString();
                                else
                                    prosjektRow["Dato opprettet"] = "Ikke satt";

                                dt.Rows.Add(prosjektRow);
                            }

                            DataRow rowLuft2 = dt.NewRow();
                            rowLuft2["Team id"] = "";
                            dt.Rows.Add(rowLuft2);

                            DataRow rowBruker = dt.NewRow();
                            rowBruker["Team id"] = "Bruker id";
                            rowBruker["Navn på team"] = "Brukernavn";
                            dt.Rows.Add(rowBruker);

                            foreach (Bruker bruker in team.Brukere)
                            {
                                DataRow brukerRow = dt.NewRow();
                                brukerRow["Team id"] = bruker.Bruker_id.ToString();
                                brukerRow["Navn på team"] = bruker.ToString();
                                dt.Rows.Add(brukerRow);
                            }

                            antallDeltakerePaTeam = team.Brukere.Count;

                        }
                    }
                    break;
                case 1:
                    {
                        Prosjekt prosjekt = Queries.GetProsjekt(_id);
                        if (prosjekt != null)
                        {
                            dt.Columns.Add(new DataColumn("Prosjekt id", typeof(System.String)));
                            dt.Columns.Add(new DataColumn("Navn på prosjekt", typeof(System.String)));
                            dt.Columns.Add(new DataColumn("Dato opprettet", typeof(System.String)));
                            dt.Columns.Add(new DataColumn("Startdato", typeof(System.String)));
                            dt.Columns.Add(new DataColumn("Sluttdato", typeof(System.String)));
                            dt.Columns.Add(new DataColumn("Prosjektleder", typeof(System.String)));
                            dt.Columns.Add(new DataColumn("Team", typeof(System.String)));

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

                            DataRow rowMain = dt.NewRow();
                            rowMain["Prosjekt id"] = prosjekt.Prosjekt_id.ToString();
                            rowMain["Navn på prosjekt"] = prosjekt.Navn;
                            rowMain["Dato opprettet"] = prosjekt.Opprettet.ToShortDateString();
                            rowMain["Startdato"] = start.ToShortDateString();
                            rowMain["Sluttdato"] = slutt.ToShortDateString();
                            rowMain["Prosjektleder"] = prosjektLeder.ToString();
                            rowMain["Team"] = prosjekt.Team.Navn;

                            dt.Rows.Add(rowMain);

                            info += "<br /><h3>Faser:</h3>";

                            int antallFaser = faseListe.Count;

                            DataRow rowLuft1 = dt.NewRow();
                            rowLuft1["Prosjekt id"] = "";
                            dt.Rows.Add(rowLuft1);

                            DataRow rowFase = dt.NewRow();
                            rowFase["Prosjekt id"] = "Fasenavn";
                            rowFase["Navn på prosjekt"] = "Faseleder";
                            rowFase["Dato opprettet"] = "Opprettet";
                            rowFase["Startdato"] = "Startdato";
                            rowFase["Sluttdato"] = "Sluttdato";
                            dt.Rows.Add(rowFase);

                            foreach (Fase f in faseListe)
                            {

                                info += "<br /><tb />Navn: " + f.Navn;
                                info += "<br /><tb />Faseleder: " + f.Bruker.ToString();
                                info += "<br /><tb />Opprettet: " + f.Opprettet.ToShortDateString();
                                info += "<br /><tb />Startdato: " + f.Start.ToShortDateString();
                                info += "<br /><tb />Sluttdato: " + f.Stopp.ToShortDateString();

                                DataRow faseRow = dt.NewRow();
                                faseRow["Prosjekt id"] = f.Navn;
                                faseRow["Navn på prosjekt"] = f.Bruker.ToString();
                                faseRow["Dato opprettet"] = f.Opprettet.ToShortDateString();
                                faseRow["Startdato"] = f.Start.ToShortDateString();
                                faseRow["Sluttdato"] = f.Stopp.ToShortDateString();

                                dt.Rows.Add(faseRow);

                                DataRow rowOppgave = dt.NewRow();
                                rowOppgave["Prosjekt id"] = "Navn på oppgave";
                                rowOppgave["Navn på prosjekt"] = "ID";
                                rowOppgave["Dato opprettet"] = "Opprettet";
                                rowOppgave["Startdato"] = "Avhengighet";
                                rowOppgave["Sluttdato"] = "Estimat";
                                rowOppgave["Prosjektleder"] = "Brukt tid";
                                rowOppgave["Team"] = "Status";
                                dt.Rows.Add(rowOppgave);

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

                                    DataRow oppgaveRow = dt.NewRow();
                                    oppgaveRow["Prosjekt id"] = o.Tittel;
                                    oppgaveRow["Navn på prosjekt"] = o.RefOppgaveId;
                                    oppgaveRow["Dato opprettet"] = o.Opprettet.ToShortDateString();
                                    oppgaveRow["Startdato"] = (avhengigOppgave == -1 ? "Nei" : "Ja");
                                    oppgaveRow["Sluttdato"] = o.Estimat.ToString();
                                    oppgaveRow["Prosjektleder"] = o.BruktTid.ToString();
                                    oppgaveRow["Team"] = status.Navn;
                                    dt.Rows.Add(oppgaveRow);

                                }
                                info += "<hr />";

                                DataRow rowLuft2 = dt.NewRow();
                                rowLuft2["Prosjekt id"] = "";
                                dt.Rows.Add(rowLuft2);

                            }

                        }

                    }
                    break;
                case 2:
                    {
                        Bruker bruker = Queries.GetBruker(_id);
                        string prosjektRapportForBruker = "";
                        if (bruker != null)
                        {
                            dt.Columns.Add(new DataColumn("Navn på bruker", typeof(System.String)));
                            dt.Columns.Add(new DataColumn("Internt brukernavn", typeof(System.String)));
                            dt.Columns.Add(new DataColumn("Lagt til", typeof(System.String)));
                            dt.Columns.Add(new DataColumn("E-post", typeof(System.String)));

                            info += "Navn på bruker: " + bruker.ToString();
                            info += "<br />Internt brukernavn: " + bruker.Brukernavn;
                            info += "<br />Lagt til: " + bruker.Opprettet.ToShortDateString();
                            info += "<br />E-post: " + bruker.Epost;

                            DataRow rowMain = dt.NewRow();
                            rowMain["Navn på bruker"] = bruker.ToString();
                            rowMain["Internt brukernavn"] = bruker.Brukernavn;
                            rowMain["Lagt til"] = bruker.Opprettet.ToShortDateString();
                            rowMain["E-post"] = bruker.Epost;

                            dt.Rows.Add(rowMain);

                            DataRow rowLuft1 = dt.NewRow();
                            rowLuft1["Navn på bruker"] = "";
                            dt.Rows.Add(rowLuft1);

                            DataRow rowProsjekt = dt.NewRow();
                            rowProsjekt["Navn på bruker"] = "Prosjektnavn";
                            rowProsjekt["Internt brukernavn"] = "Rolle";
                            dt.Rows.Add(rowProsjekt);

                            info += "<br /><h3>Prosjekter:</h3>";
                            List<Prosjekt> prosjektListe = Queries.GetAlleAktiveProsjekterForBruker(_id);
                            foreach (Prosjekt p in prosjektListe)
                            {
                                info += "<br /><t /><h4>" + p.Navn + "</h4>";
                                prosjektRapportForBruker += "<br />Navn: " + p.Navn;
                                // vi tar det sia
                                string rolle;
                                if (SessionSjekk.IsFaseleder(_id, p.Prosjekt_id))
                                    rolle = "Faseleder";
                                else
                                    rolle = (_id == p.Bruker_id ? "Prosjektleder" : "Utvikler");

                                DataRow prosjektRow = dt.NewRow();
                                prosjektRow["Navn på bruker"] = p.Navn;
                                prosjektRow["Internt brukernavn"] = rolle;

                                dt.Rows.Add(prosjektRow);
                            }

                            DataRow rowLuft2 = dt.NewRow();
                            rowLuft2["Navn på bruker"] = "";
                            dt.Rows.Add(rowLuft2);

                            DataRow rowTeam = dt.NewRow();
                            rowTeam["Navn på bruker"] = "Medlem i team";
                            dt.Rows.Add(rowTeam);

                            info += "<br /><h3>Team:</h3>";
                            foreach (Team t in bruker.Teams)
                            {
                                info += "<br /><t />" + t.Navn;
                                DataRow teamRow = dt.NewRow();
                                teamRow["Navn på bruker"] = t.Navn;
                                dt.Rows.Add(teamRow);
                            }

                            DataRow rowLuft3 = dt.NewRow();
                            rowLuft3["Navn på bruker"] = "";
                            dt.Rows.Add(rowLuft3);

                            DataRow rowOppgave = dt.NewRow();
                            rowOppgave["Navn på bruker"] = "Navn på oppgave";
                            rowOppgave["Internt brukernavn"] = "Estimert tid";
                            rowOppgave["Lagt til"] = "Brukt tid";
                            rowOppgave["E-post"] = "Gjenstående tid";
                            dt.Rows.Add(rowOppgave);

                            info += "<br /><h3>Oppgaver:</h3>";

                            List<Time> timeListe = Queries.GetTimerForBruker(_id);
                            if (timeListe != null)
                            {
                                List<Oppgave> oppgaverTilBruker = Queries.GetAlleAktiveOppgaverForBruker(_id);
                                int antallOppgaver = oppgaverTilBruker.Count;

                                foreach (Oppgave oppgave in oppgaverTilBruker)
                                {
                                    DataRow oppgaveRow = dt.NewRow();
                                    oppgaveRow["Navn på bruker"] = oppgave.RefOppgaveId + " " +oppgave.Tittel;
                                    oppgaveRow["Internt brukernavn"] = oppgave.Estimat.ToString();
                                    oppgaveRow["Lagt til"] = oppgave.BruktTid.ToString();
                                    oppgaveRow["E-post"] = (oppgave.Estimat - oppgave.BruktTid).ToString();

                                    dt.Rows.Add(oppgaveRow);

                                    info += "<br /><t /><h4>" + oppgave.Tittel + " Brukt tid: " + sumTimerForBruker.ToString() + "</h4>";
                                    prosjektRapportForBruker += "<br /><t />" + oppgave.Tittel + " Brukt tid: " + sumTimerForBruker.ToString();
                                    info += "<br />Prosjekt: " + oppgave.Prosjekt.Navn;
                                    info += "<br />Fase: " + oppgave.Fase.Navn;

                                }
                            }

                            DataRow rowLuft4 = dt.NewRow();
                            rowLuft4["Navn på bruker"] = "";
                            dt.Rows.Add(rowLuft4);


                            List<Oppgave> ferdigeOppgaver = Queries.GetAlleFerdigeOppgaverForBruker(_id);
                            antallFerdigeOppgaver = ferdigeOppgaver.Count;
                            TimeSpan sumFullforteTimerForBruker = new TimeSpan();
                            foreach (Oppgave o in ferdigeOppgaver)
                            {
                                sumFullforteTimerForBruker += (TimeSpan)o.BruktTid;
                            }


                            DataRow rowLogg = dt.NewRow();
                            rowLogg["Navn på bruker"] = "Hendelse";
                            dt.Rows.Add(rowLogg);

                            info += "<br /><h3>Hendelser:</h3>";
                            List<Logg> loggListe = Queries.GetLoggForBruker(_id);
                            foreach (Logg l in loggListe)
                            {
                                DataRow oppgaveRow = dt.NewRow();
                                oppgaveRow["Navn på bruker"] = l.Hendelse;
                                dt.Rows.Add(oppgaveRow);
                                info += "<br /><t />" + l.Hendelse;
                            }
                                

                        }
                    }
                    break;
                default:
                    {
                        info += "Feil valg, bruk Rapport.<noke>";
                    }
                    break;
            }
            return dt;
        }

        public static DataTable GetProsjektRapportForBruker(int bruker_id, int prosjekt_id) {

            DataTable dt = new DataTable();


            Bruker bruker = Queries.GetBruker(bruker_id);
            string prosjektRapportForBruker = "";
            if (bruker != null)
            {
                dt.Columns.Add(new DataColumn("Navn på prosjekt", typeof(System.String)));
                dt.Columns.Add(new DataColumn("Rolle", typeof(System.String)));
                dt.Columns.Add(new DataColumn("Prosjektleder", typeof(System.String)));
                dt.Columns.Add(new DataColumn("Dato opprettet", typeof(System.String)));
                dt.Columns.Add(new DataColumn("Startdato", typeof(System.String)));
                dt.Columns.Add(new DataColumn("Sluttdato", typeof(System.String)));

                //info += "Navn på bruker: " + bruker.ToString();
                //info += "<br />Internt brukernavn: " + bruker.Brukernavn;
                //info += "<br />E-post: " + bruker.Epost;



                //info += "<br /><h3>Prosjekter:</h3>";
                List<Prosjekt> prosjektListe = Queries.GetAlleAktiveProsjekterForBruker(bruker_id);
                foreach (Prosjekt p in prosjektListe)
                {

                   

                    //info += "<br /><t /><h4>" + p.Navn + "</h4>";
                    prosjektRapportForBruker += "<br />Navn: " + p.Navn;
                    // vi tar det sia
                    string rolle;
                    if (SessionSjekk.IsFaseleder(bruker_id, p.Prosjekt_id))
                        rolle = "Faseleder";
                    else
                        rolle = (bruker_id == p.Bruker_id ? "Prosjektleder" : "Utvikler");

                    DataRow prosjektRow = dt.NewRow();
                    prosjektRow["Navn på prosjekt"] = p.Navn;
                    prosjektRow["Rolle"] = rolle;
                    prosjektRow["Prosjektleder"] = p.Bruker.ToString();
                    prosjektRow["Dato opprettet"] = p.Opprettet.ToShortDateString();
                    prosjektRow["Startdato"] = ((DateTime)p.StartDato).ToShortDateString();
                    prosjektRow["Sluttdato"] = ((DateTime)p.SluttDato).ToShortDateString();
                    dt.Rows.Add(prosjektRow);

                    List<Oppgave> oppgaverTilBruker = Queries.GetAlleAktiveOppgaverForProsjektOgBruker(p.Prosjekt_id, bruker_id);

                    DataRow oppgaveRow = dt.NewRow();
                    oppgaveRow["Navn på prosjekt"] = "Navn på oppgave";
                    oppgaveRow["Rolle"] = "Estimert tid";
                    oppgaveRow["Prosjektleder"] = "Brukt tid";
                    oppgaveRow["Dato opprettet"] = "Resterende tid";
                    dt.Rows.Add(oppgaveRow);

                    foreach (Oppgave o in oppgaverTilBruker)
                    {
                        DataRow rowOppgave = dt.NewRow();
                        rowOppgave["Navn på prosjekt"] = o.RefOppgaveId + " " + o.Tittel; ;
                        rowOppgave["Rolle"] = o.Estimat;
                        rowOppgave["Prosjektleder"] = o.BruktTid;
                        rowOppgave["Dato opprettet"] = (o.Estimat - o.BruktTid).ToString(); ;
                        dt.Rows.Add(rowOppgave);
                    }

                    DataRow rowLuft2 = dt.NewRow();
                    rowLuft2["Navn på prosjekt"] = "";
                    dt.Rows.Add(rowLuft2);

                }

            }


            return dt;
        }

    }
}