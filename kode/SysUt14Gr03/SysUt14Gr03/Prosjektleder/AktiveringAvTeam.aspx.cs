using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SysUt14Gr03.Classes;
using SysUt14Gr03.Models;

namespace SysUt14Gr03.Prosjektleder
{
    public partial class AktiveringAvTeam : System.Web.UI.Page
    {
        private List<Team> teamListe;

        protected void Page_PreInit(Object sener, EventArgs e)
        {
            string master = SessionSjekk.findMaster();
            this.MasterPageFile = master;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            SessionSjekk.sjekkForRettighetPaaInnloggetBruker(Konstanter.rettighet.Prosjektleder);

            teamListe = Queries.GetAlleArkiverteTeam();

            if (cbl_team.Items.Count == 0 && teamListe.Count != 0)
            {
                for (int i = 0; i < teamListe.Count(); i++)
                {
                    Team team = teamListe[i];
                    cbl_team.Items.Add(team.Navn);
                }
            }
            else
            {
                lblTilbakeMelding.Visible = true;
                bt_aktivereTeam.Enabled = false;
            }
        }
        protected void bt_aktiverTeam_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < cbl_team.Items.Count; i++)
            {
                if (cbl_team.Items[i].Selected)
                {
                    Team valgtTeam = teamListe[i];
                    Queries.AktiverTeam(valgtTeam);
                    //Oppretter logg
                    string hendelse = "Team " + valgtTeam.Navn + " er blitt aktivert";
                    OppretteLogg.opprettLoggForBruker(hendelse, DateTime.Now, (int)Session["bruker_id"]);

                    Session["flashMelding"] += hendelse + "\n";
                    Session["flashStatus"] = Konstanter.notifikasjonsTyper.success.ToString();
                }
            }
            Response.Redirect("~/Prosjektleder/AdministrasjonAvTeam");
        }
    }
}