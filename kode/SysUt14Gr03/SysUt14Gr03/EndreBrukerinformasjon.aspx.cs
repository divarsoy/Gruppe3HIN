using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SysUt14Gr03.Classes;
using SysUt14Gr03.Models;

namespace SysUt14Gr03
{
    public partial class EndreBrukerinformasjon : System.Web.UI.Page
    {
        private List<Bruker> brukerListe;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                visBrukere();
            }

        }

        private void visBrukere()
        {
            using (var context = new Context())
            {
                System.Windows.Forms.BindingSource bindingSource1 = new System.Windows.Forms.BindingSource();
                bindingSource1.DataSource = context.Brukere.ToList<Bruker>();
                gridViewEndre.DataSource = bindingSource1;
                gridViewEndre.DataBind();
            }
        }
        protected void gridViewEndre_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gridViewEndre.EditIndex = e.NewEditIndex;
            visBrukere();
        }

        protected void gridViewEndre_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gridViewEndre.EditIndex = -1;
            visBrukere();
        }

        protected void gridViewEndre_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            int bruker_id = (int)gridViewEndre.DataKeys[e.RowIndex].Value;
            System.Web.UI.WebControls.TextBox tbEtternavn = (TextBox)gridViewEndre.Rows[e.RowIndex].FindControl("tbEtternavn");
            System.Web.UI.WebControls.TextBox tbFornavn = (TextBox)gridViewEndre.Rows[e.RowIndex].FindControl("tbFornavn");
            System.Web.UI.WebControls.TextBox tbEpost = (TextBox)gridViewEndre.Rows[e.RowIndex].FindControl("tbEpost");
            System.Web.UI.WebControls.CheckBox cbAktiv = (CheckBox)gridViewEndre.Rows[e.RowIndex].FindControl("cboxAktiv");


            using (var context = new Context())
            {
                Bruker bruker = context.Brukere.Where(b => b.Bruker_id == bruker_id).First();
                bruker.Etternavn = tbEtternavn.Text;
                bruker.Fornavn = tbFornavn.Text;
                bruker.Epost = tbEpost.Text;
                bruker.Aktiv = Convert.ToBoolean(cbAktiv.Checked);
                context.SaveChanges();
            }
            gridViewEndre.EditIndex = -1;
            visBrukere();
        }

        protected void btnLagre_Click(object sender, EventArgs e)
        {

        }
    }
}