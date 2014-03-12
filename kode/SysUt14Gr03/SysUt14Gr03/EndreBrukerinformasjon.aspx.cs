using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SysUt14Gr03.Classes;
using SysUt14Gr03.Models;

namespace SysUt14Gr03
{
    public partial class EndreBrukerinformasjon : System.Web.UI.Page
    {
   
        private int bruker_id;
        private MailMessage msg;
        private Classes.sendEmail sendMsg;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                visBrukere();
            }
        }

        private void visBrukere()
        {
            using (var context = new Context())
            {
                System.Windows.Forms.BindingSource bindingSource1 = new System.Windows.Forms.BindingSource();
                bindingSource1.DataSource = context.Brukere.ToList<Bruker>();
                gridViewEndre.DataSource = bindingSource1;
                gridViewEndre.DataBind();
            }
        }
        protected void gridViewEndre_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gridViewEndre.EditIndex = e.NewEditIndex;
            gridViewEndre.Columns[4].Visible = true;
            visBrukere();
        }

        protected void gridViewEndre_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gridViewEndre.EditIndex = -1;
            gridViewEndre.Columns[4].Visible = false;
            visBrukere();
        }

        protected void gridViewEndre_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            bruker_id = (int)gridViewEndre.DataKeys[e.RowIndex].Value;
            System.Web.UI.WebControls.TextBox tbEtternavn = (TextBox)gridViewEndre.Rows[e.RowIndex].FindControl("tbEtternavn");
            System.Web.UI.WebControls.TextBox tbFornavn = (TextBox)gridViewEndre.Rows[e.RowIndex].FindControl("tbFornavn");
            System.Web.UI.WebControls.TextBox tbEpost = (TextBox)gridViewEndre.Rows[e.RowIndex].FindControl("tbEpost");
            System.Web.UI.WebControls.CheckBox cbAktiv = (CheckBox)gridViewEndre.Rows[e.RowIndex].FindControl("cboxAktiv");

            using (var context = new Context())
            {
                Bruker bruker = context.Brukere.Where(b => b.Bruker_id == bruker_id).First();
                bruker.Etternavn = tbEtternavn.Text;
                bruker.Fornavn = tbFornavn.Text;
                bruker.Epost = tbEpost.Text;
                bruker.Aktiv = Convert.ToBoolean(cbAktiv.Checked);
                context.SaveChanges();
            }
            gridViewEndre.Columns[4].Visible = false;
            gridViewEndre.EditIndex = -1;
            visBrukere();
        }
        private void sendBekreftelse(string epost, string fornavn)
        {
            Guid token = Guid.NewGuid();
            msg.Subject = "Bekreftelses epost for konto aktivering";
            string ActivationUrl = Server.HtmlEncode("http://localhost:60154/AktiverKonto.aspx?Epost=" + epost + "&Token=" + token);
            msg.Body = "Hei " + fornavn + "!\n" + "Takk for at du registrerte deg hos oss\n" + " <a href='" + ActivationUrl + "'>Klikk her for å aktivere</a>  din konto.";

            sendMsg.sendEpost(epost, msg.Body, msg.Subject, ActivationUrl, null, null);
        }

        protected void gridViewEndre_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Send")
            {
                int index = Convert.ToInt32(e.CommandArgument.ToString());
                TextBox tbEmail = (TextBox)gridViewEndre.Rows[index].FindControl("tbEpost");
                TextBox tbFirstname = (TextBox)gridViewEndre.Rows[index].FindControl("tbFornavn");
                sendBekreftelse(tbEmail.Text, tbFirstname.Text);
            }
        }
    }
}