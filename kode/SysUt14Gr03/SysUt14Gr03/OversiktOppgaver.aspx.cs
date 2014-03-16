using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SysUt14Gr03.Classes;
using SysUt14Gr03.Models;
using SysUt14Gr03;
using System.Text;
using System.Text.RegularExpressions;

namespace SysUt14Gr03
{
    public partial class OversiktOppgaver : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            bool queryStatus = true;
            List<Oppgave> query = null;

            if (Request.QueryString["prosjekt_id"] != null)
            {
                int prosjekt_id = Validator.KonverterTilTall(Request.QueryString["prosjekt_id"]);
                if (prosjekt_id > 0)
                {
                    query = Queries.GetAlleAktiveOppgaverForProsjekt(prosjekt_id);
                    if (query.Count == 0)
                    {
                        lblTilbakemelding.Text = "Du har valgt et ikke eksisterende prosjekt";
                        queryStatus = false;
                    }
                }
            }
            else
            {
                query = Queries.GetAlleAktiveOppgaverDag();
            }

            if (!IsPostBack && queryStatus)
            {
                Table oppgaveTable = Tabeller.HentOppgaveTabell(query);
                oppgaveTable.CssClass = "table";
                PlaceHolderTable.Controls.Add(oppgaveTable);
            }
        }
    }
}