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
        private BrukerPreferanse brukerPrefs;
        public int brukerId { get; set; }
        public bool[] selectedItems { get; set; }
        public string brukernavn { get; set; }

        protected void Page_PreInit(Object sener, EventArgs e)
        {
            string master = SessionSjekk.findMaster();
            this.MasterPageFile = master;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            SessionSjekk.sjekkForBruker_id();
            brukerId = Validator.KonverterTilTall(Session["bruker_id"].ToString());

            if (cblElementer.Items.Count == 0)
            {
                cblElementer.Items.Add("lagt til i team");
                cblElementer.Items.Add("lagt til i prosjekt");
                cblElementer.Items.Add("tildelt en oppgave");
                cblElementer.Items.Add("nevnt i en kommentar");
                cblElementer.Items.Add("tildelt en tidsfrist på oppgave");
                cblElementer.Items.Add("Opplæring på førstesiden");
            }

            if (!IsPostBack)
            {
                brukerPrefs = Queries.GetBrukerPreferanse(brukerId);

                if (brukerPrefs != null)
                {
                    // Setter valgte verdier
                    cblElementer.Items[0].Selected = brukerPrefs.EpostTeam;
                    cblElementer.Items[1].Selected = brukerPrefs.EpostProsjekt;
                    cblElementer.Items[2].Selected = brukerPrefs.EpostOppgave;
                    cblElementer.Items[3].Selected = brukerPrefs.EpostKommentar;
                    cblElementer.Items[4].Selected = brukerPrefs.EpostTidsfrist;
                    cblElementer.Items[5].Selected = brukerPrefs.Sheperd;
                }
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
            Session["flashMelding"] = "Innstillinger lagret";
            Session["flashStatus"] = Konstanter.notifikasjonsTyper.success.ToString();

        }

        protected void btnAvbryt_Click(object sender, EventArgs e)
        {
            Response.Redirect("default.aspx", true);
        }



        public String lagrePreferanser(bool test)
        {
            if (!test)
            {
                selectedItems = new bool[cblElementer.Items.Count];

                for (int i = 0; i < cblElementer.Items.Count; i++)
                {
                    selectedItems[i] = cblElementer.Items[i].Selected;
                }
            }


            var nyBrukerpreferanser = new BrukerPreferanse
            {
                Bruker_id = brukerId,
                EpostTeam = selectedItems[0],
                EpostProsjekt = selectedItems[1],
                EpostOppgave = selectedItems[2],
                EpostKommentar = selectedItems[3],
                EpostTidsfrist = selectedItems[4],
                Sheperd = selectedItems[5]
            };

            string info;

            if (!test)
            {

                using (var db = new Context())
                {

                    BrukerPreferanse brukerPref = db.BrukerPreferanser.FirstOrDefault(o => o.Bruker_id == brukerId);
                    if (brukerPref != null)
                    {
                        brukerPref.EpostTeam = selectedItems[0];
                        brukerPref.EpostProsjekt = selectedItems[1];
                        brukerPref.EpostOppgave = selectedItems[2];
                        brukerPref.EpostKommentar = selectedItems[3];
                        brukerPref.EpostTidsfrist = selectedItems[4];
                        brukerPref.Sheperd = selectedItems[5];
                    }
                    else
                    {
                        db.BrukerPreferanser.Add(nyBrukerpreferanser);
                    }

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