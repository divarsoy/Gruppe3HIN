using System;
using System.Collections.Generic;
using System.Drawing;
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
    public partial class InvitasjonAvBruker : System.Web.UI.Page
    {
        private List<Bruker> brukerListe;
        private int oppgaveid;
        private int brukerid;
        private Bruker innloggetbruker;

        protected void Page_PreInit(Object sener, EventArgs e)
        {
            string master = SessionSjekk.findMaster();
            this.MasterPageFile = master;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            SessionSjekk.sjekkForBruker_id();
            brukerid = Validator.KonverterTilTall(Session["bruker_id"].ToString());
            innloggetbruker = Queries.GetBruker(brukerid);

            
           

                if (Request.QueryString["oppgave_id"] != null)
                {
                    brukerListe = Queries.GetAlleAktiveBrukere();
                    oppgaveid = Validator.KonverterTilTall(Request.QueryString["oppgave_id"]);
                    Oppgave oppg = Queries.GetOppgave(oppgaveid);
                    lblInvitasjon.Text = "<h2>" + oppg.Tittel + "</h2>";
                    if (!IsPostBack)
                    {
                    for (int i = 0; i < brukerListe.Count; i++)
                    {

                        Bruker bruker = brukerListe[i];
                        ddlBrukere.Items.Add(new ListItem(bruker.ToString(), bruker.Bruker_id.ToString()));
                        ddlBrukere.Items.Remove(ddlBrukere.Items.FindByValue(brukerid.ToString()));

                    }
                }
              
            }
        }
   
        protected void btnSendInvitasjon_Click(object sender, EventArgs e)
        {
            
            brukerid = Convert.ToInt32(ddlBrukere.SelectedValue);
            int oppgave_id = Validator.KonverterTilTall(Session["bruker_id"].ToString());
            // Sorry Eivind
            // Varsel.SendVarsel(brukerid, Varsel.OPPGAVEVARSEL, "Hjelp", innloggetbruker.ToString() + " trenger hjelp til oppgaven: ", oppgaveid, 1);
            Varsel.SendInvitasjon(brukerid, innloggetbruker.Bruker_id, oppgave_id, innloggetbruker.ToString() + " trenger hjelp til oppgaven: ");
           // lblInvitasjon.ForeColor = Color.Green;
            Oppgave oppgave = Queries.GetOppgave(oppgaveid);
            //lblInvitasjon.Text = "Invitasjon sendt til: " + ddlBrukere.SelectedItem.ToString();
            Session["flashMelding"] = "Invitasjon til hjelp av oppgave " + oppgave.Tittel + " er sendt til " + ddlBrukere.SelectedItem.ToString();
            Session["flashStatus"] = Konstanter.notifikasjonsTyper.info.ToString();
            Response.Redirect(Request.RawUrl);
        }
    }
}