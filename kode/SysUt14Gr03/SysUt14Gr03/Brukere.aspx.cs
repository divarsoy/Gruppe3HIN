using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SysUt14Gr03;


namespace SysUt14Gr03
{
    public partial class Brukere : System.Web.UI.Page
    {
        public BrukerEksempel bruker; 
        
        protected void Page_Load(object sender, EventArgs e)
        {
            bruker = new BrukerEksempel();
            bruker.Etternavn = "Dag";
            bruker.Fornavn = "Ivarsøy";
        }
    }
}