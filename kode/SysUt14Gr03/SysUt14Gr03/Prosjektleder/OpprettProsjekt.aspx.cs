using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SysUt14Gr03.Classes;
using SysUt14Gr03.Models;

namespace SysUt14Gr03
{
    public partial class OpprettProsjekt : System.Web.UI.Page
    {
        private DateTime dtStart;
        private DateTime dtSlutt;
        private List<Bruker> brukerListe;
        private List<Team> teamListe;
        private DataTable dtFaser = new DataTable();
        GridView gvFaser;
        private List<Fase> faseListe = new List<Fase>();
    
        private int team_id;
        private int bruker_id;
        private const string BTN_START = "btnStart";
        private const string BTN_SLUTT = "btnSlutt";
        private const string BTN_FASE_START = "btnFaseStart";
        private const string BTN_FASE_SLUTT = "btnFaseSlutt";

        protected void Page_Load(object sender, EventArgs e)
        {
            //SessionSjekk.sjekkForRettighetPaaInnloggetBruker(Konstanter.rettighet.Prosjektleder);

            // Henter alle medlemmene av et team hvis team er valgt og fyller de inn i FaselederDropDown
            if (ddFaseLeder.SelectedValue != null && ddFaseLeder.SelectedValue == "0")
            {
                if (dropTeam.Items != null && dropTeam.SelectedValue != "0")
                {
                    team_id = Validator.KonverterTilTall(dropTeam.SelectedValue);

                    var teamMedlemerListe = Queries.GetAlleBrukereIEtTeam(team_id);
                    if (teamMedlemerListe.Count > 0)
                    {
                        ddFaseLeder.Items.Clear();
                        ListItem firstItem = new ListItem();
                        firstItem.Text = "Velg Faseleder";
                        firstItem.Value = "0";
                        ddFaseLeder.Items.Add(firstItem);

                        foreach (Bruker bruker in teamMedlemerListe)
                        {
                            ListItem item = new ListItem();
                            item.Text = bruker.ToString();
                            item.Value = bruker.Bruker_id.ToString();
                            ddFaseLeder.Items.Add(item);
                        }
                    }

                }

            }
            //Viser fram faselisten
            if (faseListe.Count > 0)
            {
                makeDataTableForFaser();
                gvFaser = new GridView();
                gvFaser.DataSource = dtFaser;
                gvFaser.DataBind();
                phFaser.Controls.Add(gvFaser);
            }
            else {
                gvFaser = null;
                phFaser.Controls.Clear();
            }


            if (!IsPostBack)
            {
                teamListe = Queries.GetAlleAktiveTeam();
                brukerListe = Queries.GetProsjektledere(Konstanter.rettighet.Prosjektleder);
                
                for (int i = 0; i < teamListe.Count(); i++)
                {
                    Team team = teamListe[i];
                   
                    dropTeam.Items.Add(new ListItem(team.Navn, team.Team_id.ToString()));
                }
                for (int i = 0; i < brukerListe.Count(); i++)
                {
                    Bruker bruker = brukerListe[i]; 
                    ddlBrukere.Items.Add(new ListItem(bruker.ToString(), bruker.Bruker_id.ToString()));
                }
            }
        }

        protected void btnLagre_Click(object sender, EventArgs e)
        {
            opprettProsjekt();
        }

