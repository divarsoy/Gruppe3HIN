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
        private List<Oppgave> oppgListe;
        private int brukerid;
        private Bruker innloggetbruker;
        protected void Page_Load(object sender, EventArgs e)
        {
            SessionSjekk.sjekkForBruker_id();
            brukerid = Validator.KonverterTilTall(Session["bruker_id"].ToString());
            innloggetbruker = Queries.GetBruker(brukerid);

            
            if (!IsPostBack)
            {
            

                brukerListe = Queries.GetAlleAktiveBrukere();
                oppgListe = Queries.GetAlleAktiveOppgaverForBruker(brukerid);

                for (int i = 0; i < oppgListe.Count; i++ )
                {
                    Oppgave oppg = oppgListe[i];
                    ddlOppgave.Items.Add(new ListItem(oppg.Tittel, oppg.Oppgave_id.ToString()));
                }

                    for (int i = 0; i < brukerListe.Count; i++)
                    {
                     
                        Bruker bruker = brukerListe[i];
                        ddlBrukere.Items.Add(new ListItem(bruker.ToString(), bruker.Bruker_id.ToString()));
                        ddlBrukere.Items.Remove(ddlBrukere.Items.FindByValue(brukerid.ToString()));

                    }
              
            }
        }
   
        protected void btnSendInvitasjon_Click(object sender, EventArgs e)
        {
            
            brukerid = Convert.ToInt32(ddlBrukere.SelectedValue);
            int oppgave_id = Convert.ToInt32(ddlOppgave.SelectedValue);
            Varsel.SendVarsel(brukerid, Varsel.OPPGAVEVARSEL, "Hjelp", innloggetbruker.ToString() + " trenger hjelp til oppgaven: ", oppgave_id, 1);
            lblInvitasjon.ForeColor = Color.Green;
            lblInvitasjon.Text = "Invitasjon sendt til: " + ddlBrukere.SelectedItem.ToString();
        }
    }
}