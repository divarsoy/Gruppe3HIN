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
    public partial class ArkiveringAvKommentarer : System.Web.UI.Page
    {
        private List<Kommentar> kommentarList;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                kommentarList = Queries.GetAlleKommentarTilBruker(1);

                for (int i = 0; i < kommentarList.Count; i++)
                {
                    Kommentar kom = kommentarList[i];
                    lbKommentarer.Items.Add(kom.Tekst);
                }
            }
        }
    }
}