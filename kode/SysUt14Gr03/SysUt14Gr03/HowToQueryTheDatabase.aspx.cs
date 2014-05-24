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
    /// <summary>
    /// Denne ble laget helt i starten bare for oss siden vi alle var nybegynner med MSQL databasen. så denne
    /// skulle bare være en bruksanvisning for oss for å kunne hente ting ut av databasen.
    /// </summary>
    public partial class HowToQueryTheDatabase : System.Web.UI.Page
    {
        public Bruker bruker;
        public string fornavn;
        public List<Bruker> brukerListe;

        protected void Page_PreInit(Object sener, EventArgs e)
        {
            string master = SessionSjekk.findMaster();
            this.MasterPageFile = master;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            int bruker_id = 1;
            bruker = Queries.GetBruker(bruker_id);

            brukerListe = Queries.GetAlleAktiveBrukere();
        }
    }
}