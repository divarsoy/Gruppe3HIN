﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SysUt14Gr03.Classes;
using SysUt14Gr03.Models;

namespace SysUt14Gr03
{
    public partial class OversiktBrukerSomUtvikler : System.Web.UI.Page
    {
        private List<Bruker> queryProsjekt = null;
        Button btnProsjekt;

        protected void Page_Load(object sender, EventArgs e)
        {
            bool queryStatus = false;
            int bruker_id = 2;

            
            // Sjekker om det er lagt ved et Get parameter "prosjekt_id" og lager en spørring basert på prosjekt_id og team_id på innlogget bruker
            if (Request.QueryString["prosjekt_id"] != null)
            {
                int prosjekt_id = 4; // Validator.KonverterTilTall(Request.QueryString["prosjekt_id"]);
                if (prosjekt_id >= 1)
                {
                    queryProsjekt = Queries.GetAlleBrukereIEtProjekt(prosjekt_id);
                    if (queryProsjekt.Count == 0)
                    {
                        lblTilbakemelding.Text = "Brukeren er ikke i ditt prosjekt";
                    }
                    else
                    {
                        string prosjektNavn = Queries.GetProsjekt(prosjekt_id).Navn;
                        string brukerNavn = Queries.GetBruker(bruker_id).ToString();
                        lblTilbakemelding.Text = string.Format("<h3>Prosjekt: {0}</h3><h3>Bruker: {1}</h3>", prosjektNavn, brukerNavn);
                        queryStatus = true;
                    }
                }
                else
                {
                    lblTilbakemelding.Text = "Brukeren er ikke i ditt prosjekt";
                }
            }
            // Dersom prosjekt eller team ikke er oppgitt lages en spørring basert på bruker_id til innlogget bruker
            else
            {
                int prosjekt_id = 4; // Validator.KonverterTilTall(Request.QueryString["prosjekt_id"]);
                queryProsjekt = Queries.GetAlleBrukereIEtProjekt(prosjekt_id);
                string brukerNavn = Queries.GetBruker(bruker_id).ToString();
                lblTilbakemelding.Text = string.Format("<h3>Bruker: {0}</h3>", brukerNavn);
                queryStatus = true;
            }
            // Lager Tabell for å vise oppgaver
            Table prosjektTable = Tabeller.HentBrukerTabellIProsjektTeamUtviklere(queryProsjekt);
            prosjektTable.CssClass = "table";
            if (!IsPostBack && queryStatus)
            {
                
                PlaceHolderTableProject.Controls.Add(prosjektTable);
                btnProsjekt = (prosjektTable.FindControl("btnProsjekt") as Button);
            }    
            //PlaceHolderTableProject.FindControl()
            //Button btnProsjekt = new Button();
            btnProsjekt.Click += Button1_Click;
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            foreach(Bruker bruker in queryProsjekt)
            {
                DropDownList ddlProsjekt = new DropDownList();
                for (int i = 0; i < bruker.Prosjekter.Count; i++)
                {
                    Prosjekt prosjekt = bruker.Prosjekter[i];
                    ddlProsjekt.Items.Add(new ListItem(prosjekt.Navn, prosjekt.Prosjekt_id.ToString()));
                }
                string id = ddlProsjekt.SelectedValue;
                //Response.Redirect("HistorikkStattestikk.aspx?Prosjekt_id=" + id);
            }
        }
    }
}