        private void opprettProsjekt()
        {
            lblFeil.Visible = false;

            if (tbProsjektnavn.Text != String.Empty && tbStart.Text != String.Empty && tbSlutt.Text != String.Empty && dropTeam.SelectedValue != "0" && ddlBrukere.SelectedValue != "0")
            {

                dtStart = Convert.ToDateTime(tbStart.Text);
                dtSlutt = Convert.ToDateTime(tbSlutt.Text);


                team_id = Convert.ToInt32(dropTeam.SelectedValue);
                Team team = Queries.GetTeamById(team_id);
                bruker_id = Convert.ToInt32(ddlBrukere.SelectedValue);

                using (var context = new Context())
                {
                    var nyttProsjekt = new Prosjekt { Navn = tbProsjektnavn.Text, Bruker_id = bruker_id, Aktiv = true, Opprettet = DateTime.Now, Team_id = team_id, StartDato = dtStart, SluttDato = dtSlutt };
                    context.Prosjekter.Add(nyttProsjekt);
                    context.SaveChanges();
                }

                lblFeil.Visible = true;
                lblFeil.ForeColor = Color.Green;
                lblFeil.Text = "Prosjektet ble lagret!";
                Response.AddHeader("REFRESH", "3;URL=OpprettProsjekt");

                Varsel.SendVarsel(team.Brukere, Varsel.PROSJEKTVARSEL, "Du har blitt lagt til i prosjekt "
                    + tbProsjektnavn.Text + " av prosjektleder " + ddlBrukere.SelectedItem.ToString());

            }
            else
            {
                lblFeil.Visible = true;
                lblFeil.ForeColor = Color.Red;
                lblFeil.Text = "Feltene kan ikke være tomme";
            }
        }
    

        protected void btnDato_Click(object sender, EventArgs e)
        {
            lblFeil.Visible = false;
            if (cal.SelectedDate == DateTime.Parse("01.01.0001"))
            {
                lblFeil.Visible = true;
                lblFeil.Text = "Du må velge en dato";
                lblFeil.ForeColor = Color.Red;
            }
            else
            {
                Button clickedButton = (Button)sender;
                string buttonId = clickedButton.ID;

                switch (buttonId)
                {
                    case BTN_START:
                        tbStart.Text = cal.SelectedDate.ToShortDateString();
                        break;
                    case BTN_SLUTT:
                        tbSlutt.Text = cal.SelectedDate.ToShortDateString();
                        break;
                    case BTN_FASE_START:
                        tbFaseStart.Text = cal.SelectedDate.ToShortDateString();
                        break;
                    case BTN_FASE_SLUTT:
                        tbFaseSlutt.Text = cal.SelectedDate.ToShortDateString();
                        break;
                    default:
                        break;
 
                }
            }      
        }

        protected void TeamDropDown_Change(object sender, EventArgs e)
        {
            DropDownList TeamDropDown = (DropDownList)sender;
            if (TeamDropDown.SelectedIndex != null && TeamDropDown.SelectedIndex > 0)
            {
                team_id = Validator.KonverterTilTall(TeamDropDown.SelectedValue);
            }
        }

        protected void btnLeggTilFase_Click(object sender, EventArgs e)
        {
            if (tbFasenavn.Text != string.Empty && tbFaseStart.Text != string.Empty && tbFaseSlutt.Text != string.Empty && ddFaseLeder.SelectedValue != "0")
            {
                Fase nyFase = new Fase();
                nyFase.Navn = tbFasenavn.Text;
                nyFase.Start = Convert.ToDateTime(tbFaseStart.Text);
                nyFase.Stopp = Convert.ToDateTime(tbFaseSlutt.Text);
                nyFase.Bruker_id = Validator.KonverterTilTall(ddFaseLeder.SelectedValue);
                faseListe.Add(nyFase);
                lblFaseFeil.Visible = false;
            }
            else
            {
                lblFaseFeil.Visible = true;
            }
        }

        private void makeDataTableForFaser()
        {
            DataColumn colFaseNavn = new DataColumn("Fasenavn");
            DataColumn colFaseLeder = new DataColumn("Faseleder");
            DataColumn colFaseStart = new DataColumn("Start");
            DataColumn colFaseSlutt = new DataColumn("Slutt");
            dtFaser.Columns.Add(colFaseNavn);
            dtFaser.Columns.Add(colFaseLeder);
            dtFaser.Columns.Add(colFaseStart);
            dtFaser.Columns.Add(colFaseSlutt);

            foreach (Fase fase in faseListe)
            {
                DataRow row = dtFaser.NewRow();
                row[colFaseNavn] = fase.Navn;
                row[colFaseLeder] = fase.Bruker_id.ToString();
                row[colFaseStart] = fase.Start.ToString();
                row[colFaseSlutt] = fase.Stopp.ToString();
                dtFaser.Rows.Add(row);
            }

        }

    }
}