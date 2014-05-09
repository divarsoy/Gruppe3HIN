using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SysUt14Gr03.Classes;
using SysUt14Gr03.Models;

namespace SysUt14Gr03.Prosjektleder
{
    public partial class AdministrasjonAvFase : System.Web.UI.Page
    {
        private int prosjekt_id;

        protected void Page_PreInit(Object sener, EventArgs e)
        {
            string master = SessionSjekk.findMaster();
            this.MasterPageFile = master;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            SessionSjekk.sjekkForRettighetPaaInnloggetBruker(Konstanter.rettighet.Prosjektleder);
            if (Request.QueryString["prosjekt_id"] != null)
            {
                prosjekt_id = Validator.KonverterTilTall(Request.QueryString["prosjekt_id"].ToString());
            }
            if (!IsPostBack)
            {
                visFase();
            }
        }
        private void visFase()
        {
            using (var context = new Context())
            {
                System.Windows.Forms.BindingSource bindingSource1 = new System.Windows.Forms.BindingSource();
                bindingSource1.DataSource = context.Faser.Include("Bruker").Where(p => p.Prosjekt_id == prosjekt_id).ToList<Fase>();
                gridViewFase.DataSource = bindingSource1;
                gridViewFase.DataBind();

            }
        }
        protected void gridViewFase_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gridViewFase.EditIndex = -1;
            visFase();
        }

        protected void gridViewFase_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gridViewFase.EditIndex = e.NewEditIndex;
            gridViewFase.RowDataBound -= new GridViewRowEventHandler(gridViewFase_RowDataBound);
            gridViewFase.RowDataBound += new GridViewRowEventHandler(gridViewFase_EditRowDataBound);
            visFase();
        }

        protected void gridViewFase_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
            int fase_id = (int)gridViewFase.DataKeys[e.RowIndex].Value;
          
            System.Web.UI.WebControls.TextBox tbFase = (TextBox)gridViewFase.Rows[e.RowIndex].FindControl("tbFase");
            System.Web.UI.WebControls.TextBox tbStart = (TextBox)gridViewFase.Rows[e.RowIndex].FindControl("tbStart");
            System.Web.UI.WebControls.TextBox tbSlutt = (TextBox)gridViewFase.Rows[e.RowIndex].FindControl("tbStopp");
            System.Web.UI.WebControls.CheckBox cbAktiv = (CheckBox)gridViewFase.Rows[e.RowIndex].FindControl("cboxAktiv");
            System.Web.UI.WebControls.DropDownList ddlFaseleder = (DropDownList)gridViewFase.Rows[e.RowIndex].FindControl("ddlFaseleder");

          
                using (var context = new Context())
                {
                    int bruker_id = Validator.KonverterTilTall(ddlFaseleder.SelectedValue);
                    Fase fase = context.Faser.Where(f => f.Fase_id == fase_id).FirstOrDefault();
                    fase.Navn = tbFase.Text;
                    fase.Start = DateTime.Parse(tbStart.Text);
                    fase.Stopp = DateTime.Parse(tbSlutt.Text);
                    fase.Aktiv = Convert.ToBoolean(cbAktiv.Checked);
                    fase.Bruker_id = bruker_id;

                    context.SaveChanges();
                }

                gridViewFase.EditIndex = -1;
                visFase();
            }
            catch
            {
                Session["flashMelding"] = "Du må oppgi en gyldig dato!";
                Session["flashStatus"] = Konstanter.notifikasjonsTyper.danger.ToString();
                Response.Redirect(Request.RawUrl);
            }

        }
        protected void gridViewFase_EditRowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow && gridViewFase.EditIndex == e.Row.RowIndex)
            {
                DropDownList ddlFaseleder = e.Row.FindControl("ddlFaseleder") as DropDownList;
                Label Bruker_id = e.Row.FindControl("Bruker_id") as Label;
                Bruker bruker = Queries.GetBruker(Convert.ToInt32(Bruker_id.Text));
                int id = bruker.Bruker_id;
                List<Bruker> listBrukere = Queries.GetAlleAktiveBrukere();
                for (int i = 0; i < listBrukere.Count; i++)
                {
                    Bruker brukere = listBrukere[i];
                    ddlFaseleder.Items.Add(new ListItem(brukere.ToString(), brukere.Bruker_id.ToString()));
                }
                ddlFaseleder.SelectedIndex = id - 1;
            }
        }
        protected void gridViewFase_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                DropDownList ddlFaseledere = e.Row.FindControl("ddlFaseledere") as DropDownList;

                List<Bruker> listBrukere = Queries.GetAlleAktiveBrukere();

                ddlFaseledere.Items.Add("Velg faseleder");
                foreach (Bruker bruker in listBrukere)
                {
                    ddlFaseledere.Items.Add(new ListItem(bruker.ToString(), bruker.Bruker_id.ToString()));
                }

            }

               if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    Label lblBruker_id = e.Row.FindControl("lblBruker_id") as Label;
                    Label lblBrukere = e.Row.FindControl("lblBrukere") as Label;
                    string navn = Queries.GetBruker(Convert.ToInt32(lblBruker_id.Text)).ToString();
                    lblBrukere.Text = navn;
                }  

        }
        private void nyFase()
        {

            System.Web.UI.WebControls.TextBox tbNyFase = (TextBox)gridViewFase.FooterRow.FindControl("tbNyFase");
            System.Web.UI.WebControls.DropDownList ddlFaseledere = (DropDownList)gridViewFase.FooterRow.FindControl("ddlFaseledere");
            System.Web.UI.WebControls.TextBox tbStartny = (TextBox)gridViewFase.FooterRow.FindControl("tbStartny");
            System.Web.UI.WebControls.TextBox tbStoppny = (TextBox)gridViewFase.FooterRow.FindControl("tbStoppny");

            if (tbNyFase.Text != String.Empty && tbStartny.Text != String.Empty && tbStoppny.Text != String.Empty && ddlFaseledere.SelectedValue != "0")
            {
                using (var context = new Context())
                {
                    Fase fase = new Fase();
                    fase.Prosjekt_id = prosjekt_id;
                    fase.Navn = tbNyFase.Text;
                    fase.Start = DateTime.Parse(tbStartny.Text);
                    fase.Stopp = DateTime.Parse(tbStoppny.Text);
                    fase.Bruker_id = Convert.ToInt32(ddlFaseledere.SelectedValue);
                    fase.Opprettet = DateTime.Now;
                    fase.Aktiv = true;

                    context.Faser.Add(fase);
                    context.SaveChanges();
                }

                visFase();
            }
            else
            {
                Session["flashMelding"] = "Feltene kan ikke være tomme!";
                Session["flashStatus"] = Konstanter.notifikasjonsTyper.danger.ToString();
                Response.Redirect(Request.RawUrl);
            }
        }

        protected void btnLagre_Click(object sender, EventArgs e)
        {
            nyFase();
        }
    }
}