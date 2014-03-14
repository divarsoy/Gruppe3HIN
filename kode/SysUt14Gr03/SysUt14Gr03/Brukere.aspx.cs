using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SysUt14Gr03;
using SysUt14Gr03.Models;


namespace SysUt14Gr03
{
    public partial class Brukere : System.Web.UI.Page
    {
        public BrukerEksempel bruker;
        public List<Bruker> listeMedBrukere;
        
        protected void Page_Load(object sender, EventArgs e)
        {
        //    using (var context = new Context()) {
        //        listeMedBrukere = (from b in context.Brukere
        //                              where b.Teams.Any(t => t.Navn == "Alpha")
        //                              select b);
       //}

            bruker = new BrukerEksempel();
            bruker.Etternavn = "Dag";
            bruker.Fornavn = "Ivarsøy";
        }
    }
}