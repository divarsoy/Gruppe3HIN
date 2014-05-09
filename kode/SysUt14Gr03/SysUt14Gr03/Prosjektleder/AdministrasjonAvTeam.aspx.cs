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
    public partial class AdministrasjonAvTeam : System.Web.UI.Page
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
            
            this.hide();
            
            teamListe = Queries.GetAlleAktiveTeam();

            if (cbl_team.Items.Count == 0)
            {
                for (int i = 0; i < teamListe.Count(); i++)
                {
                    Team team = teamListe[i];
                    cbl_team.Items.Add(team.Navn);
                }
            }
        }

        protected void bt_endreTeam_Click(object sender, EventArgs e)
        {
            if (cbl_team.SelectedItem != null)
            {
                Team valgtTeam = Queries.GetTeamByName(cbl_team.SelectedValue);
                Response.Redirect("~/Prosjektleder/AdministrasjonAvTeamBrukere?Team_id=" + valgtTeam.Team_id);
            }
            else
            {
                Session["flashMelding"] = "Vennligst velg et team du vil endre på";
                Session["flashStatus"] = Konstanter.notifikasjonsTyper.info.ToString();
            }
        }

        protected void bt_arkivereTeam_Click(object sender, EventArgs e)
        {
            this.show();
        }

        protected void bt_aktiverTeam_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Prosjektleder/AktiveringAvTeam.aspx");
        }
        protected void yes_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < cbl_team.Items.Count; i++)
            {
                if (cbl_team.Items[i].Selected)
                {
                    Team valgtTeam = teamListe[i];
                    Queries.ArkiverTeam(valgtTeam);
                    //Oppretter logg
                    string hendelse = "Team " + valgtTeam.Navn + " er blitt arkivert";
                    OppretteLogg.opprettLoggForBruker(hendelse, DateTime.Now, (int)Session["bruker_id"]);
                }
            }
            this.hide();
            Response.Redirect(Request.RawUrl);
        }
        protected void no_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < cbl_team.Items.Count; i++)
            {
                cbl_team.Items[i].Selected = false;
            }
            this.hide();
        }
        private void show()
        {
            lblMessage.Visible = true;
            yes.Visible = true;
            no.Visible = true;
        }
        private void hide()
        {
            lblMessage.Visible = false;
            no.Visible = false;
            yes.Visible = false;
        }
    }
}