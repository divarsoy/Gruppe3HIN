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
    }
}