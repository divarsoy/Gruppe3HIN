using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SysUt14Gr03.Models;
using SysUt14Gr03.Classes;
using System.Windows.Forms;
using System.Drawing;

namespace SysUt14Gr03
{
    /// <summary>
    /// Her velger prosjektleder oppgaver som skal linkes sammen med avhengighet.
    /// Brukeren ser en gridview med oppgaver i prosjektet, velger prioritet fra
    /// én til ti, og et navn på gruppen
    /// </summary>
    public partial class OpprettOppgavegruppe : System.Web.UI.Page
    {
        private int prosjekt_id;
        private int bruker_id;
        private List<Oppgave> oppgaveListe;
        private List<Oppgave> valgteOppgaver;
        private DropDownList ddlPrioritet;

        protected void Page_PreInit(Object sener, EventArgs e)
        {
            string master = SessionSjekk.findMaster();
            this.MasterPageFile = master;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            
            SessionSjekk.sjekkForRettighetPaaInnloggetBruker(Konstanter.rettighet.Prosjektleder);
            SessionSjekk.sjekkForProsjekt_id();
            bruker_id = Validator.KonverterTilTall(Session["bruker_id"].ToString());

            prosjekt_id = Validator.KonverterTilTall(Session["prosjekt_id"].ToString());
            Prosjekt prosjekt = Queries.GetProsjekt(prosjekt_id);

            // Sjekk om prosjektleder er prosjektleder for valgt prosjekt
            if (prosjekt.Bruker_id == bruker_id)
            {
                oppgaveListe = Queries.GetAlleAktiveOppgaverForProsjekt(prosjekt_id);
                valgteOppgaver = new List<Oppgave>();

                if (!IsPostBack)
                {
                    BindingSource bindingsource = new BindingSource();
                    bindingsource.DataSource = oppgaveListe;
                    gvwOppgaver.DataSource = bindingsource;
                    gvwOppgaver.DataBind();
                }
            }

        }

    

        protected void gvwOppgaver_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DropDownList ddlPrioritet = (e.Row.FindControl("ddlPrioritet") as DropDownList);
                // Fyller listen med tall
                for (int i = 1; i < 11; i++)
                {
                    ddlPrioritet.Items.Add(i.ToString());
                }
            }
        }

        protected void btnOpprett_Click(object sender, EventArgs e)
        {

            for (int i = 0; i < gvwOppgaver.Rows.Count; i++)
            {
                System.Web.UI.WebControls.CheckBox chkBox = (System.Web.UI.WebControls.CheckBox)gvwOppgaver.Rows[i].FindControl("chbGruppe");
                ddlPrioritet = gvwOppgaver.Rows[i].FindControl("ddlPrioritet") as DropDownList;
                if (chkBox != null)
                {
                    if (chkBox.Checked)
                    {
                        Oppgave oppgave = oppgaveListe[i];
                        oppgave.Prioritering_id = ddlPrioritet.SelectedIndex + 1; ;
                        valgteOppgaver.Add(oppgaveListe[i]);
                    }
                }
            }
            // Sjekker hvilke oppgaver som er valgt
            if (txtNavn.Text != string.Empty)
            {
                if (valgteOppgaver.Count < 10)
                {
                    if (valgteOppgaver.Count >= 2)
                    {
                        using (var context = new Context())
                        {
                            List<Oppgave> oppgaverTilDatabase = new List<Oppgave>();

                            var nyOppgaveGruppe = new OppgaveGruppe
                            {
                                Navn = txtNavn.Text
                            };

                            context.OppgaveGrupper.Add(nyOppgaveGruppe);
                            context.SaveChanges();

                            foreach (Oppgave oppgave in valgteOppgaver)
                            {

                                Oppgave op = context.Oppgaver.FirstOrDefault(o => o.Oppgave_id == oppgave.Oppgave_id);
                                op.OppgaveGruppe = nyOppgaveGruppe;
                                op.Prioritering = context.Prioriteringer.FirstOrDefault(p => p.Prioritering_id == oppgave.Prioritering_id);
                                context.SaveChanges();
                            }

                        }
                        Response.Redirect(Request.RawUrl);
                    }
                    else
                    {
                        Session["flashMelding"] = "Velg minst to oppgaver";
                        Session["flashStatus"] = Konstanter.notifikasjonsTyper.danger.ToString();
                    }

                }

                else
                {
                    Session["flashMelding"] = "Maks antall oppgaver i en gruppe er 10";
                    Session["flashStatus"] = Konstanter.notifikasjonsTyper.danger.ToString();
                }

            }
            else
            {
                Session["flashMelding"] = "Skriv inn et navn";
                Session["flashStatus"] = Konstanter.notifikasjonsTyper.danger.ToString();
            }
        }
    }
}