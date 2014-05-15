using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SysUt14Gr03;
using SysUt14Gr03.Classes;
using SysUt14Gr03.Models;

/// <summary>
/// Forsiden for utvikler/faseleder. Man kan se kalenderen, velge et prosjekt og se hvilken prosjekt som er valgt. Står også at dette er utvikler forsiden :P 
/// </summary>

namespace SysUt14Gr03
{
    public partial class BrukerForside : System.Web.UI.Page
    {
        private int bruker_id;
        List<Prosjekt> listeMedProsjekter;

        protected void Page_PreInit(Object sener, EventArgs e)
        {
            string master = SessionSjekk.findMaster();
            this.MasterPageFile = master;
            
            if (Session["prosjekt_id"] != null)
            {

                int prosjekt_id = Validator.KonverterTilTall(Session["prosjekt_id"].ToString());
                if (SessionSjekk.IsFaseleder(prosjekt_id))
                {
                    List<Oppgave> oppgaveListe = Queries.GetAlleAktiveOppgaverForProsjekt(prosjekt_id);
                    List<Time> timeListe = new List<Time>();
                    List<string> infoListe = new List<string>();

                    foreach (Oppgave o in oppgaveListe)
                    {
                        timeListe.AddRange(Queries.GetTimerForGodkjenning(o.Oppgave_id));
                    }

                    if (timeListe.Count > 0)
                    {
                        Session["flashMelding"] = "Det finnes timeregistreringer i prosjektet som må godkjennes. <a href=\"../GodkjennTimer.aspx\">Vis oversikt</a>";
                        Session["flashStatus"] = Konstanter.notifikasjonsTyper.info.ToString();
                    }
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {

            SessionSjekk.sjekkForRettighetPaaInnloggetBruker(Konstanter.rettighet.Utvikler);
            bruker_id = Validator.KonverterTilTall(Session["bruker_id"].ToString());


            if (Session["prosjekt_id"] != null)
            {
                string prosjektNavn = Session["prosjekt_navn"].ToString();
                lblValgtProsjekt.Text = String.Format("Valgt prosjekt er <b>{0}</b>", prosjektNavn);
            }

            if (!Page.IsPostBack)
            {
                Bruker bruker = Queries.GetBruker(bruker_id);

                //Sjekker om sheperd skal deaktiveres eller vises
                if (Request.QueryString["sheperd"] != null)
                {
                    Queries.SetSheperd(bruker_id);
                }
                else if (Queries.GetBrukerPreferanse(bruker_id).Sheperd)
                {
                    ScriptManager.RegisterClientScriptInclude(this.Page, this.GetType(), "jquery", "../Scripts/jquery-1.10.2.js");
                    ScriptManager.RegisterClientScriptInclude(this.Page, this.GetType(), "SheperdScript", "../Scripts/MorildShepherdUtvikler.js");
                }
                
                // Henter alle aktive prosjekter for innlogget bruker
                if (ListBoxProsjekt.SelectedValue != null && ListBoxProsjekt.SelectedValue == "0")
                {
                    listeMedProsjekter = Queries.GetAlleAktiveProsjekterForBruker(bruker_id);
                    if (listeMedProsjekter.Count > 0)
                    {
                        ListBoxProsjekt.Items.Clear();
                        ListItem firstItem = new ListItem();
                        firstItem.Text = "Velg Prosjekt";
                        firstItem.Value = "0";
                        ListBoxProsjekt.Items.Add(firstItem);

                        foreach (Prosjekt prosjekt in listeMedProsjekter)
                        {
                            ListItem item = new ListItem();
                            item.Text = prosjekt.Navn;
                            item.Value = prosjekt.Prosjekt_id.ToString();
                            ListBoxProsjekt.Items.Add(item);
                        }
                    }
                    ListBoxProsjekt.CssClass = "form-control";
                    btnVelgProsjekt.CssClass = "btn btn-primary";
                }

            }

        }

        protected void btnVelgProsjekt_Click(object sender, EventArgs e)
        {
            if (ListBoxProsjekt.Items.Count > 0 && ListBoxProsjekt.SelectedValue != null && ListBoxProsjekt.SelectedValue != "0")
            {
                int prosjekt_id = Validator.KonverterTilTall(ListBoxProsjekt.SelectedItem.Value);
                Session["prosjekt_id"] = ListBoxProsjekt.SelectedItem.Value;
                Prosjekt prosjekt = Queries.GetProsjekt(prosjekt_id);
                Session["prosjekt_navn"] = prosjekt.Navn;
                lblValgtProsjekt.Text = String.Format("<h4>Valgt prosjekt er <b>{0}</b></h4>", prosjekt.Navn);
                //Response.Redirect(String.Format("OversiktOppgaver?bruker_id={0}&prosjekt_id={1}", Session["bruker_id"], ListBoxProsjekt.SelectedItem.Value));
                Response.Redirect(Request.Url.ToString());
            }
        }
    }
}