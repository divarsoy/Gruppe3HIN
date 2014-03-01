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
    public partial class RegistreringAvBrukere : System.Web.UI.Page
    {
        private string etternavn;
        private string fornavn;
        private string epost;
        private Bruker bruker;
        private bool emailUnq = true;

        protected void Page_Load(object sender, EventArgs e)
        {
   
        }

        protected void opprettBruker(string _etternavn, string _fornavn, string _epost)
        {
            using (var db = new Context())
            {
                var nyBruker = new Bruker { Etternavn = etternavn, Fornavn = fornavn, Epost = epost };
                db.Brukere.Add(nyBruker);
                db.SaveChanges();
            }
        }

        protected void bt_adm_reg_Click(object sender, EventArgs e)
        {
            if (tb_reg_etternavn.Text.Length < 256)
                etternavn = tb_reg_etternavn.Text;
            else
                FeilmeldingEtternavn.Visible = true;
            if (tb_reg_fornavn.Text.Length < 256)
                fornavn = tb_reg_fornavn.Text;
            else
                FeilMeldingFornavn.Visible = true;
                
            for (int i = 0; i < Queries.GetAlleAktiveBrukere().Count; i++)
            {
                bruker = Queries.GetBruker(i);
                if (bruker.Epost == tb_reg_epost.Text)
                emailUnq = false;
            }

            if (emailUnq && tb_reg_epost.Text.Length < 256)
                epost = tb_reg_epost.Text;
            else
                FeilMeldingEpost.Visible = true;
        }



    }
}