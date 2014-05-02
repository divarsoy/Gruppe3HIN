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
    
        private int team_id;
        private int bruker_id;
        private const string BTN_START = "btnStart";
        private const string BTN_SLUTT = "btnSlutt";
        private const string BTN_FASE_START = "btnFaseStart";
        private const string BTN_FASE_SLUTT = "btnFaseSlutt";

        protected void Page_Load(object sender, EventArgs e)
        {
            SessionSjekk.sjekkForRettighetPaaInnloggetBruker(Konstanter.rettighet.Prosjektleder);

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
                dtFaser = Session["FaseTable"] as DataTable;
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

                    if (dtFaser != null)
                    {
                        for (int i = 0; i < dtFaser.Rows.Count; i++)
                        {
                            Fase fase = new Fase
                            {
                                Navn = dtFaser.Rows[i]["Fasenavn"].ToString(),
                                Bruker_id = Convert.ToInt32(dtFaser.Rows[i]["bruker_id"]),
                                Start = Convert.ToDateTime(dtFaser.Rows[i]["Start"]),
                                Stopp = Convert.ToDateTime(dtFaser.Rows[i]["Slutt"]),
                                Opprettet = DateTime.Now,
                                Aktiv = true,
                                Prosjekt_id = nyttProsjekt.Prosjekt_id
                            };
                            context.Faser.Add(fase);
                        }
                        context.SaveChanges();
                    }
                }

//                lblFeil.Visible = true;
//                lblFeil.ForeColor = Color.Green;
//                lblFeil.Text = "Prosjektet ble lagret!";
//                Response.AddHeader("REFRESH", "3;URL=OpprettProsjekt");

                Varsel.SendVarsel(team.Brukere, Varsel.PROSJEKTVARSEL, "Du har blitt lagt til i prosjekt "
                    + tbProsjektnavn.Text + " av prosjektleder " + ddlBrukere.SelectedItem.ToString());

                Session["flashMelding"] = string.Format("Prosjektet '{0}' ble opprettet ", tbProsjektnavn.Text);
                Session["flashStatus"] = Konstanter.notifikasjonsTyper.success.ToString();
                Session["dtFaser"] = null;

                //Oppretter logg for opprettelse av prosjektet
                String hendelse = "Prosjektet " + tbProsjektnavn.Text + "ble opprettet med startdato " + tbStart.Text
                    + " og sluttdato " + tbSlutt.Text;
                OppretteLogg.opprettLoggForBruker(hendelse, DateTime.Now, (int)Session["bruker_id"]);

                Response.Redirect("~/OversiktProsjekter", true);

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

            BindGrid();
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

                BindGrid();

                //Tømmer input feltene
                tbFasenavn.Text = string.Empty;
                ddFaseLeder.SelectedIndex = 0;
                tbFaseStart.Text = string.Empty;
                tbFaseSlutt.Text = string.Empty;
            }
        }
        protected void FaserOnRowDeleting(object sender, GridViewDeleteEventArgs e)
        {
           int index = Convert.ToInt32(e.RowIndex);
           dtFaser = Session["FaseTable"] as DataTable;
           dtFaser.Rows[index].Delete();

           BindGrid();
        }

        protected void FaserRowEditing(object sender, GridViewEditEventArgs e)
        {
            gvFaser.EditIndex = e.NewEditIndex;
            dtFaser = Session["FaseTable"] as DataTable;
            BindGrid();
        }

        protected void FaserEditRowDataBound(object sender, GridViewRowEventArgs e)
        {
            // Denne ifen er utrolig viktig for å få hentet ut ddFaselederEdit!!! Dersom den ikke står her blir ddFaselederEdit = null
            if (e.Row.RowType == DataControlRowType.DataRow && (e.Row.RowState & DataControlRowState.Edit) == DataControlRowState.Edit)
            {
                DropDownList ddFaselederEdit = e.Row.FindControl("ddFaselederEdit") as DropDownList;
                int index = gvFaser.EditIndex;

                string faseleder_id = dtFaser.Rows[index]["bruker_id"].ToString();
                string faselederText = dtFaser.Rows[index]["Faseleder"].ToString();

                if (ddFaselederEdit != null)
                {
                    if (ddFaselederEdit.SelectedValue != null && ddFaselederEdit.SelectedValue != faseleder_id)
                    {
                        ddFaselederEdit.Items.Clear();
                        ListItem firstItem = new ListItem();
                        firstItem.Text = faselederText;
                        firstItem.Value = faseleder_id;
                        ddFaselederEdit.Items.Add(firstItem);

                        team_id = Validator.KonverterTilTall(dropTeam.SelectedValue);
                        var teamMedlemerListe = Queries.GetAlleBrukereIEtTeam(team_id);
                        foreach (Bruker bruker in teamMedlemerListe)
                        {
                            ListItem item = new ListItem();
                            item.Text = bruker.ToString();
                            item.Value = bruker.Bruker_id.ToString();
                            ddFaselederEdit.Items.Add(item);
                        }
                    }
                }
            }
        }       
 
        protected void FaserRowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvFaser.EditIndex = -1;
            dtFaser = Session["FaseTable"] as DataTable;
            BindGrid();

        }

        protected void FaserRowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            dtFaser = Session["FaseTable"] as DataTable;
            GridViewRow row = gvFaser.Rows[e.RowIndex];

            dtFaser.Rows[row.DataItemIndex]["bruker_id"] = gvFaser.DataKeys[e.RowIndex].Value.ToString();

            dtFaser.Rows[row.DataItemIndex]["Fasenavn"] = e.NewValues["Fasenavn"];
            dtFaser.Rows[row.DataItemIndex]["Start"] = e.NewValues["Start"];
            dtFaser.Rows[row.DataItemIndex]["Slutt"] = e.NewValues["Slutt"];
                        
            gvFaser.EditIndex = -1;

            BindGrid();
        }

        private void BindGrid()
        {
            Session["FaseTable"] = dtFaser;
            gvFaser.DataSource = dtFaser;
            gvFaser.DataBind();
        }

        protected void ddFaselederEdit_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList ddFaselederEdit = sender as DropDownList;
            int index = gvFaser.EditIndex;
            DataTable dtFaser = Session["FaseTable"] as DataTable;
            string nyFaselederId = ddFaselederEdit.SelectedValue;
            string nyFaseleder = ddFaselederEdit.SelectedItem.Text;
            dtFaser.Rows[index]["bruker_id"] = Validator.KonverterTilTall(nyFaselederId);
            dtFaser.Rows[index]["Faseleder"] = nyFaseleder;
            Session["FaseTable"] = dtFaser;
        }
    }
}