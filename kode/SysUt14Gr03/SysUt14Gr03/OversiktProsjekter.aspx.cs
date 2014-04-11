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

        private int bruker_id;
        private List<Prosjekt> prosjektListe;

        // Laster inn riktig masterfil
        protected void Page_PreInit(Object sener, EventArgs e)
        {
            string master = SessionSjekk.findMaster();
            this.MasterPageFile = master;
        }

        protected void Page_Load(object sender, EventArgs e)
        {

            SessionSjekk.sjekkForBruker_id();

            bruker_id = Validator.KonverterTilTall(Session["bruker_id"].ToString());

            if (Validator.SjekkRettighet(bruker_id, Konstanter.rettighet.Prosjektleder))
            {
                prosjektListe = Queries.GetAlleAktiveProsjekter();

                Button btnProsjektleder = new Button();
                btnProsjektleder.Click += new EventHandler(BtnOpprettProsjekt_Click);
                btnProsjektleder.Text = "Opprett Prosjekt";
                btnProsjektleder.CssClass = "btn btn-primary";
                PlaceHolderProsjektleder.Controls.Add(btnProsjektleder);
            }
            else
                prosjektListe = Queries.GetAlleAktiveProsjekterForBruker(bruker_id);

            if (!IsPostBack)
            {
                Table prosjektTabell = Tabeller.HentProsjekterTabell(prosjektListe);
                PlaceHolderTable.Controls.Add(prosjektTabell);
            }
        }

        protected void BtnOpprettProsjekt_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Prosjektleder/OpprettProsjekt");
        }
    }
}