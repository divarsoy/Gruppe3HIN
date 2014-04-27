using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SysUt14Gr03.Classes;
using SysUt14Gr03.Models;

namespace SysUt14Gr03
{
    public partial class OversiktBrukereSomAdministrator : System.Web.UI.Page
    {
        private int brukerid;

        protected void Page_Load(object sender, EventArgs e)
        {
            SessionSjekk.sjekkForRettighetPaaInnloggetBruker(Konstanter.rettighet.Administrator);
            if (!IsPostBack)
            {
                List<Bruker> brukerListe = Queries.GetAlleBrukere();
                //PlaceHolderBrukere.Controls.Add(Tabeller.HentBrukereTabellForAdministrator(brukerListe));                    
            }            
        }
    }
}
