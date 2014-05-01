﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SysUt14Gr03;
using SysUt14Gr03.Models;
using SysUt14Gr03.Classes;
using System.Data;

namespace SysUt14Gr03
{
    public partial class DefaultProsjektleder : System.Web.UI.Page
    {
        private int bruker_id;
        List<Prosjekt> listeMedProsjekter;

        protected void Page_Load(object sender, EventArgs e)
        {
            SessionSjekk.sjekkForRettighetPaaInnloggetBruker(Konstanter.rettighet.Prosjektleder);

            if (Session["prosjekt_id"] != null)
            {
                string prosjektNavn = Session["prosjekt_navn"].ToString();
                lblValgtProsjekt.Text = String.Format("Valgt prosjekt er <b>{0}</b>", prosjektNavn);
            }

            if (!Page.IsPostBack)
            {
                bruker_id = Validator.KonverterTilTall(Session["bruker_id"].ToString());

                Bruker bruker = Queries.GetBruker(bruker_id);

                // Henter alle aktive prosjekter for innlogget bruker
                if (ListBoxProsjekt.SelectedValue != null && ListBoxProsjekt.SelectedValue == "0")
                {
                    listeMedProsjekter = Queries.GetAlleAktiveProsjekterForProsjektLeder(bruker_id);
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
                Session["prosjekt_id"] = prosjekt_id;
                Prosjekt prosjekt = Queries.GetProsjekt(prosjekt_id);
                Session["prosjekt_navn"] = prosjekt.Navn;
                lblValgtProsjekt.Text = String.Format("Valgt prosjekt er <b>{0}</b>", prosjekt.Navn);
                //Response.Redirect(String.Format("OversiktOppgaver?bruker_id={0}&prosjekt_id={1}", Session["bruker_id"], ListBoxProsjekt.SelectedItem.Value));
            }
        }
    }
}