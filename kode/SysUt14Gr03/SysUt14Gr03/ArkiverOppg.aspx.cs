using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SysUt14Gr03.Models;

namespace SysUt14Gr03
{
    public partial class CancelOppg : System.Web.UI.Page
    {
        private int oppgave_id;

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
                gridViewOppgave.DataSource = bindingSource1;
                gridViewOppgave.DataBind();
            }
        }
        protected void gridViewGroup_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gridViewOppgave.EditIndex = e.NewEditIndex;
            visGrupper();
        }

        protected void gridViewGroup_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gridViewOppgave.EditIndex = -1;
            visGrupper();
        }

        protected void gridViewGroup_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            oppgave_id = (int)gridViewOppgave.DataKeys[e.RowIndex].Value;
            System.Web.UI.WebControls.TextBox tbTittel = (TextBox)gridViewOppgave.Rows[e.RowIndex].FindControl("tbTittel");
            System.Web.UI.WebControls.TextBox tbEstimat = (TextBox)gridViewOppgave.Rows[e.RowIndex].FindControl("tbEstimat");
            System.Web.UI.WebControls.TextBox tbBruktTid = (TextBox)gridViewOppgave.Rows[e.RowIndex].FindControl("tbBruktTid");
            System.Web.UI.WebControls.TextBox tbTidsfrist = (TextBox)gridViewOppgave.Rows[e.RowIndex].FindControl("tbTidsfrist");
            System.Web.UI.WebControls.TextBox tbOpprettet = (TextBox)gridViewOppgave.Rows[e.RowIndex].FindControl("tbOpprettet");
            System.Web.UI.WebControls.TextBox tbProsjekt = (TextBox)gridViewOppgave.Rows[e.RowIndex].FindControl("tProsjekt");
            System.Web.UI.WebControls.TextBox tbStatus = (TextBox)gridViewOppgave.Rows[e.RowIndex].FindControl("tbStatus");
            System.Web.UI.WebControls.CheckBox cbAktiv = (CheckBox)gridViewOppgave.Rows[e.RowIndex].FindControl("cboxAktiv");

            using (var context = new Context())
            {
                Oppgave oppgaver = context.Oppgaver.Where(b => b.Oppgave_id == oppgave_id).First();
                oppgaver.Tittel = tbTittel.Text;
                oppgaver.Estimat = Convert.ToInt16(tbEstimat.Text);
                oppgaver.BruktTid = Convert.ToInt32(tbBruktTid.Text);
                oppgaver.Tidsfrist = Convert.ToDateTime(tbTidsfrist.Text);
                oppgaver.Opprettet = Convert.ToDateTime(tbOpprettet.Text);
                oppgaver.Prosjekt_id = Convert.ToInt32(tbProsjekt.Text);
                oppgaver.Status_id = Convert.ToInt32(tbStatus.Text);
                oppgaver.Aktiv = Convert.ToBoolean(cbAktiv.Checked);
                context.SaveChanges();
            }
            gridViewOppgave.EditIndex = -1;
            visGrupper();
        }
    }
}