using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SysUt14Gr03.Classes;
using SysUt14Gr03.Models;

namespace SysUt14Gr03
{
    public partial class AdministrasjonAvProsjekt : System.Web.UI.Page
    {
        private int team_id;
        private List<Team> teamListe = null;
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
            try
            {
                lblFeil.Visible = false;
                int prosjekt_id = (int)gridViewProsjekt.DataKeys[e.RowIndex].Value;
               
                int bruker_id;

                System.Web.UI.WebControls.TextBox tbProsjektnavn = (TextBox)gridViewProsjekt.Rows[e.RowIndex].FindControl("tbProsjektnavn");
                System.Web.UI.WebControls.TextBox tbProsjektleder = (TextBox)gridViewProsjekt.Rows[e.RowIndex].FindControl("tbProsjektleder");
                System.Web.UI.WebControls.TextBox tbStart = (TextBox)gridViewProsjekt.Rows[e.RowIndex].FindControl("tbStart");
                System.Web.UI.WebControls.TextBox tbSlutt = (TextBox)gridViewProsjekt.Rows[e.RowIndex].FindControl("tbSlutt");
                System.Web.UI.WebControls.DropDownList tbTeam = (DropDownList)gridViewProsjekt.Rows[e.RowIndex].FindControl("ddlTeam");
                System.Web.UI.WebControls.CheckBox cbAktiv = (CheckBox)gridViewProsjekt.Rows[e.RowIndex].FindControl("cboxAktiv");

                using (var context = new Context())
                {
                   
                    Team team = context.Teams.Where(t => t.Navn == tbTeam.SelectedValue).First();
                    team_id = team.Team_id;

                    Bruker bruker = context.Brukere.Where(b => b.Fornavn == tbProsjektleder.Text).First();
                    bruker_id = bruker.Bruker_id;

                    Prosjekt prosjekt = context.Prosjekter.Where(p => p.Prosjekt_id == prosjekt_id).First();
                    prosjekt.Navn = tbProsjektnavn.Text;
                    prosjekt.Aktiv = Convert.ToBoolean(cbAktiv.Checked);
                    prosjekt.StartDato = Convert.ToDateTime(tbStart.Text);
                    prosjekt.SluttDato = Convert.ToDateTime(tbSlutt.Text);
                    prosjekt.Team_id = team_id;
                    prosjekt.Bruker_id = bruker_id;
                    context.SaveChanges();
                }
                gridViewProsjekt.EditIndex = -1;
                gridViewProsjekt.Columns[6].Visible = true;
                visProsjekt();
            }
            catch 
            {
                lblFeil.Visible = true;
                lblFeil.ForeColor = Color.Red;
                lblFeil.Text = "Stemmer ikke overrens med databasen!";
                
            }
        }
        
        protected void gridViewProsjekt_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gridViewProsjekt.EditIndex = -1;
            gridViewProsjekt.Columns[6].Visible = true;
            visProsjekt();
        }

        protected void gridViewProsjekt_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gridViewProsjekt.EditIndex = e.NewEditIndex;
            gridViewProsjekt.RowDataBound -= new GridViewRowEventHandler(gridViewProsjekt_RowDataBound);
            gridViewProsjekt.RowDataBound += new GridViewRowEventHandler(gridViewProsjekt_RowDataBoun);        
            gridViewProsjekt.Columns[6].Visible = false;
            visProsjekt();
        }
         protected void gridViewProsjekt_RowDataBoun(object sender, GridViewRowEventArgs e)
          {
              if (e.Row.RowType == DataControlRowType.DataRow  && gridViewProsjekt.EditIndex == e.Row.RowIndex)
              {
               
                  Label tTeam = e.Row.FindControl("lbTeam") as Label;
                  teamListe = Queries.GetAlleAktiveTeam();
                  for (int i = 0; i < teamListe.Count; i++)
                  {
                      Team team = teamListe[i];
                      DropDownList ddlt = e.Row.FindControl("ddlTeam") as DropDownList;
                      ddlt.Items.Add(team.Navn);
                  }
              
              }
          } 
        protected void gridViewProsjekt_RowDataBound(object sender, GridViewRowEventArgs e)
        {
           
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    using (var context = new Context())
                    {
                     
                        Label tTeam = e.Row.FindControl("lblTeam_id") as Label;
                        Label lblProsjekt = e.Row.FindControl("lbProsjektnavn") as Label;
                        Prosjekt prosjekt = context.Prosjekter.Where(p => p.Navn == lblProsjekt.Text).First();
                        int team_id = Convert.ToInt32(tTeam.Text);
                        //Team team = context.Teams.Where(t => t.Team_id == team_id).First();
                       // team_id = team.Team_id;
                        HyperLink prosjektLink = e.Row.FindControl("pLink") as HyperLink;
                        prosjektLink.Text = prosjekt.Navn;
                        prosjektLink.NavigateUrl = "visProsjekt?Prosjekt_id=" + prosjekt.Prosjekt_id;
                        HyperLink link = e.Row.FindControl("asp") as HyperLink;
                        link.NavigateUrl = "AdministrasjonAvTeamBrukere?Team_id=" + team_id;
                        
                    }
                }         
        }
    }
}