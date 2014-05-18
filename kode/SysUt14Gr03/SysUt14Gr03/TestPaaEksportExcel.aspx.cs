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
    
    public partial class TestPaaEksportExcel : System.Web.UI.Page
    {
        DataTable dt = new DataTable();

 /*       protected void Page_PreInit(Object sener, EventArgs e)
        {
            string master = SessionSjekk.findMaster();
            this.MasterPageFile = master;
        } */

        protected void Page_Load(object sender, EventArgs e)
        {


        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            var query = Queries.GetAlleBrukere();
 //           dt = DataTabeller.OversiktBrukere(query); 
            dt = DataTabeller.BurnDownChartForFase(10);
            EksporterTilExcel.CreateExcelDocument(dt, "reg.xlsx", Response); 



        }
    }
}