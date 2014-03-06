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
    public partial class AdministrasjonAvTeamBrukere : System.Web.UI.Page
    {
        private List<Bruker> brukerListe;
        private List<Bruker> brukerPaaTeamListe;

        protected void Page_Load(object sender, EventArgs e)
        {
            brukerListe = Queries.GetAlleAktiveBrukere();

            if (cbl_brukere.Items.Count == 0)
            {
                for (int i = 0; i < brukerListe.Count(); i++)
                {
                    Bruker bruker = brukerListe[i];
                    cbl_brukere.Items.Add(bruker.Etternavn + ", " + bruker.Fornavn);
                }
            }
        }

    }
}