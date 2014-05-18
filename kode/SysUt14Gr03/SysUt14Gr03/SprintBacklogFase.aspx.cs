using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SysUt14Gr03.Models;
using SysUt14Gr03.Classes;
using System.Data;
using System.IO;
using System.Text.RegularExpressions;
using System.Web.UI.DataVisualization.Charting;

/// <summary>
/// Tar inn en prosjekt_id og Viser en sprintbacklog for hver fase
/// i det prosjektet som har den id'en, kan bytte mellom faser ved hjelp av en dropdownlist og 
/// så er det metoder for å eksportere sprintbacklogen til excell format.
/// </summary>
namespace SysUt14Gr03
{
    public partial class SprintBacklogFase : System.Web.UI.Page
    {

       
        private Fase fase; //inneholder fasen
        private DataTable dt = new DataTable(); //Datatabell for bruk av eksportering(excell)
        
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
            if (!IsPostBack)
            {
                List<Fase> faseListe = Queries.GetFaseForProsjekt(prosjekt_id);
                foreach (Fase f in faseListe)
                {
                    ddlfaser.Items.Add(new ListItem(f.Navn, f.Fase_id.ToString()));
                }
            }
           fase = Queries.GetFase(Validator.KonverterTilTall(ddlfaser.SelectedValue));
           lblfasenavn.Text = fase.Navn + " i prosjekt: " + fase.Prosjekt.Navn;
           Table FaseTabell = Tabeller.HentFaseTabell(fase);
           FaseTabell.CssClass = "table";
           phFase.Controls.Add(FaseTabell);

        }
        
        protected void btnExport_Click(object sender, EventArgs e)
        {
            dt = DataTabeller.SprintBacklogFase(fase);
            EksporterTilExcel.CreateExcelDocument(dt, "SprintBacklog for fase.xlsx", Response);
        }

        protected void btnExportBurndownskjema_Click(object sender, EventArgs e)
        {
            dt = DataTabeller.BurnDownChartForFase(fase.Fase_id);
            EksporterTilExcel.CreateExcelDocument(dt, "SprintBacklog for fase.xlsx", Response);
            Chart chart = BurnDownDiagram.getChartForFase(fase.Fase_id);
            btnExportExcel_Click(chart);

        }

        protected void btnExportExcel_Click(Chart Chart1)
        {
            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename=ChartExport.xls");
            Response.ContentType = "application/vnd.ms-excel";
            Response.Charset = "";
            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);
            Chart1.RenderControl(hw);
            string src = Regex.Match(sw.ToString(), "<img.+?src=[\"'](.+?)[\"'].+?>", RegexOptions.IgnoreCase).Groups[1].Value;
            string img = string.Format("<img src = '{0}{1}' />", Request.Url.GetLeftPart(UriPartial.Authority), src);

            Table table = new Table();
            TableRow row = new TableRow();
            row.Cells.Add(new TableCell());
            row.Cells[0].Width = 200;
            row.Cells[0].HorizontalAlign = HorizontalAlign.Center;
            row.Cells[0].Controls.Add(new Label { Text = "Fruits Distribution (India)", ForeColor = System.Drawing.ColorTranslator.FromHtml("#FF0000FF") });
            table.Rows.Add(row);
            row = new TableRow();
            row.Cells.Add(new TableCell());
            row.Cells[0].Controls.Add(new Literal { Text = img });
            table.Rows.Add(row);

            sw = new StringWriter();
            hw = new HtmlTextWriter(sw);
            table.RenderControl(hw);
            Response.Write(sw.ToString());
            Response.Flush();
            Response.End();
        }
    }
}