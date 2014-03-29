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
                if (Session["bruker_id"] == null)
                {
                    //Response.Redirect("~/Login.aspx", true);
                }
                else
                {
                    bruker_id = Validator.KonverterTilTall(Session["bruker_id"].ToString());
                }
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
                Session["prosjekt_id"] = ListBoxProsjekt.SelectedItem.Value;
                //Session["prosjektNavn"] = 
                //Response.Redirect(String.Format("OversiktOppgaver?bruker_id={0}&prosjekt_id={1}", Session["bruker_id"], ListBoxProsjekt.SelectedItem.Value));
            }
        }
    }
}