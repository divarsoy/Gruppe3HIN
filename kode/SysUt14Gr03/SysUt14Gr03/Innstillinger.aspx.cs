using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SysUt14Gr03.Models;
using SysUt14Gr03.Classes;

namespace SysUt14Gr03
{
    public partial class Innstillinger : System.Web.UI.Page
    {
        private int bruker_id = 1;
        private Bruker bruker;
        private List<string> elementer = new List<string>();
        private BrukerPreferanse brukerPrefs;
        public bool[] selectedItems { get; set; }
        public string brukernavn { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            //SessionSjekk.sjekkForBruker_id();
            //bruker_id = Validator.KonverterTilTall(Session["bruker_id"].ToString());
            bruker = Queries.GetBruker(bruker_id);
            if (bruker != null)
            {
                txtFornavn.Text = bruker.Fornavn;
                txtEtternavn.Text = bruker.Etternavn;
                txtBrukernavn.Text = bruker.Brukernavn;
                txtIM.Text = bruker.IM;
                if (cblElementer.Items.Count == 0)
                {
                    cblElementer.Items.Add("lagt til i team");
                    cblElementer.Items.Add("lagt til i prosjekt");
                    cblElementer.Items.Add("tildelt en oppgave");
                    cblElementer.Items.Add("nevnt i en kommentar");
                    cblElementer.Items.Add("tildelt en tidsfrist på oppgave");
                }

                if (!IsPostBack)
                {
                    brukerPrefs = Queries.GetEpostPreferanser(bruker_id);

                    if (brukerPrefs != null)
                    {
                        // Setter valgte verdier
                        cblElementer.Items[0].Selected = brukerPrefs.EpostTeam;
                        cblElementer.Items[1].Selected = brukerPrefs.EpostProsjekt;
                        cblElementer.Items[2].Selected = brukerPrefs.EpostOppgave;
                        cblElementer.Items[3].Selected = brukerPrefs.EpostKommentar;
                        cblElementer.Items[4].Selected = brukerPrefs.EpostTidsfrist;
                    }
                }
            }

        }

        protected void btnLagre_Click(object sender, EventArgs e)
        {
            // Lagrer innstillinger til databasen..
            lagrePreferanser(false);

            if (txtFornavn.Text != string.Empty &&
                txtEtternavn.Text != string.Empty &&
                txtBrukernavn.Text != string.Empty &&
                txtIM.Text != string.Empty)
            {
                Bruker brukerTestIM = Queries.GetBrukerVedIM(txtIM.Text);
                if (brukerTestIM.IM == string.Empty)
                {
                    using (var context = new Context())
                    {
                        Bruker _bruker = context.Brukere.Where(b => b.Bruker_id == bruker_id).FirstOrDefault();

                        _bruker.Fornavn = txtFornavn.Text.Trim();
                        _bruker.Etternavn = txtEtternavn.Text.Trim();
                        _bruker.Brukernavn = txtBrukernavn.Text.Trim();
                        _bruker.IM = txtIM.Text.Trim();

                        context.SaveChanges();
                        Session["flashMelding"] = "Innstillinger lagret";
                        Session["flashStatus"] = Konstanter.notifikasjonsTyper.success.ToString();
                    }
                }
                else
                {
                    Session["flashMelding"] = "\"" + txtIM + "\" brukes av en annen bruker";
                    Session["flashStatus"] = Konstanter.notifikasjonsTyper.danger.ToString();
                }
                
            }
            else
            {
                Session["flashMelding"] = "Et eller flere felt er ikke fylt ut";
                Session["flashStatus"] = Konstanter.notifikasjonsTyper.danger.ToString();
            }
        }

        protected void btnLagrePassord_Click(object sender, EventArgs e)
        {

            if (txtPassord.Text != string.Empty &&
                txtNyPass.Text != string.Empty &&
                txtNyPass1.Text != string.Empty)
            {
                string oppgittPassord = txtPassord.Text;
                string nyttPassord = txtNyPass.Text;
                string nyttPassord1 = txtNyPass1.Text;
                string salt = bruker.Salt;
                string hash = bruker.Passord;
                if (Hash.CheckPassord(oppgittPassord, hash, salt))
                {
                    if (nyttPassord.Equals(nyttPassord1)) {
                        using (var context = new Context())
                        {
                            Bruker _bruker = context.Brukere.Where(b => b.Bruker_id == bruker_id).FirstOrDefault();

                            string nyttSalt = Hash.GetSalt();
                            _bruker.Salt = nyttSalt;
                            _bruker.Passord = Hash.GetHash(nyttPassord, nyttSalt);

                            context.SaveChanges();

                            Session["flashMelding"] = "Nytt passord lagret";
                            Session["flashStatus"] = Konstanter.notifikasjonsTyper.success.ToString();
                        }

                    }
                    else {
                        Session["flashMelding"] = "Passordene er ikke like";
                        Session["flashStatus"] = Konstanter.notifikasjonsTyper.danger.ToString();
                    }
                }
                else
                {
                    Session["flashMelding"] = "Feil passord oppgitt for bruker";
                    Session["flashStatus"] = Konstanter.notifikasjonsTyper.danger.ToString();
                }
            }
                    
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
                Bruker_id = bruker_id,
                EpostTeam = selectedItems[0],
                EpostProsjekt = selectedItems[1],
                EpostOppgave = selectedItems[2],
                EpostKommentar = selectedItems[3],
                EpostTidsfrist = selectedItems[4],
            };

            string info;

            if (!test)
            {

                using (var db = new Context())
                {

                    BrukerPreferanse brukerPref = db.BrukerPreferanser.FirstOrDefault(o => o.Bruker_id == bruker_id);
                    if (brukerPref != null)
                    {
                        brukerPref.EpostTeam = selectedItems[0];
                        brukerPref.EpostProsjekt = selectedItems[1];
                        brukerPref.EpostOppgave = selectedItems[2];
                        brukerPref.EpostKommentar = selectedItems[3];
                        brukerPref.EpostTidsfrist = selectedItems[4];
                    }
                    else
                    {
                        db.BrukerPreferanser.Add(nyBrukerpreferanser);
                    }

                    db.SaveChanges();
                }
                info = Queries.GetBruker(bruker_id).Fornavn;
            }

            info = brukernavn;
            for (int i = 0; i < selectedItems.Length; i++)
                info += " " + selectedItems[i].ToString();

            return info;
        }

    }
}