using DocumentFormat.OpenXml.Spreadsheet;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Windows.Forms;
using SysUt14Gr03.Models;
using System.Web.UI.DataVisualization.Charting;

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

                DataRow luft = dt.NewRow();
                luft["Fornavn"] = "";
                dt.Rows.Add(luft);
                dt.Rows.Add(row);

            }
            return dt;
        }
        /// <summary>
        /// Datatabell for sprintbacklog for en fase
        /// </summary>
        /// <param name="fase">fase</param>
        /// <returns></returns>
        public static DataTable SprintBacklogFase(Fase fase)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("ProsjektNavn", typeof(System.String)));
            dt.Columns.Add(new DataColumn("Fase", typeof(System.String)));
            dt.Columns.Add(new DataColumn("Oppgave-ID", typeof(System.String)));
            dt.Columns.Add(new DataColumn("Oppgave", typeof(System.String)));
            dt.Columns.Add(new DataColumn("Estimert tid", typeof(System.String)));
            dt.Columns.Add(new DataColumn("Brukt tid", typeof(System.String)));
            dt.Columns.Add(new DataColumn("Gjenstående tid", typeof(System.String)));
            dt.Columns.Add(new DataColumn("Ansvarlig Bruker", typeof(System.String)));
            dt.Columns.Add(new DataColumn("Status", typeof(System.String)));
            
            TimeSpan bruktTid = new TimeSpan(0);
            TimeSpan estimertTid = new TimeSpan(0);
            TimeSpan gjenstaendeTid = new TimeSpan(0);
         
            DataRow sumRow = dt.NewRow();
          

            foreach (Oppgave o in fase.Oppgaver)
            {
                          
                DataRow row = dt.NewRow();
                row["Prosjektnavn"] = o.Prosjekt.Navn;
                row["fase"] = fase.Navn;
                row["Oppgave-ID"] = o.RefOppgaveId;
                row["Oppgave"] = o.Tittel;
                row["Estimert tid"] = o.Estimat.ToString();
                row["Brukt tid"] = o.BruktTid.ToString();
                row["Gjenstående tid"] = o.RemainingTime.ToString();
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
        /// <summary>
        /// ProductBacklog for et prosjekt
        /// </summary>
        /// <param name="prosjekt">prosjekt</param>
        /// <returns></returns>
        public static DataTable ProductBacklogProsjekt(Prosjekt prosjekt)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("Prosjektnavn", typeof(System.String)));
            dt.Columns.Add(new DataColumn("Oppgave-Id", typeof(System.String)));
            dt.Columns.Add(new DataColumn("Oppgavenavn", typeof(System.String)));
            dt.Columns.Add(new DataColumn("Fase", typeof(System.String)));
            dt.Columns.Add(new DataColumn("Status", typeof(System.String)));

            foreach (Oppgave o in prosjekt.Oppgaver)
            {
                DataRow row = dt.NewRow();
                row["Prosjektnavn"] = o.Prosjekt.Navn;
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
            dt.Columns.Add(new DataColumn("Bruker navn", typeof(System.String)));
            dt.Columns.Add(new DataColumn("Prosjekt navn", typeof(System.String))); 
            dt.Columns.Add(new DataColumn("Dato opprettet", typeof(System.DateTime)));

            foreach (Logg bruker in query)
            {
                DataRow row = dt.NewRow();
                row["Logg id"] = bruker.Logg_id;
                row["Hendelse"] = bruker.Hendelse;
                row["Bruker navn"] = bruker.Bruker.Brukernavn;
                if (bruker.Prosjekt_id != null)
                {
                    Prosjekt prosjekt = Queries.GetProsjekt((int)bruker.Prosjekt_id);
                    row["Prosjekt navn"] = prosjekt.Navn;
                }
                row["Dato opprettet"] = bruker.Opprettet;
                

                dt.Rows.Add(row);
            }
            return dt;

        }

        /// <summary>
        /// Denne metoden returnerer en datatable for en rapport
        /// </summary>
        /// <param name="type">Hvilken type rapport</param>
        /// <param name="_id">Id til det som skal rapporteres</param>
        /// <returns></returns>
        public static DataTable GetRapport(int type, int _id)
        {
            TimeSpan bruktTidProsjekt = new TimeSpan();
            TimeSpan estimertTidProsjekt = new TimeSpan();
            int antallFerdigeOppgaver = 0;
            int antallDeltakerePaTeam = 0;

            DataTable dt = new DataTable();

            // Her er en switch. Den velger på type
            switch (type)
            {
                case 0:
                    {
                        
                        // Oppretter kolonner
                        dt.Columns.Add(new DataColumn("Team id", typeof(System.String)));
                        dt.Columns.Add(new DataColumn("Navn på team", typeof(System.String)));
                        dt.Columns.Add(new DataColumn("Dato opprettet", typeof(System.String)));

                        // Henter team
                        Team team = Queries.GetTeamById(_id);
                        if (team != null)
                        {
                            // Legger til en ny rad
                            DataRow rowMain = dt.NewRow();
                            rowMain["Team id"] = team.Team_id.ToString();
                            rowMain["Navn på team"] = team.Navn;
                            rowMain["Dato opprettet"] = team.Opprettet.ToShortDateString();

                            dt.Rows.Add(rowMain);

                            // Legger til mellomrom
                            DataRow rowLuft1 = dt.NewRow();
                            rowLuft1["Team id"] = "";
                            dt.Rows.Add(rowLuft1);

                            // Legger til rad
                            DataRow rowProsjekt = dt.NewRow();
                            rowProsjekt["Team id"] = "Prosjekt id";
                            rowProsjekt["Navn på team"] = "Prosjektnavn";
                            rowProsjekt["Dato opprettet"] = "Faseleder";
                            dt.Rows.Add(rowProsjekt);

                            foreach (Prosjekt p in team.Prosjekter)
                            {
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
                            DateTime start = (DateTime)prosjekt.StartDato;
                            DateTime slutt = (DateTime)prosjekt.SluttDato;
                            Bruker prosjektLeder = Queries.GetBruker(prosjekt.Bruker_id);

                            DataRow rowMain = dt.NewRow();
                            rowMain["Prosjekt id"] = prosjekt.Prosjekt_id.ToString();
                            rowMain["Navn på prosjekt"] = prosjekt.Navn;
                            rowMain["Dato opprettet"] = prosjekt.Opprettet.ToShortDateString();
                            rowMain["Startdato"] = start.ToShortDateString();
                            rowMain["Sluttdato"] = slutt.ToShortDateString();
                            rowMain["Prosjektleder"] = prosjektLeder.ToString();
                            rowMain["Team"] = prosjekt.Team.Navn;

                            dt.Rows.Add(rowMain);

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
                                    int avhengigOppgave = Validator.SjekkAvhengighet(o.Oppgave_id);

                                    int test1 = o.Oppgave_id;
                                    List<Time> timeListe = Queries.GetTimerForOppgave(o.Oppgave_id);
                                    TimeSpan bruktTid = new TimeSpan(0);
                                    foreach (Time t in timeListe)
                                    {
                                        bruktTidProsjekt += t.Tid;
                                        bruktTid += t.Tid;

                                    }

                                    Status status = Queries.GetStatus(o.Status_id);
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
                        if (bruker != null)
                        {
                            dt.Columns.Add(new DataColumn("Navn på bruker", typeof(System.String)));
                            dt.Columns.Add(new DataColumn("Internt brukernavn", typeof(System.String)));
                            dt.Columns.Add(new DataColumn("Lagt til", typeof(System.String)));
                            dt.Columns.Add(new DataColumn("E-post", typeof(System.String)));

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

                            List<Prosjekt> prosjektListe = Queries.GetAlleAktiveProsjekterForBruker(_id);
                            foreach (Prosjekt p in prosjektListe)
                            {
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

                            foreach (Team t in bruker.Teams)
                            {
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

                            List<Logg> loggListe = Queries.GetLoggForBruker(_id);
                            foreach (Logg l in loggListe)
                            {
                                DataRow oppgaveRow = dt.NewRow();
                                oppgaveRow["Navn på bruker"] = l.Hendelse;
                                dt.Rows.Add(oppgaveRow);
                            }
                                

                        }
                    }
                    break;
                default:
                    {

                    }
                    break;
            }
            return dt;
        }

        public static DataTable GetProsjektRapportForBruker(int bruker_id, int prosjekt_id) {

            DataTable dt = new DataTable();


            Bruker bruker = Queries.GetBruker(bruker_id);
            if (bruker != null)
            {
                dt.Columns.Add(new DataColumn("Navn på prosjekt", typeof(System.String)));
                dt.Columns.Add(new DataColumn("Rolle", typeof(System.String)));
                dt.Columns.Add(new DataColumn("Prosjektleder", typeof(System.String)));
                dt.Columns.Add(new DataColumn("Dato opprettet", typeof(System.String)));
                dt.Columns.Add(new DataColumn("Startdato", typeof(System.String)));
                dt.Columns.Add(new DataColumn("Sluttdato", typeof(System.String)));

                List<Prosjekt> prosjektListe = Queries.GetAlleAktiveProsjekterForBruker(bruker_id);
                foreach (Prosjekt p in prosjektListe)
                {

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

        /// <summary>
        /// Denne tabellen er for å eksportere burndownchart til excell
        /// </summary>
        public static DataTable BurnDownChartForFase(int faseId)
        {
            DataTable datatabell = new DataTable();
    //        Chart chart = BurnDownChartForFase.g

            Fase fase = Queries.GetFase(faseId);
            TimeSpan estimatForFase = new TimeSpan();
            TimeSpan totalSluttTid = new TimeSpan();
            TimeSpan totalAvvik = new TimeSpan();
            TimeSpan nullTimeSpan = new TimeSpan(0);
            List<TimeSpan> nyEstimertTidFase = new List<TimeSpan>();
            List<Oppgave> oppgaverForFase = Queries.getOppgaverIFase(faseId);

            List<DateTime> range = Enumerable.Range(0, (fase.Stopp - fase.Start).Days + 1)
                .Select(i => fase.Start.AddDays(i))
                .ToList();

            datatabell.Columns.Add("Oppgave ID", typeof(System.String));
            datatabell.Columns.Add("Oppgavenavn", typeof(System.String));
            datatabell.Columns.Add("Ansvarlig utvikler(e)", typeof(System.String));
            datatabell.Columns.Add("Estimat", typeof(System.String));
            for (int i = 0; i < range.Count; i++)
            {
                datatabell.Columns.Add(range.ElementAt(i).ToShortDateString(), typeof(System.String));
            }
            datatabell.Columns.Add("Slutt", typeof(System.String));
            datatabell.Columns.Add("Avvik", typeof(System.String));

            DataRow luftRow1 = datatabell.NewRow();
            luftRow1["Oppgavenavn"] = "";
            datatabell.Rows.Add(luftRow1);

            DataRow totalRow = datatabell.NewRow();
            totalRow["Oppgavenavn"] = "Total tid for " + fase.Navn;

            List<TimeSpan> totalTider = new List<TimeSpan>();
            foreach (Oppgave o in oppgaverForFase)
            {
                estimatForFase = estimatForFase + (TimeSpan)o.Estimat;
            }
            foreach (DateTime d in range)
            {
                totalTider.Add(new TimeSpan(0));
                nyEstimertTidFase.Add(estimatForFase);
            }

            foreach (Oppgave o in oppgaverForFase)
            {
                TimeSpan resterendeTid = (TimeSpan)o.Estimat;
           //     estimatForFase = estimatForFase + resterendeTid;
                List<Time> registrerteTimerPaaOppgave = Queries.GetTimerForOppgave(o.Oppgave_id);

                String utviklere = "";
                List<Bruker> brukerePaaOppgave = Queries.GetBrukereForOppgave(o.Oppgave_id);
                foreach(Bruker b in brukerePaaOppgave) {
                    utviklere = utviklere + b.Fornavn + " " + b.Etternavn + " ";
                }

                DataRow oppgaveRow = datatabell.NewRow();
                oppgaveRow["Oppgave ID"] = o.RefOppgaveId.ToString();
                oppgaveRow["Oppgavenavn"] = o.Tittel;
                oppgaveRow["Ansvarlig utvikler(e)"] = utviklere;
                oppgaveRow["Estimat"] = o.Estimat.ToString();

                for (int i = 0; i < range.Count; i++)
                {
                    for (int j = 0; j < registrerteTimerPaaOppgave.Count; j++) {
                        DateTime en = range.ElementAt(i);
                        DateTime to = (DateTime)registrerteTimerPaaOppgave.ElementAt(j).Stopp;

                        if (range.ElementAt(i).Date.Equals(to.Date))
                            resterendeTid = resterendeTid - (TimeSpan)registrerteTimerPaaOppgave.ElementAt(j).Tid;
                    }

                    oppgaveRow[range.ElementAt(i).ToShortDateString()] = resterendeTid;

                    totalTider[i] = totalTider[i] + resterendeTid;

                    if (resterendeTid < nullTimeSpan)
                    {
                        nyEstimertTidFase[i] = nyEstimertTidFase[i] - resterendeTid;
                    }

                    if (o.Avsluttet != null)
                    {
                        DateTime sluttDato = (DateTime)o.Avsluttet;

                        if (o.Status_id == 3 && sluttDato.Date == range[i].Date)
                        {
                            for (int j = i; j < range.Count; j++)
                            {
                                nyEstimertTidFase[j] = nyEstimertTidFase[j] - resterendeTid;
                            }
                        } 

                    } 
                    
                }

                for (int i = 0; i < totalTider.Count; i++)
                {
                    totalRow[range.ElementAt(i).ToShortDateString()] = totalTider[i].ToString();
                }

                

                oppgaveRow["Slutt"] = o.BruktTid.ToString();
                totalSluttTid = totalSluttTid + (TimeSpan)o.BruktTid;
                oppgaveRow["Avvik"] = (o.BruktTid - o.Estimat).ToString();
                totalAvvik = totalAvvik + (TimeSpan)(o.BruktTid - o.Estimat);

                datatabell.Rows.Add(oppgaveRow);
            }

            totalRow["Estimat"] = estimatForFase.ToString();
            totalRow["Slutt"] = totalSluttTid.ToString();
            totalRow["Avvik"] = totalAvvik.ToString();

            DataRow luftRow2 = datatabell.NewRow();
            luftRow2["Oppgavenavn"] = "";
            datatabell.Rows.Add(luftRow2);
            datatabell.Rows.Add(totalRow);

            DataRow ideellTidsbruk = datatabell.NewRow();
            ideellTidsbruk["Oppgavenavn"] = "Ideell tidsbruk";
            ideellTidsbruk["Estimat"] = estimatForFase.ToString();

            double estimatSomDouble = (double) estimatForFase.TotalHours;
            double ideellTid = estimatSomDouble;
            double ideellTidRest;
            int ideelleTimer;
            int ideelleMinutter;

            for (int i = 0; i < range.Count; i++)
            {
                ideellTid = ideellTid - (estimatSomDouble / range.Count);
                ideelleTimer = (int)ideellTid;
                ideellTidRest = ideellTid - (double)ideelleTimer;
                ideelleMinutter = (int)(ideellTidRest * 60);

                TimeSpan ideellTidTimeSpan = new TimeSpan(ideelleTimer, ideelleMinutter, 0);

                ideellTidsbruk[range.ElementAt(i).ToShortDateString()] = ideellTidTimeSpan.ToString();
            }

            ideellTidsbruk["Slutt"] = estimatForFase.ToString();
            ideellTidsbruk["Avvik"] = nullTimeSpan.ToString();

            datatabell.Rows.Add(ideellTidsbruk);

            DataRow beregnetTid = datatabell.NewRow();
            beregnetTid["Oppgavenavn"] = "Beregnet totaltid (fase)";
            beregnetTid["Estimat"] = estimatForFase.ToString();

            for (int i = 0; i < range.Count; i++)
            {
                beregnetTid[range.ElementAt(i).ToShortDateString()] = nyEstimertTidFase[i];
            }

            beregnetTid["Slutt"] = totalSluttTid.ToString();
            beregnetTid["Avvik"] = totalAvvik.ToString();

            datatabell.Rows.Add(beregnetTid);

            return datatabell;
        }

    }
}