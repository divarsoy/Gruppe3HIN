using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SysUt14Gr03.Classes;
using SysUt14Gr03.Models;
using SysUt14Gr03;
using System.Text;

namespace SysUt14Gr03
{
    public partial class OversiktOppgaver : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //System.Windows.Forms.BindingSource bindingSource1 = new System.Windows.Forms.BindingSource();
            //bindingSource1.DataSource = Queries.GetAlleAktiveOppgaverDag();
            //ListView1.DataSource = bindingSource1;
            //ListView1.DataBind();
            //ListView1.ItemType = Models.Oppgave;
            var query = Queries.GetAlleAktiveOppgaverDag();

            TableHeaderRow headerRow = new TableHeaderRow();
            TableHeaderCell tittelHeaderCell = new TableHeaderCell();
            TableHeaderCell brukerHeaderCell = new TableHeaderCell();
            TableHeaderCell statusHeaderCell = new TableHeaderCell();

            tittelHeaderCell.Text = "Tittel";
            brukerHeaderCell.Text = "Brukere";
            statusHeaderCell.Text = "Status";
            headerRow.Cells.Add(tittelHeaderCell);
            headerRow.Cells.Add(brukerHeaderCell);
            headerRow.Cells.Add(statusHeaderCell);
            Table1.Rows.Add(headerRow);




            foreach (Oppgave oppgave in query)
            {
                StringBuilder brukereIOppgave = new StringBuilder();
                foreach (Bruker bruker in oppgave.Brukere)
                {
                    brukereIOppgave.Append(bruker.Brukernavn + " ");
                }
                TableRow tRow = new TableRow();
                TableCell tittelCell = new TableCell();
                TableCell brukerCell = new TableCell();
                TableCell statusCell = new TableCell();
                
                tittelCell.Text = oppgave.Tittel;
                brukerCell.Text = brukereIOppgave.ToString();
                statusCell.Text = Queries.GetStatus(oppgave.Status_id).Navn;

                tRow.Cells.Add(tittelCell);
                tRow.Cells.Add(brukerCell);
                tRow.Cells.Add(statusCell);
                Table1.Rows.Add(tRow);                
            }
            Table1.CssClass = "table";


            //ListView1.InsertItem(query.Oppgave.Tittel);

        }
        public void test()
        {

        }


        //Default loader inn innlogget brukers oppgaver som er påbegynt eller som er klare
    }
}