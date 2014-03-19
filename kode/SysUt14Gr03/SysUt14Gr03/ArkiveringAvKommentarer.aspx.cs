using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SysUt14Gr03.Classes;
using SysUt14Gr03.Models;

namespace SysUt14Gr03
{
    public partial class ArkiveringAvKommentarer : System.Web.UI.Page
    {
        private List<Kommentar> kommentarList;
        private int bruker_id = 1;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                kommentarList = Queries.GetAlleKommentarTilBruker(bruker_id);

                for (int i = 0; i < kommentarList.Count; i++)
                {
                    Kommentar kom = kommentarList[i];
                    lbKommentarer.Items.Add(new ListItem(kom.Tekst, kom.Kommentar_id.ToString()));
                }
                using (var context = new Context())
                {
                    Bruker bruker = context.Brukere.Where(b => b.Bruker_id == bruker_id).First();
                    lblBruker.ForeColor = Color.Green;
                    lblBruker.Text = "Logget inn som " + bruker.ToString();

                }
            }
        }

        protected void btnSlett_Click(object sender, EventArgs e)
        {
            using (var context = new Context())
            {
                int komm_id = Convert.ToInt32(lbKommentarer.SelectedValue);
                var kommentar = (from k in context.Kommentarer
                           where k.Kommentar_id == komm_id
                           select k).FirstOrDefault();
                kommentar.Aktiv = false;
                context.SaveChanges();
                Response.Redirect("ArkiveringAvKommentarer.aspx");
            }
        }
    }
}