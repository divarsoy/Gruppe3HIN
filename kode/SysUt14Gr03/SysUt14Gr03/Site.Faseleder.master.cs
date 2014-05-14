using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace SysUt14Gr03
{
    public partial class Site_Faseleder : System.Web.UI.MasterPage
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            HtmlLink myHtmlLink = new HtmlLink();
            myHtmlLink.Href = "~/Content/faselederStylesheet.less";
            myHtmlLink.Attributes.Add("rel", "stylesheet");
            myHtmlLink.Attributes.Add("type", "text/css");

            Page.Header.Controls.Add(myHtmlLink);
        }
        protected void Page_Load(object sender, EventArgs e)
        {

        }
    }
}