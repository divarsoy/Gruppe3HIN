﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SysUt14Gr03.Models;
using SysUt14Gr03.Classes;
using System.Data;

namespace SysUt14Gr03
{
    public partial class SprintBacklogFase : System.Web.UI.Page
    {
        private Fase fase;
        private DataTable dt = new DataTable();
        protected void Page_Load(object sender, EventArgs e)
        {
            SessionSjekk.sjekkForProsjekt_id();
            int prosjekt_id = Validator.KonverterTilTall(Session["prosjekt_id"].ToString());
            if (!IsPostBack)
            {
                List<Fase> prosjektListe = Queries.GetFaseForProsjekt(prosjekt_id);
                foreach (Fase f in prosjektListe)
                {
                    ddlfaser.Items.Add(new ListItem(f.Navn, f.Fase_id.ToString()));
                }
            }
           fase = Queries.GetFase(Validator.KonverterTilTall(ddlfaser.SelectedValue));
           lblfasenavn.Text = fase.Navn + " i prosjekt: " + fase.Prosjekt.Navn;
           Table FaseTabell = Tabeller.HentFaseTabell(fase);
           FaseTabell.CssClass = "table";
           phFase.Controls.Add(FaseTabell);

        }

        protected void btnExport_Click(object sender, EventArgs e)
        {
            dt = DataTabeller.SprintBacklogFase(fase);
            EksporterTilExcel.CreateExcelDocument(dt, "SprintBacklog for fase.xlsx", Response);
        }
    }
}