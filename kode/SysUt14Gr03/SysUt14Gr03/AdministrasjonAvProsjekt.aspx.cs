using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SysUt14Gr03.Models;

namespace SysUt14Gr03
{
    public partial class AdministrasjonAvProsjekt : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                visProsjekt();
            }
        }
        private void visProsjekt()
        {
            using (var context = new Context())
            {
                System.Windows.Forms.BindingSource bindingSource1 = new System.Windows.Forms.BindingSource();
                bindingSource1.DataSource = context.Prosjekter.ToList<Prosjekt>();
                gridViewProsjekt.DataSource = bindingSource1;
                gridViewProsjekt.DataBind();
             
            }          
        }
        protected void gridViewProsjekt_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            int prosjekt_id = (int)gridViewProsjekt.DataKeys[e.RowIndex].Value;
            int team_id;
            int gruppe_id;
            int bruker_id;
            System.Web.UI.WebControls.TextBox tbProsjektnavn = (TextBox)gridViewProsjekt.Rows[e.RowIndex].FindControl("tbProsjektnavn");
            System.Web.UI.WebControls.TextBox tbProsjektleder = (TextBox)gridViewProsjekt.Rows[e.RowIndex].FindControl("tbProsjektleder");
            System.Web.UI.WebControls.TextBox tbStart = (TextBox)gridViewProsjekt.Rows[e.RowIndex].FindControl("tbStart");
            System.Web.UI.WebControls.TextBox tbSlutt = (TextBox)gridViewProsjekt.Rows[e.RowIndex].FindControl("tbSlutt");
            System.Web.UI.WebControls.TextBox tbTeam = (TextBox)gridViewProsjekt.Rows[e.RowIndex].FindControl("tbTeam");
            System.Web.UI.WebControls.TextBox tbGruppe = (TextBox)gridViewProsjekt.Rows[e.RowIndex].FindControl("tbGruppe");
            System.Web.UI.WebControls.CheckBox cbAktiv = (CheckBox)gridViewProsjekt.Rows[e.RowIndex].FindControl("cboxAktiv");
          
            using (var context = new Context())
            {
                Team team = context.Teams.Where(t => t.Navn == tbTeam.Text).First();
                team_id = team.Team_id;

                Gruppe gruppe = context.Grupper.Where(g => g.Navn == tbGruppe.Text).First();
                gruppe_id = gruppe.Gruppe_id;

                Bruker bruker = context.Brukere.Where(b => b.Fornavn == tbProsjektleder.Text).First();
                bruker_id = bruker.Bruker_id;

                Prosjekt prosjekt = context.Prosjekter.Where(p => p.Prosjekt_id == prosjekt_id).First();
                prosjekt.Navn = tbProsjektnavn.Text;
                prosjekt.Aktiv = Convert.ToBoolean(cbAktiv.Checked);
                prosjekt.StartDato = Convert.ToDateTime(tbStart.Text);
                prosjekt.SluttDato = Convert.ToDateTime(tbSlutt.Text);
                prosjekt.Team_id = team_id;
                prosjekt.Gruppe_id = gruppe_id;
                prosjekt.Bruker_id = bruker_id;
                context.SaveChanges();
            }
            gridViewProsjekt.EditIndex = -1;
            visProsjekt();
        }

        protected void gridViewProsjekt_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gridViewProsjekt.EditIndex = -1;
            visProsjekt();
        }

        protected void gridViewProsjekt_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gridViewProsjekt.EditIndex = e.NewEditIndex;
            visProsjekt();
        }
    }
}