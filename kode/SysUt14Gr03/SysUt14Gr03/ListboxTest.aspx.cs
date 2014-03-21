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
    public partial class ListboxTest : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                List<Bruker> listMedBrukere = Queries.GetAlleAktiveBrukere();
                DataTable datatable = new DataTable();
                datatable.Columns.Add("bruker_id");
                datatable.Columns.Add("Navn");
                foreach (Bruker bruker in listMedBrukere)
                {
                    datatable.Rows.Add(bruker.Bruker_id, bruker.ToString());
                }
                ListBox1.DataValueField = "bruker_id";
                ListBox1.DataTextField = "Navn";
                ListBox1.SelectionMode = ListSelectionMode.Multiple;
                ListBox1.DataSource = datatable;
                ListBox1.DataBind();
                ListBox1.CssClass = "form-control listbox";
                btnRemove.CssClass = "btn btn-primary";
            }
        }

        protected void btnRemove_Click(object sender, EventArgs e)
        {
            if (ListBox1.Items.Count > 0 && ListBox1.SelectedIndex >= 0)
            {
                for (int i = 0; i < ListBox1.Items.Count; i++)
                {
                    if (ListBox1.Items[i].Selected)
                    {
                        ListBox1.Items.Remove(ListBox1.Items[i]);
                        i--;
                    }
                }
            }
        }
    }
}