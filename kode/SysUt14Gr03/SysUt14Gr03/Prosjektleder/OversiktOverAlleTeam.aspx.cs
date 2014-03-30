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
    public partial class OversiktOverTeam : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            List<Team> teamene = Queries.GetAlleAktiveTeam();

            foreach (Team t in teamene)
            {
                List<Bruker> query = Queries.GetAlleBrukerePaaTeam(t.Team_id);

                if (!IsPostBack)
                {
                    Table brukerTabell = Tabeller.HentBrukerTabellForTeam(query, t);
                    
                    PlaceHolderTable.Controls.Add(brukerTabell);
                }
            }
        }
    }
}