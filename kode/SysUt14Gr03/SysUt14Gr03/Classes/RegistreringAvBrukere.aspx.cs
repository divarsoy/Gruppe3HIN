using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SysUt14Gr03.Classes
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        private string etternavn;
        private string fornavn;
        private string epost;

        protected void Page_Load(object sender, EventArgs e)
        {
   
        }

        protected void opprettBruker(string _etternavn, string _fornavn, string _epost)
        {
            using (var db = new Context())
            {
                var nyBruker = new Bruker { Etternavn = etternavn, Fornavn = fornavn, Epost = epost };
                db.Bruker.Add(nyBruker);
                db.SaveChanges();
            }
        }

        protected void bt_adm_reg_Click(object sender, EventArgs e)
        {
            etternavn = tb_reg_etternavn.Text;
            fornavn = tb_reg_fornavn.Text;
            epost = tb_reg_epost.Text;
        }


    }
}