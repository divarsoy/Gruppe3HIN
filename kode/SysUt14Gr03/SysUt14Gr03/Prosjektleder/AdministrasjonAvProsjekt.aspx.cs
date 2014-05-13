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
        private List<Bruker> prosjektLeder;
        private int bruker_id;
        private int prosjekt_id;
        private Rettighet rettighet;

        protected void Page_PreInit(Object sener, EventArgs e)
        {
            string master = SessionSjekk.findMaster();
            this.MasterPageFile = master;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            SessionSjekk.sjekkForRettighetPaaInnloggetBruker(Konstanter.rettighet.Prosjektleder);
            bruker_id = Validator.KonverterTilTall(Session["bruker_id"].ToString());
            rettighet = Queries.GetRettighet(bruker_id);
            if (rettighet.Rettighet_id == 3)
            {
                prosjekt_id = Validator.KonverterTilTall(Session["prosjekt_id"].ToString());

            }
            if (Validator.SjekkRettighet(Validator.KonverterTilTall(Session["bruker_id"].ToString()), Konstanter.rettighet.Prosjektleder) || SessionSjekk.IsFaseleder())
            {
                if (!IsPostBack)
                {
                    visProsjekt();
                }
            }
        }

        private void visProsjekt()
        {
            using (var context = new Context())
            {
                System.Windows.Forms.BindingSource bindingSource1 = new System.Windows.Forms.BindingSource();
                if (rettighet.Rettighet_id == 3)
                {
                    bindingSource1.DataSource = context.Prosjekter.Where(P => P.Aktiv == true).Where(p => p.Prosjekt_id == prosjekt_id).ToList<Prosjekt>();
                    gridViewProsjekt.Columns[8].Visible = false;
                }
                else if (rettighet.Rettighet_id == 2)
                    bindingSource1.DataSource = context.Prosjekter.Where(P => P.Aktiv == true).Where(p => p.Bruker_id == bruker_id).ToList<Prosjekt>();
                gridViewProsjekt.DataSource = bindingSource1;
                gridViewProsjekt.DataBind();

            }
        }

        protected void gridViewProsjekt_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                int prosjekt_id = (int)gridViewProsjekt.DataKeys[e.RowIndex].Value;
                System.Web.UI.WebControls.TextBox tbProsjektnavn = (TextBox)gridViewProsjekt.Rows[e.RowIndex].FindControl("tbProsjektnavn");
                System.Web.UI.WebControls.DropDownList tbProsjektleder = (DropDownList)gridViewProsjekt.Rows[e.RowIndex].FindControl("ddlLeder");
                System.Web.UI.WebControls.TextBox tbStart = (TextBox)gridViewProsjekt.Rows[e.RowIndex].FindControl("tbStart");
                System.Web.UI.WebControls.TextBox tbSlutt = (TextBox)gridViewProsjekt.Rows[e.RowIndex].FindControl("tbSlutt");
                System.Web.UI.WebControls.DropDownList tbTeam = (DropDownList)gridViewProsjekt.Rows[e.RowIndex].FindControl("ddlTeam");
                System.Web.UI.WebControls.CheckBox cbAktiv = (CheckBox)gridViewProsjekt.Rows[e.RowIndex].FindControl("cboxAktiv");

                String prosjektNavnFoerOppdatering;
                bool currentIsActive = true;
                using (var context = new Context())
                {                    
                    team_id = Validator.KonverterTilTall(tbTeam.SelectedValue);
                    int bruker_id = Validator.KonverterTilTall(tbProsjektleder.SelectedValue);

                    Prosjekt prosjekt = context.Prosjekter.Where(p => p.Prosjekt_id == prosjekt_id).First();
                    prosjektNavnFoerOppdatering = prosjekt.Navn;
                    prosjekt.Navn = tbProsjektnavn.Text;
                    prosjekt.Aktiv = Convert.ToBoolean(cbAktiv.Checked);
                    prosjekt.StartDato = Convert.ToDateTime(tbStart.Text);
                    prosjekt.SluttDato = Convert.ToDateTime(tbSlutt.Text);
                    prosjekt.Team_id = team_id;
                    prosjekt.Bruker_id = bruker_id;
                    context.SaveChanges();

                    prosjekt_id = prosjekt.Prosjekt_id;
                    
                    currentIsActive = prosjekt.Aktiv;
                }
                gridViewProsjekt.EditIndex = -1;
                gridViewProsjekt.Columns[6].Visible = true;
                gridViewProsjekt.Columns[7].Visible = true;
                visProsjekt();

                //Oppretter logg for opprettelse av prosjektet
                String hendelse = "Prosjektet " + prosjektNavnFoerOppdatering + " ble endret";
                String arkiveringHendelse = "Prosjektet " + prosjektNavnFoerOppdatering + " ble arkivert";

                if (prosjektNavnFoerOppdatering != tbProsjektnavn.Text)
                {
                    hendelse = hendelse + ". Nytt navn på prosjektet er " + tbProsjektnavn.Text;
                }

                if (!currentIsActive)
                    OppretteLogg.opprettLoggForProsjekt(arkiveringHendelse, DateTime.Now, (int)Session["bruker_id"], prosjekt_id);
                else
                    OppretteLogg.opprettLoggForProsjekt(hendelse, DateTime.Now, (int)Session["bruker_id"], prosjekt_id);

            }
            catch
            {
                Session["flashMelding"] = "Stemmer ikke overrens med databasen!";
                Session["flashStatus"] = Konstanter.notifikasjonsTyper.danger.ToString();
            }
        }

        protected void gridViewProsjekt_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gridViewProsjekt.EditIndex = -1;
            gridViewProsjekt.Columns[6].Visible = true;
            gridViewProsjekt.Columns[7].Visible = true;
            visProsjekt();
        }

        protected void gridViewProsjekt_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gridViewProsjekt.EditIndex = e.NewEditIndex;

            gridViewProsjekt.RowDataBound -= new GridViewRowEventHandler(gridViewProsjekt_RowDataBound);
            gridViewProsjekt.RowDataBound += new GridViewRowEventHandler(gridViewProsjekt_EditRowDataBound);
            gridViewProsjekt.Columns[6].Visible = false;
            gridViewProsjekt.Columns[7].Visible = false;

            visProsjekt();
        }

        protected void gridViewProsjekt_EditRowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow && gridViewProsjekt.EditIndex == e.Row.RowIndex)
            {
                prosjektLeder = Queries.GetProsjektledere(Konstanter.rettighet.Prosjektleder);
                teamListe = Queries.GetAlleAktiveTeam();
                DropDownList ddlt = e.Row.FindControl("ddlTeam") as DropDownList;
                Label lbTeam = e.Row.FindControl("lbTeam") as Label;
                Label lblProsjekt = e.Row.FindControl("lbProsjektnavn") as Label;
                                
                int t_id = Validator.KonverterTilTall(lbTeam.Text);
                using (var context = new Context())
                {
                    Team team_id = context.Teams.Where(t => t.Team_id == t_id).First();
                    int Prosjekt_id = Validator.KonverterTilTall(lblProsjekt.Text);
                   
                    for (int i = 0; i < teamListe.Count; i++)
                    {
                        Team team = teamListe[i];
                        ddlt.Items.Add(new ListItem(team.Navn, team.Team_id.ToString()));
                    }
                    ddlt.SelectedIndex = Prosjekt_id - 1;

                    DropDownList ddlLeder = e.Row.FindControl("ddlLeder") as DropDownList;
                    for (int i = 0; i < prosjektLeder.Count; i++)
                    {
                        Label lblProsjektLeder = e.Row.FindControl("lblProsjektleder") as Label;
                        int id = Validator.KonverterTilTall(lblProsjektLeder.Text);
                        Bruker leder = context.Brukere.Where(b => b.Bruker_id == id).First();
                        Bruker bruker = prosjektLeder[i];

                        ddlLeder.Items.Add(new ListItem(bruker.Fornavn, bruker.Bruker_id.ToString()));
                    }
                    ddlLeder.SelectedIndex = Prosjekt_id - 1;
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
                    int team_id = Validator.KonverterTilTall(tTeam.Text);
                    int prosjekt_id = Validator.KonverterTilTall(lblProsjekt.Text);
                    Prosjekt prosjekt = context.Prosjekter.Where(p => p.Prosjekt_id == prosjekt_id).First();
                    HyperLink prosjektLink = e.Row.FindControl("pLink") as HyperLink;
                    prosjektLink.Text = prosjekt.Navn;
                    prosjektLink.NavigateUrl = "~/visProsjekt?Prosjekt_id=" + prosjekt_id;
                    HyperLink link = e.Row.FindControl("asp") as HyperLink;
                    link.NavigateUrl = "AdministrasjonAvTeamBrukere?Team_id=" + team_id;
                    HyperLink linkFase = e.Row.FindControl("hlFase") as HyperLink;
                    linkFase.NavigateUrl = "~/AdministrasjonAvFase?prosjekt_id=" + prosjekt_id;
                   
                }
            }
        }
    }
}