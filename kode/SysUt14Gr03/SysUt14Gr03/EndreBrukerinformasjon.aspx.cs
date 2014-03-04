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
        private string password = "blahimmel";
        private int bruker_id;
        private MailMessage msg;

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
        private void sendBekreftelse(string epost)
        {


            string ActivationUrl = string.Empty;
            string email = string.Empty;
            email = epost;

            try
            {
                Guid token = Guid.NewGuid();

                msg = new MailMessage();
                SmtpClient smtp = new SmtpClient();

                //bruker gruppe eposten som avsender
                msg.From = new MailAddress("sysut14gr03@gmail.com");
                msg.To.Add(email);

                msg.Subject = "Bekreftelses epost for konto aktivering";

                //har begynt å lage en aktiverkonto side 
                ActivationUrl = Server.HtmlEncode("http://localhost:60154/AktiverKonto.aspx?Epost=" + email + "&Token=" + token);

                msg.Body = "Hei " + "kakskiv" + "!\n" + "Takk for at du registrerte deg hos oss\n" + " <a href='" + ActivationUrl + "'>Klikk her for å aktivere</a>  din konto.";
                msg.IsBodyHtml = true;
                smtp.Credentials = new NetworkCredential("sysut14gr03@gmail.com", password);
                smtp.Port = 587;
                smtp.Host = "smtp.gmail.com";
                smtp.EnableSsl = true;
                smtp.Send(msg);



                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('En link for å aktivere brukerkontoen er sendt til brukereposten');", true);

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Error occured : " + ex.Message.ToString() + "');", true);
                return;
            }
        }

        protected void gridViewEndre_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Send")
            {
                int index = Convert.ToInt32(e.CommandArgument.ToString());
                TextBox tbEmail = (TextBox)gridViewEndre.Rows[index].FindControl("tbEpost");
                sendBekreftelse(tbEmail.Text);
            }
        }
    }
}