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

            BindingSource bindingsource = new BindingSource();
            valgteOppgaver = new List<Oppgave>();
            bindingsource.DataSource = oppgaveListe;
            gvwOppgaver.DataSource = bindingsource;
            gvwOppgaver.DataBind();

            if (!IsPostBack)
            {
                foreach (Oppgave oppgave in oppgaveListe)
                {
                    
                }
                
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

            int i = 0;

            foreach (GridViewRow row in gvwOppgaver.Rows)
            {
                System.Web.UI.WebControls.CheckBox chkBox = row.FindControl("chbGruppe") as System.Web.UI.WebControls.CheckBox;
                ddlPrioritet = row.FindControl("ddlPrioritet") as DropDownList;
                if (chkBox != null)
                {
                    if (chkBox.Checked)
                    {
                        Oppgave oppgave = oppgaveListe[i];
                        valgteOppgaver.Add(oppgaveListe[i]);
                    }
                }
                i++;
            }

            using (var context = new Context())
            {
                List<Oppgave> oppgaverTilDatabase = new List<Oppgave>();

                var nyOppgaveGruppe = new OppgaveGruppe
                {
                    Navn = txtNavn.Text
                };

                context.OppgaveGrupper.Add(nyOppgaveGruppe);

                foreach (Oppgave oppgave in valgteOppgaver)
                {

                    Oppgave op = context.Oppgaver.FirstOrDefault(o => o.Oppgave_id == oppgave.Oppgave_id);
                    op.OppgaveGruppe = nyOppgaveGruppe;
                    op.Prioritering = context.Prioriteringer.FirstOrDefault(p => p.Prioritering_id == 10);
                    context.SaveChanges();
                }
                

            }
        }
    }
}