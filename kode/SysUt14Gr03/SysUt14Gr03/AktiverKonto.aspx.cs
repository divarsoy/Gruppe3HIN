using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SysUt14Gr03
{
    public partial class AktiverKonto : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                ActivateMyAccount();
            }
        }

        private void ActivateMyAccount()
        {

            try
            {


                if ((!string.IsNullOrEmpty(Request.QueryString["Epost"])) & (!string.IsNullOrEmpty(Request.QueryString["Token"])))
                {
                    Response.Write("<h2 align=center>Kontoen din er aktivert. Du kan <a href='Login.aspx'>Logge deg Inn</a> nå!</h2> ");
                }
                else
                {
                    Response.Write("<h2 align=center>Kontoen din ble ikke aktivert!</h2>");
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Error occured : " + ex.Message.ToString() + "');", true);
                return;
            }


        }
    }
}
