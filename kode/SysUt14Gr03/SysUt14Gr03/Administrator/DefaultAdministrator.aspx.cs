﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SysUt14Gr03.Classes;
using SysUt14Gr03.Models;

namespace SysUt14Gr03
{
    public partial class DefaultAdministrator : System.Web.UI.Page
    {
        DataTable dt = new DataTable();
        Table tabell = new Table();
        List<Logg> query = Queries.GetLoggForAdministrator();
        
        protected void Page_Load(object sender, EventArgs e)
        {
            SessionSjekk.sjekkForRettighetPaaInnloggetBruker(Konstanter.rettighet.Administrator);
            tabell = Tabeller.hentLoggForAdministrator(query);
            PlaceHolderTable.Controls.Add(tabell);
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            dt = DataTabeller.OversiktLoggAdministrator(query);
            EksporterTilExcel.CreateExcelDocument(dt, "Rapport over hendelser.xlsx", Response);
        }
    }
}