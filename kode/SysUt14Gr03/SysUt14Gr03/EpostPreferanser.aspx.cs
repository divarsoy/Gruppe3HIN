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
    /// Lar brukeren velge hvilke varsler han skal få på epost fra
    /// en liste
    /// </summary>
    public partial class EpostPreferanser : System.Web.UI.Page
    {
        private List<string> elementer = new List<string>();
        public int brukerId { get; set; }
        public bool[] selectedItems { get; set; }
        public string brukernavn { get; set; }


        protected void Page_Load(object sender, EventArgs e)
        {
            // Får innlogget brukerId
            brukerId = 1;
            if (cblElementer.Items.Count == 0)
            {
                cblElementer.Items.Add("lagt til i team");
                cblElementer.Items.Add("lagt til i prosjekt");
                cblElementer.Items.Add("tildelt en oppgave");
                cblElementer.Items.Add("nevnt i en kommentar");
                cblElementer.Items.Add("tildelt en tidsfrist på oppgave");
                cblElementer.Items.Add("rapportert");
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnLagre_Click(object sender, EventArgs e)
        {

            lagrePreferanser(false);

        }

        protected void btnAvbryt_Click(object sender, EventArgs e)
        {
            Response.Redirect("default.aspx", true);
        }

        

        public String lagrePreferanser(bool test)
        {

            var nyBrukerpreferanser = new BrukerPreferanse { Bruker_id = brukerId, EpostTeam = selectedItems[0], EpostProsjekt = selectedItems[1], EpostOppgave = selectedItems[2], EpostKommentar = selectedItems[3], EpostTidsfrist = selectedItems[4], EpostRapport = selectedItems[5] };
            string info;

            if (!test)
            {
                selectedItems = new bool[cblElementer.Items.Count];

                for (int i = 0; i < cblElementer.Items.Count; i++)
                {
                    selectedItems[i] = cblElementer.Items[i].Selected;
                }
                using (var db = new Context())
                {

                    db.BrukerPreferanser.Add(nyBrukerpreferanser);
                    db.SaveChanges();
                }
                info = Queries.GetBruker(brukerId).Fornavn;
            }




            info = brukernavn;
            for (int i = 0; i < selectedItems.Length; i++)
                info += " " + selectedItems[i].ToString();

            return info;
        }
    }
}