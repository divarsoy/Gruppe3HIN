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
    public partial class OversiktProsjekter : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            List<Prosjekt> query = null;
            LblTilbakemelding.Text = "";

            int bruker_id;

            //Sjekker om brukeren er logget inn
            if (Session["bruker_id"] != null)
                bruker_id = Validator.KonverterTilTall((string)Session["bruker_id"]);
            else
                bruker_id = 3;

            if (Validator.SjekkRettighet(bruker_id, Konstanter.rettighet.Prosjektleder))
                query = Queries.GetAlleAktiveProsjekter();
            else
                query = Queries.GetAlleAktiveProsjekterForBruker(bruker_id);

            if (!IsPostBack)
            {
                Table prosjektTabell = Tabeller.HentProsjekterTabell(query);
                PlaceHolderTable.Controls.Add(prosjektTabell);
            }
        }
    }
}