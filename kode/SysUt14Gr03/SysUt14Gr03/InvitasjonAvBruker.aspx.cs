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

/// <summary>
/// klasse som sender ut invitasjon om hjelp til en oppgave, tar inn en oppgave_id for å vite 
/// hvilken oppgave det trengs hjelp til for så å sende ut ved hjelp av varsel klassen.
/// </summary>

namespace SysUt14Gr03
{
    public partial class InvitasjonAvBruker : System.Web.UI.Page
    {
        private List<Bruker> brukerListe; //liste med brukere
        private int oppgaveid; //Oppgave id til oppgaven
        private int brukerid; //brukerid til innlogget bruker
        private Bruker innloggetbruker; //bruker objekt til innlogget bruker
        
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
            Varsel.SendInvitasjon(brukerid, innloggetbruker.Bruker_id, oppgave_id, innloggetbruker.ToString() + " trenger hjelp til oppgaven: ");
            Oppgave oppgave = Queries.GetOppgave(oppgaveid);
            Session["flashMelding"] = "Invitasjon til hjelp av oppgave " + oppgave.Tittel + " er sendt til " + ddlBrukere.SelectedItem.ToString();
            Session["flashStatus"] = Konstanter.notifikasjonsTyper.info.ToString();
            Response.Redirect(Request.RawUrl);
        }
    }
}