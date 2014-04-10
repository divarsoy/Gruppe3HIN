using System;
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
        int brukerId = 2;
        Prosjekt prj = new Prosjekt();
        List<Time> timer;


        protected void Page_Load(object sender, EventArgs e)
        {
            timer = Queries.GetTimerForBruker(brukerId);
            prj.Navn = "EksempelProsjekt";

                if (!IsPostBack)
                {
                    Table timeTabell = Tabeller.HentTimerForBruker(timer, brukerId, prj);

                    PlaceHolderTable.Controls.Add(timeTabell);
                }
            
            
        }
    }
}