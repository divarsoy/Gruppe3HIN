using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SysUt14Gr03.Classes;
using SysUt14Gr03;

namespace SysUt14Gr03
{
    public partial class OversiktOppgaver : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            System.Windows.Forms.BindingSource bindingSource1 = new System.Windows.Forms.BindingSource();
            bindingSource1.DataSource = Queries.GetAlleAktiveOppgaverDag();
            ListView1.DataSource = bindingSource1;
            ListView1.DataBind();
            //ListView1.ItemType = Models.Oppgave;
        }


        //Default loader inn innlogget brukers oppgaver som er påbegynt eller som er klare
    }
}