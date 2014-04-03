﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SysUt14Gr03.Classes;
using SysUt14Gr03.Models;

namespace SysUt14Gr03
{
    public partial class OversiktProsjekterSomProsjektleder : System.Web.UI.Page
    {
        private int brukerid;
        private List<Prosjekt> prosjektListe;
        protected void Page_Load(object sender, EventArgs e)
        {
            SessionSjekk.sjekkForRettighetPaaInnloggetBruker(Konstanter.rettighet.Prosjektleder);

            brukerid = Validator.KonverterTilTall(Session["bruker_id"].ToString());

            if (!IsPostBack)
            {
                prosjektListe = Queries.GetAlleProsjekterForLeder(brukerid);
                Table prosjektTabell = Tabeller.HentProsjekterTabellProsjektLeder(prosjektListe);
                ProsjektTable.Controls.Add(prosjektTabell);
            }
        }

        protected void BtnOpprettProsjekt_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Prosjektleder/OpprettProsjekt");
        }
    }
}