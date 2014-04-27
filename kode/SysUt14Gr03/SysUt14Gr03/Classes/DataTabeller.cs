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

    }
}