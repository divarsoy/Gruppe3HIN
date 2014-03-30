using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SysUt14Gr03.Models;
using SysUt14Gr03.Classes;
using System.Windows.Forms;

namespace SysUt14Gr03
{
    public partial class OpprettOppgavegruppe : System.Web.UI.Page
    {
        private int prosjekt_id = 1;
        private List<Oppgave> oppgaveListe;
        private List<Oppgave> valgteOppgaver;
        private DropDownList ddlPrioritet;
        //private DropDownList ddlPrioritet;

        protected void Page_Load(object sender, EventArgs e)
        {
            oppgaveListe = Queries.GetAlleAktiveOppgaverForProsjekt(prosjekt_id);
            valgteOppgaver = new List<Oppgave>();

            if (!IsPostBack)
            {
                BindingSource bindingsource = new BindingSource();
                bindingsource.DataSource = oppgaveListe;
                gvwOppgaver.DataSource = bindingsource;
                gvwOppgaver.DataBind();
            }
        }

        protected void gvwOppgaver_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DropDownList ddlPrioritet = (e.Row.FindControl("ddlPrioritet") as DropDownList);

                for (int i = 1; i < 11; i++)
                {
                    ddlPrioritet.Items.Add(i.ToString());
                }
            }
        }

        protected void btnOpprett_Click(object sender, EventArgs e)
        {
            //DropDownList ddlPrioritet;

            for (int i = 0; i < gvwOppgaver.Rows.Count; i++)
            {
                System.Web.UI.WebControls.CheckBox chkBox = (System.Web.UI.WebControls.CheckBox)gvwOppgaver.Rows[i].FindControl("chbGruppe");
                ddlPrioritet = gvwOppgaver.Rows[i].FindControl("ddlPrioritet") as DropDownList;
                if (chkBox != null)
                {
                    if (chkBox.Checked)
                    {
                        Oppgave oppgave = oppgaveListe[i];
                        oppgave.Prioritering_id = ddlPrioritet.SelectedIndex + 1; ;
                        valgteOppgaver.Add(oppgaveListe[i]);
                    }
                }
            }
            if (valgteOppgaver.Count > 2)
            {
                using (var context = new Context())
                {
                    List<Oppgave> oppgaverTilDatabase = new List<Oppgave>();

                    var nyOppgaveGruppe = new OppgaveGruppe
                    {
                        Navn = txtNavn.Text
                    };

                    context.OppgaveGrupper.Add(nyOppgaveGruppe);
                    context.SaveChanges();

                    foreach (Oppgave oppgave in valgteOppgaver)
                    {

                        Oppgave op = context.Oppgaver.FirstOrDefault(o => o.Oppgave_id == oppgave.Oppgave_id);
                        op.OppgaveGruppe = nyOppgaveGruppe;
                        //int prioritering = ddlPrioritet.SelectedIndex + 1;
                        op.Prioritering = context.Prioriteringer.FirstOrDefault(p => p.Prioritering_id == oppgave.Prioritering_id);
                        context.SaveChanges();
                    }

                }
            }
            else
            {

            }
            
        }
    }
}