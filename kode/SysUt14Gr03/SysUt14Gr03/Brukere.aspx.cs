using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SysUt14Gr03;
using SysUt14Gr03.Models;
using SysUt14Gr03.Classes;


namespace SysUt14Gr03
{
    public partial class Brukere : System.Web.UI.Page
    {
        public BrukerEksempel bruker;
        public List<Bruker> listeMedBrukere;
        private int brukerId;

        protected void Page_PreInit(Object sener, EventArgs e)
        {
            string master = SessionSjekk.findMaster();
            this.MasterPageFile = master;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
        //    using (var context = new Context()) {
        //        listeMedBrukere = (from b in context.Brukere
        //                              where b.Teams.Any(t => t.Navn == "Alpha")
        //                              select b);
       //}
            if (Session["loggedIn"] == null)
            {
                lblNavn.Visible = false;
            }
            else
            {
                brukerId = Convert.ToInt32(Session["bruker_id"]);
                Bruker bruker = Queries.GetBruker(brukerId);
                lblNavn.Text = bruker.ToString();
            }

            int team_id = 2;
            using (var context = new Context())
            {
                var brukerListe = (from bruker in context.Brukere
                                   where bruker.Teams.Any(team => team.Team_id == team_id)
                                   select bruker).ToList();
                 

                List<string> liste = new List<string>();
                foreach (var brukers in brukerListe) {
                    liste.Add(brukers.Brukernavn);
                }
                //return brukerListe;
             }
        }
    }
}