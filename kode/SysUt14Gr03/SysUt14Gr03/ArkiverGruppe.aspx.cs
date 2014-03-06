using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SysUt14Gr03.Models;

namespace SysUt14Gr03
{
    public partial class ArkiverGruppe : System.Web.UI.Page
    {
        private int gruppe_id;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                visGrupper();
            }
        }
        private void visGrupper()
        {
            using (var context = new Context())
            {
                System.Windows.Forms.BindingSource bindingSource1 = new System.Windows.Forms.BindingSource();
                bindingSource1.DataSource = context.Grupper.ToList<Gruppe>();
                gridViewGroup.DataSource = bindingSource1;
                gridViewGroup.DataBind();
            }
        }
        protected void gridViewGroup_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gridViewGroup.EditIndex = e.NewEditIndex;
            visGrupper();
        }

        protected void gridViewGroup_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gridViewGroup.EditIndex = -1;
            visGrupper();
        }

        protected void gridViewGroup_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            gruppe_id = (int)gridViewGroup.DataKeys[e.RowIndex].Value;
            System.Web.UI.WebControls.TextBox tbNavn = (TextBox)gridViewGroup.Rows[e.RowIndex].FindControl("tbNavn");
            System.Web.UI.WebControls.CheckBox cbAktiv = (CheckBox)gridViewGroup.Rows[e.RowIndex].FindControl("cboxAktiv");

            using (var context = new Context())
            {
                Gruppe grupper = context.Grupper.Where(b => b.Gruppe_id == gruppe_id).First();
                grupper.Navn = tbNavn.Text;
                grupper.Aktiv = Convert.ToBoolean(cbAktiv.Checked);
                context.SaveChanges();
            }
            gridViewGroup.EditIndex = -1;
            visGrupper();
        }
    }
}