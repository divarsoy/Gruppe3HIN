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
    public partial class EndreBrukerinformasjonSomAdministrator : System.Web.UI.Page
    {
        private int bruker_id;
        private MailMessage msg;
        private sendEmail sendMsg;
        private bool aktiv;

        protected void Page_Load(object sender, EventArgs e)
        {
            SessionSjekk.sjekkForRettighetPaaInnloggetBruker(Konstanter.rettighet.Administrator);
            msg = new MailMessage();
            sendMsg = new sendEmail();
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
                bindingSource1.DataSource = context.Brukere.Include("Rettigheter").ToList<Bruker>();
                gridViewEndre.DataSource = bindingSource1;
                gridViewEndre.DataBind();
            }
        }
        protected void gridViewEndre_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gridViewEndre.EditIndex = e.NewEditIndex;
            gridViewEndre.RowDataBound -= new GridViewRowEventHandler(gridViewEndre_RowDataBound);
            gridViewEndre.RowDataBound += new GridViewRowEventHandler(gridViewEndre_EditRowDataBound);
            gridViewEndre.Columns[6].Visible = true;
            visBrukere();
        }

        protected void gridViewEndre_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gridViewEndre.EditIndex = -1;
            gridViewEndre.Columns[6].Visible = false;
            visBrukere();
        }

        protected void gridViewEndre_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            bruker_id = (int)gridViewEndre.DataKeys[e.RowIndex].Value;
            System.Web.UI.WebControls.TextBox tbEtternavn = (TextBox)gridViewEndre.Rows[e.RowIndex].FindControl("tbEtternavn");
            System.Web.UI.WebControls.TextBox tbFornavn = (TextBox)gridViewEndre.Rows[e.RowIndex].FindControl("tbFornavn");
            System.Web.UI.WebControls.TextBox tbEpost = (TextBox)gridViewEndre.Rows[e.RowIndex].FindControl("tbEpost");
            System.Web.UI.WebControls.CheckBox cbAktiv = (CheckBox)gridViewEndre.Rows[e.RowIndex].FindControl("cboxAktiv");
            System.Web.UI.WebControls.DropDownList ddlRettighet = (DropDownList)gridViewEndre.Rows[e.RowIndex].FindControl("ddlRettighet");

            using (var context = new Context())
            {
                int rettighet_id = Validator.KonverterTilTall(ddlRettighet.SelectedValue);
                List<Rettighet> rettighetListe = new List<Rettighet>();
                var rettighet = context.Rettigheter.Where(r => r.Rettighet_id == rettighet_id).First();
                rettighetListe.Add(rettighet);

                Bruker bruker = context.Brukere
                                .Include("Rettigheter")
                                .Where(b => b.Bruker_id == bruker_id)
                                .First();
                bruker.Etternavn = tbEtternavn.Text;
                bruker.Fornavn = tbFornavn.Text;
                bruker.Epost = tbEpost.Text;
                bruker.Rettigheter = rettighetListe;
                bruker.Aktiv = Convert.ToBoolean(cbAktiv.Checked);
                context.SaveChanges();
               
                aktiv = bruker.Aktiv;
                string hendelseEndring = "Bruker " + bruker.Bruker_id + " er blitt endret: Fornavn: " + bruker.Fornavn +
                    " Etternavn: " + bruker.Etternavn + " epost: " + bruker.Epost + " Rettigheter: " + rettighet.RettighetNavn;

                OppretteLogg.opprettLoggForBruker(hendelseEndring, DateTime.Now, bruker.Bruker_id);
                OppretteLogg.opprettLoggForBruker(hendelseEndring, DateTime.Now, (int)Session["bruker_id"]);

                if (!aktiv)
                {
                    OppretteLogg.opprettLoggForBruker("Bruker " + bruker.Bruker_id + " er arkivert",
                    DateTime.Now, bruker.Bruker_id);
                }
            }
            gridViewEndre.Columns[6].Visible = false;
            gridViewEndre.EditIndex = -1;
            visBrukere();

            
        }
        private void sendBekreftelse(string epost, string fornavn)
        {
            Guid token = Guid.NewGuid();
            msg.Subject = "Bekreftelses epost for konto aktivering";
            string ActivationUrl = Server.HtmlEncode("~/AktiverKonto.aspx?Epost=" + epost + "&Token=" + token);
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
        protected void gridViewEndre_EditRowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow && gridViewEndre.EditIndex == e.Row.RowIndex)
            {
                string lsDataKeyValue = gridViewEndre.DataKeys[e.Row.RowIndex].Values[0].ToString();
                bruker_id = Convert.ToInt32(lsDataKeyValue);
                Bruker bruker = Queries.GetBruker(bruker_id);          

                List<Rettighet> rettighet = Queries.GetAlleRettigheter();
                DropDownList ddlRettighet = e.Row.FindControl("ddlRettighet") as DropDownList;
                foreach (Rettighet rett in rettighet)
                {
                    ddlRettighet.Items.Add(new ListItem(rett.RettighetNavn, rett.Rettighet_id.ToString()));
                }
                int id = Queries.GetRettighet(bruker_id).Rettighet_id;
                ddlRettighet.SelectedIndex = id - 1;
                }
        }
        protected void gridViewEndre_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                using (var context = new Context())
                {
                    string lsDataKeyValue = gridViewEndre.DataKeys[e.Row.RowIndex].Values[0].ToString();
                    bruker_id = Convert.ToInt32(lsDataKeyValue);
                    Bruker bruker = context.Brukere.Where(b => b.Bruker_id == bruker_id).FirstOrDefault();

                    foreach (Rettighet rett in bruker.Rettigheter)
                    {
                        Label lblRett = e.Row.FindControl("lblRettighet") as Label;
                        lblRett.Text = rett.RettighetNavn;
                    }
                    HyperLink EtternavnLink = e.Row.FindControl("EtternavnLink") as HyperLink;
                    Label lblEtternavn = e.Row.FindControl("lblEtternavn") as Label;
                    lblEtternavn.Visible = false;
                    EtternavnLink.Text = bruker.Etternavn;
                    EtternavnLink.NavigateUrl = ResolveUrl("~/visBruker?bruker_id=" + bruker_id);
                    
                    HyperLink FornavnLink = e.Row.FindControl("FornavnLink") as HyperLink;
                    Label lblFornavn = e.Row.FindControl("lblFornavn") as Label;
                    lblFornavn.Visible = false;
                    FornavnLink.Text = bruker.Fornavn;
                    FornavnLink.NavigateUrl = ResolveUrl("~/visBruker?bruker_id=" + bruker_id);
                }
            }
        }
    }
}