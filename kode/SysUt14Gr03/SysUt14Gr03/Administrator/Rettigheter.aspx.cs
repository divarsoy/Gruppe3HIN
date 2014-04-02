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
    public partial class Rettigheter : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            SessionSjekk.sjekkForRettighetPaaInnloggetBruker(Konstanter.rettighet.Administrator);
            if (!IsPostBack)
            {
                BindRettigheter();
            }
        }

        private void BindRettigheter()
        {
            using (var context = new Context())
            {
                System.Windows.Forms.BindingSource bindingSource1 = new System.Windows.Forms.BindingSource();
                bindingSource1.DataSource = context.Rettigheter.ToList<Rettighet>();
                GridViewRettigheter.DataSource = bindingSource1;
                GridViewRettigheter.DataBind();
            }
        }
        
        protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridViewRettigheter.EditIndex = e.NewEditIndex;
            BindRettigheter();
        }


        protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GridViewRettigheter.EditIndex = -1;
            BindRettigheter();
        }

        protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            int rettighet_id = (int)GridViewRettigheter.DataKeys[e.RowIndex].Value;
            System.Web.UI.WebControls.TextBox txtRettighetNavn = (TextBox)GridViewRettigheter.Rows[e.RowIndex].FindControl("txtRettighetNavn");

            using (var context = new Context())
            {
                Rettighet rettighet = context.Rettigheter.Where(c => c.Rettighet_id == rettighet_id).First();
                rettighet.RettighetNavn = txtRettighetNavn.Text;
                context.SaveChanges();
            }
            GridViewRettigheter.EditIndex = -1;
            BindRettigheter();

        }
        protected void insertData()
        {
            System.Web.UI.WebControls.TextBox txtNyRettighetNavn = (TextBox)GridViewRettigheter.FooterRow.FindControl("txtNyRettighetNavn");

            using (var context = new Context())
            {
                Rettighet rettighet = new Rettighet();
                rettighet.RettighetNavn = txtNyRettighetNavn.Text;

                context.Rettigheter.Add(rettighet);
                context.SaveChanges();
            }
            BindRettigheter();
        }
        protected void btnLagre_Click(object sender, EventArgs e)
        {
            insertData();
        } 
    }
}