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
    public partial class OversiktBrukereSomProsjektleder : System.Web.UI.Page
    {
        private List<Prosjekt> listProsjekt;
        private List<Bruker> brukerListe;
        private List<Bruker> brukerProsjekt;
        private List<Bruker> queryTeam = null;
        private int brukerid;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["loggedIn"] == null)
            {
                Response.Redirect("Login.aspx", true);
            }
            else
            {
                brukerid = Validator.KonverterTilTall(Session["bruker_id"].ToString());
            }
          // if (Validator.SjekkRettighet(brukerid, Konstanter.rettighet.Prosjektleder))
              //  {
                listProsjekt = Queries.GetAlleProsjekterForLeder(brukerid);
                brukerListe = Queries.GetAlleAktiveBrukere();

               // lbBrukere.Items.Add("Prosjekt " + " " + " Brukere");
                for (int i = 0; i < listProsjekt.Count; i++)
                {
                    using (var context = new Context())
                    {

                        Prosjekt prosjekt = listProsjekt[i];
                        queryTeam = Queries.GetAlleBrukereIEtTeam((int)prosjekt.Team_id);
                        Team team = context.Teams.Where(t => t.Team_id == prosjekt.Team_id).First();
                      //  Table table = Tabeller.HentBrukerTabell(team.Brukere);
                      //  PlaceHolderBrukere.Controls.Add(table);
                        for (int j = 0; j < queryTeam.Count; j++)
                        {
                            Bruker bruk = brukerListe[j];
                            brukerProsjekt = Queries.GetAlleAktiveBrukereID(bruk.Bruker_id);

                            Table table = Tabeller.HentBrukerTabellIProsjektTeamProsjektLeder(brukerProsjekt);
                            PlaceHolderBrukere.Controls.Add(table);
                           lbBrukere.Items.Add(prosjekt.Navn + " " + bruk.ToString());
                        }
                    }
                }
        /*    }
            else
            {
                Response.Redirect("Brukere.aspx");
            } */
        }
    }
}