using System;
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
        private DataTable dt = new DataTable();
        private Table tabell = new Table();
        private int bruker_id;
        private List<Logg> query = Queries.GetLoggForAdministrator();

        protected void Page_PreInit(Object sener, EventArgs e)
        {
            string master = SessionSjekk.findMaster();
            this.MasterPageFile = master;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            SessionSjekk.sjekkForRettighetPaaInnloggetBruker(Konstanter.rettighet.Administrator);
            bruker_id = Validator.KonverterTilTall(Session["bruker_id"].ToString());

            tabell = Tabeller.hentLoggForAdministrator(query);
            PlaceHolderTable.Controls.Add(tabell);

            if (!Page.IsPostBack)
            {
                //Sjekker om sheperd skal deaktiveres eller vises
                if (Request.QueryString["sheperd"] != null)
                {
                    Queries.SetSheperd(bruker_id);
                }
                else if (Queries.GetBrukerPreferanse(bruker_id).Sheperd)
                {
                    ScriptManager.RegisterClientScriptInclude(this.Page, this.GetType(), "jquery", "../Scripts/jquery-1.10.2.js");
                    ScriptManager.RegisterClientScriptInclude(this.Page, this.GetType(), "SheperdScript", "../Scripts/MorildSheperdAdministrator.js");
                }
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            dt = DataTabeller.OversiktLoggAdministrator(query);
            EksporterTilExcel.CreateExcelDocument(dt, "Rapport over hendelser.xlsx", Response);
        }
    }
}