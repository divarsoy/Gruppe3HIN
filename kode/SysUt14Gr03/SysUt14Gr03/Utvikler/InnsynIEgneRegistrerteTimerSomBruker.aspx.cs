﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SysUt14Gr03.Models;
using SysUt14Gr03.Classes;

namespace SysUt14Gr03.Utvikler
{
    public partial class InnsynIEgneRegistrerteTimerSomBruker : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            SessionSjekk.sjekkForBruker_id();

            int brukerId = Validator.KonverterTilTall(Session["bruker_id"].ToString());
            List<Time> timer = Queries.GetTimerForBruker(brukerId);
            int prosjektId = Validator.KonverterTilTall(Session["prosjekt_id"].ToString());
            Prosjekt prosjekt = Queries.GetProsjekt(prosjektId);

                if (!IsPostBack)
                {
                    Table timeTabell = Tabeller.HentTimerForBruker(timer, brukerId, prosjekt);

                    PlaceHolderTable.Controls.Add(timeTabell);
                }
            
            
        }
    }
}