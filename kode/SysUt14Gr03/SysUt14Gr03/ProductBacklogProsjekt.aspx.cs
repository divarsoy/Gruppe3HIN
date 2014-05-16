using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SysUt14Gr03.Models;
using SysUt14Gr03.Classes;
using System.Data;

/// <summary>
/// Klasse som viser productBacklog for et prosjekt, henter ut alle oppgavene
/// til det prosjektet som er valgt. Etter det kan du eksportere productbacklogen til ei excell fil.
/// </summary>
namespace SysUt14Gr03
{
    public partial class ProductBacklogProsjekt : System.Web.UI.Page
    {
     
        private Prosjekt prosjekt; //Prosjekt 
        private DataTable dt = new DataTable(); //datatabell for oppgavene(excell)

        // Laster inn riktig masterfil
        protected void Page_PreInit(Object sener, EventArgs e)
        {
            string master = SessionSjekk.findMaster();
            this.MasterPageFile = master;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            SessionSjekk.sjekkForProsjekt_id();
            int prosjekt_id = Validator.KonverterTilTall(Session["prosjekt_id"].ToString());
            prosjekt = Queries.GetProsjekt(prosjekt_id);
            lblProsjektnavn.Text = "Oppgaver i prosjekt: " + prosjekt.Navn;
            Table prosjektTable = Tabeller.HentProsjektTabell(prosjekt);
            prosjektTable.CssClass = "table";
            phProsjekt.Controls.Add(prosjektTable);
        }
        /// <summary>
        /// Eksporterer til Excell
        /// </summary>
        protected void btnExport_Click(object sender, EventArgs e)
        {
            dt = DataTabeller.ProductBacklogProsjekt(prosjekt);
            EksporterTilExcel.CreateExcelDocument(dt, "ProductBacklog for prosjekt.xlsx", Response);
        }
    }
}