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
//        private List<Fase> faseListe = new List<Fase>();
        private DataTable dtFaser = new DataTable();
    
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


            if (!IsPostBack)
            {
                //Setter opp gridviewen for faser
              /*
                BoundField fieldFasenavn = new BoundField();
                BoundField fieldFaseleder = new BoundField();
                BoundField fieldFaseStart = new BoundField();
                BoundField fieldFaseSlutt = new BoundField();
                fieldFasenavn.HeaderText = "Fasenavn";
                fieldFaseleder.HeaderText = "Faseleder";
                fieldFaseStart.HeaderText = "Fase Start";
                fieldFaseSlutt.HeaderText = "Fase Slutt";
                gvFaser.Columns.Add(fieldFasenavn);
                gvFaser.Columns.Add(fieldFaseleder);
                gvFaser.Columns.Add(fieldFaseStart);
                gvFaser.Columns.Add(fieldFaseSlutt);
*/
                makeDataTableForFaser();
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
            if (TeamDropDown.Items != null && TeamDropDown.SelectedIndex > 0)
            {
                team_id = Validator.KonverterTilTall(TeamDropDown.SelectedValue);
            }
        }
        
        protected void btnLeggTilFase_Click(object sender, EventArgs e)
        {
            if (tbFasenavn.Text != string.Empty && tbFaseStart.Text != string.Empty && tbFaseSlutt.Text != string.Empty && ddFaseLeder.SelectedValue != "0")
            {
                AddNewFaseRow();
                lblFaseFeil.Visible = false;
            }
            else
            {
                lblFaseFeil.Visible = true;
            }
        }

        private void makeDataTableForFaser()
        {
            dtFaser.Columns.Add(new DataColumn("bruker_id", typeof(System.Int32)));
            dtFaser.Columns.Add(new DataColumn("Fasenavn", typeof(System.String)));
            dtFaser.Columns.Add(new DataColumn("Faseleder", typeof(System.String)));
            dtFaser.Columns.Add(new DataColumn("Start", typeof(System.String)));
            dtFaser.Columns.Add(new DataColumn("Slutt", typeof(System.String)));

            DataRow row = dtFaser.NewRow();
            row["bruker_id"] = Validator.KonverterTilTall("1");
            row["Fasenavn"] = "TestFase";
            row["Faseleder"] = "Tony";
            row["Start"] = "14.05.2014";
            row["Slutt"] = "21.05.2014";
            dtFaser.Rows.Add(row);

            DataRow row2 = dtFaser.NewRow();
            row2["bruker_id"] = Validator.KonverterTilTall("2");
            row2["Fasenavn"] = "TestFase2";
            row2["Faseleder"] = "Lars";
            row2["Start"] = "14.05.2014";
            row2["Slutt"] = "21.05.2014";
            dtFaser.Rows.Add(row2);

            DataRow row3 = dtFaser.NewRow();
            row3["bruker_id"] = Validator.KonverterTilTall("3");
            row3["Fasenavn"] = "TestFase2";
            row3["Faseleder"] = "Markus";
            row3["Start"] = "14.05.2014";
            row3["Slutt"] = "21.05.2014";
            dtFaser.Rows.Add(row3);

            Session["FaseTable"] = dtFaser;

            gvFaser.DataSource = dtFaser;
            gvFaser.DataBind();
        }

        private void AddNewFaseRow()
        {

            if (Session["FaseTable"] != null)
            {
                dtFaser = Session["FaseTable"] as DataTable;

                DataRow row = dtFaser.NewRow();
                row["bruker_id"] = ddFaseLeder.SelectedValue;
                row["Fasenavn"] = tbFasenavn.Text;
                row["Faseleder"] = ddFaseLeder.SelectedItem.Text;
                row["Start"] = tbFaseStart.Text;
                row["Slutt"] = tbFaseSlutt.Text;

                dtFaser.Rows.Add(row);
                Session["FaseTable"] = dtFaser;

                gvFaser.DataSource = dtFaser;
                gvFaser.DataBind();

                //Tømmer input feltene
                tbFasenavn.Text = string.Empty;
                ddFaseLeder.SelectedIndex = 0;
                tbFaseStart.Text = string.Empty;
                tbFaseSlutt.Text = string.Empty;
            }
        }
        protected void OnRowDeleting(object sender, GridViewDeleteEventArgs e)
        {
           int index = Convert.ToInt32(e.RowIndex);
           dtFaser = Session["FaseTable"] as DataTable;
           dtFaser.Rows[index].Delete();
           Session["FaseTable"] = dtFaser;
           gvFaser.DataSource = dtFaser;
           gvFaser.DataBind();
        }
    }
}