using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SysUt14Gr03;
using SysUt14Gr03.Models;
using SysUt14Gr03.Classes;
using System.Data;

namespace SysUt14Gr03
{
    public partial class DefaultProsjektleder : System.Web.UI.Page
    {
        private int bruker_id;
        protected string Fornavn;
        protected void Page_Load(object sender, EventArgs e)
        {
            
            if (!Page.IsPostBack)
            {

                SessionSjekk.sjekkForRettighetPaaInnloggetBruker(Konstanter.rettighet.Prosjektleder);
                
                bruker_id = Validator.KonverterTilTall(Session["bruker_id"].ToString());

                Bruker bruker = Queries.GetBruker(bruker_id);
                Fornavn = bruker.Fornavn;
                List<Prosjekt> listeMedProsjekter = Queries.GetAlleAktiveProsjekterForProsjektLeder(bruker_id);
                DataTable datatable = new DataTable();
                datatable.Columns.Add("Prosjekt_id");
                datatable.Columns.Add("Navn");
                foreach (Prosjekt prosjekt in listeMedProsjekter)
                {
                    datatable.Rows.Add(prosjekt.Prosjekt_id, prosjekt.Navn);
                }
                ListBoxProsjekt.DataValueField = "Prosjekt_id";
                ListBoxProsjekt.DataTextField = "Navn";
                ListBoxProsjekt.SelectionMode = ListSelectionMode.Single;
                ListBoxProsjekt.DataSource = datatable;
                ListBoxProsjekt.DataBind();
                ListBoxProsjekt.CssClass = "form-control";
                btnVelgProsjekt.CssClass = "btn btn-primary";


            }


        }
        protected void btnVelgProsjekt_Click(object sender, EventArgs e)
        {
            if (ListBoxProsjekt.SelectedItem.Value != null)
            {
                int prosjekt_id = Validator.KonverterTilTall(ListBoxProsjekt.SelectedItem.Value);
                Session["prosjekt_id"] = prosjekt_id;
                Prosjekt prosjekt = Queries.GetProsjekt(prosjekt_id);
                Session["prosjekt_navn"] = prosjekt.Navn;
                lblValgtProsjekt.Text = String.Format("<h4>Valgt prosjekt er <b>{0}</b></h4>", prosjekt.Navn);
                //Response.Redirect(String.Format("OversiktOppgaver?bruker_id={0}&prosjekt_id={1}", Session["bruker_id"], ListBoxProsjekt.SelectedItem.Value));
            }
        }
    }
}