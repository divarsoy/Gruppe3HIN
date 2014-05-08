﻿using System;
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

        protected void Page_PreInit(Object sener, EventArgs e)
        {
            string master = SessionSjekk.findMaster();
            this.MasterPageFile = master;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            SessionSjekk.sjekkForBruker_id();
            bruker_id = Validator.KonverterTilTall(Session["bruker_id"].ToString());
            bruker = Queries.GetBruker(bruker_id);
            if (bruker != null)
            {
                if (!IsPostBack)
                {
                    txtFornavn.Text = bruker.Fornavn;
                    txtEtternavn.Text = bruker.Etternavn;
                    txtEpost.Text = bruker.Epost;
                    txtBrukernavn.Text = bruker.Brukernavn;
                    txtIM.Text = bruker.IM;
                }
                

                if (Validator.SjekkRettighet(bruker_id, Konstanter.rettighet.Administrator)) {
                    cblElementer.Visible = false;
                    Label1.Visible = false;
                }

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
                    brukerPrefs = Queries.GetBrukerPreferanse(bruker_id);

                    if (brukerPrefs != null)
                    {
                        // Setter valgte verdier
                        cblElementer.Items[0].Selected = brukerPrefs.EpostTeam;
                        cblElementer.Items[1].Selected = brukerPrefs.EpostProsjekt;
                        cblElementer.Items[2].Selected = brukerPrefs.EpostOppgave;
                        cblElementer.Items[3].Selected = brukerPrefs.EpostKommentar;
                        cblElementer.Items[4].Selected = brukerPrefs.EpostTidsfrist;
                        chkShepherd.Checked = brukerPrefs.Sheperd;
                    }
                }
            }

        }

        protected void btnLagre_Click(object sender, EventArgs e)
        {
            // Lagrer innstillinger til databasen..

            if (txtFornavn.Text != string.Empty &&
                txtEpost.Text != string.Empty &&
                txtEtternavn.Text != string.Empty &&
                txtBrukernavn.Text != string.Empty &&
                txtIM.Text != string.Empty)
            {

                string nyBrukernavn = txtBrukernavn.Text.Trim();

                // Han har ikke endret brukernavn, trenger ikke sjekke
                if (bruker.Brukernavn.Equals(nyBrukernavn))
                {
                    lagrePreferanser();
                }
                else 
                {
                    if (Queries.GetBrukerVedBrukernavn(nyBrukernavn).Brukernavn.Equals(nyBrukernavn))
                    {
                        Session["flashMelding"] = "\"" + nyBrukernavn + "\" brukes av en annen bruker";
                        Session["flashStatus"] = Konstanter.notifikasjonsTyper.danger.ToString();
                    }

                    else
                    {
                        lagrePreferanser();
                    }
                }
                
            }
            else
            {
                Session["flashMelding"] = "Et eller flere felt er ikke fylt ut";
                Session["flashStatus"] = Konstanter.notifikasjonsTyper.danger.ToString();
            }

            Response.Redirect(Request.Url.ToString());
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
            Response.Redirect(Request.Url.ToString());
                    
        }

        private void lagrePreferanser()
        {
            string nyFornavn = txtFornavn.Text;
            string nyEtternavn = txtEtternavn.Text;
            string nyEpost = txtEpost.Text.Trim();
            string nyBrukernavn = txtBrukernavn.Text.Trim();
            string nyIM = txtIM.Text.Trim();

            using (var context = new Context())
            {
                Bruker _bruker = context.Brukere.Where(b => b.Bruker_id == bruker_id).FirstOrDefault();

                _bruker.Fornavn = nyFornavn;
                _bruker.Etternavn = nyEtternavn;
                _bruker.Epost = nyEpost;
                _bruker.Brukernavn = nyBrukernavn;
                _bruker.IM = nyIM;

                context.SaveChanges();

            }

            Session["flashMelding"] = "Innstillinger lagret";
            Session["flashStatus"] = Konstanter.notifikasjonsTyper.success.ToString();

            selectedItems = new bool[6];

                for (int i = 0; i < cblElementer.Items.Count; i++)
                {
                    selectedItems[i] = cblElementer.Items[i].Selected;
                }

                selectedItems[5] = chkShepherd.Checked;

            var nyBrukerpreferanser = new BrukerPreferanse
            {
                Bruker_id = bruker_id,
                EpostTeam = selectedItems[0],
                EpostProsjekt = selectedItems[1],
                EpostOppgave = selectedItems[2],
                EpostKommentar = selectedItems[3],
                EpostTidsfrist = selectedItems[4],
                Sheperd = chkShepherd.Checked
            };


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
                    brukerPref.Sheperd = selectedItems[5];
                }
                else
                {
                    db.BrukerPreferanser.Add(nyBrukerpreferanser);
                }

                db.SaveChanges();
            }
        }


    }
}