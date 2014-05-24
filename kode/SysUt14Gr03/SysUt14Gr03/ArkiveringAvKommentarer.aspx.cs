using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SysUt14Gr03.Classes;
using SysUt14Gr03.Models;

namespace SysUt14Gr03
{
    /// <summary>
    /// Denne klassen tar seg av aktivering av kommentarer. man kan bare kommentere dem man selv har laget. 
    /// </summary>
    public partial class ArkiveringAvKommentarer : System.Web.UI.Page
    {
        private List<Kommentar> kommentarList;
        private int bruker_id;
        private int komm_id;
        private int oppg_id;

        protected void Page_PreInit(Object sener, EventArgs e)
        {
            string master = SessionSjekk.findMaster();
            this.MasterPageFile = master;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                bruker_id = Validator.KonverterTilTall(Session["bruker_id"].ToString());
                kommentarList = Queries.GetAlleKommentarTilBruker(bruker_id);

                for (int i = 0; i < kommentarList.Count; i++)
                {
                    Kommentar kom = kommentarList[i];
                    lbKommentarer.Items.Add(new ListItem(kom.Tekst, kom.Kommentar_id.ToString()));
                   
                }
                using (var context = new Context())
                {
                    Bruker bruker = context.Brukere.Where(b => b.Bruker_id == bruker_id).First();
                    Session["flashMelding"] = "Logget inn som " + bruker.ToString();
                    Session["flashStatus"] = Konstanter.notifikasjonsTyper.success;
                }
               
            }
        }
       
        protected void btnSlett_Click(object sender, EventArgs e)
        {
            using (var context = new Context())
            {
                if(lbKommentarer.SelectedValue == String.Empty)
                {
                    Session["flashMelding"] = "Du må velge hvilen kommentar som skal slettes!";
                    Session["flashStatus"] = Konstanter.notifikasjonsTyper.danger;
                }
                else
                { 
                komm_id = Convert.ToInt32(lbKommentarer.SelectedValue);
                var kommentar = (from k in context.Kommentarer
                           where k.Kommentar_id == komm_id
                           select k).FirstOrDefault();
                kommentar.Aktiv = false;
                context.SaveChanges();
              
                Response.Redirect("ArkiveringAvKommentarer.aspx");
                }
               
            }
        }

        protected void lbKommentarer_SelectedIndexChanged(object sender, EventArgs e)
        {

            using (var context = new Context())
            {
                int com_id = Convert.ToInt32(lbKommentarer.SelectedValue);
                Kommentar kom = context.Kommentarer.Where(k => k.Kommentar_id == com_id).First();
                oppg_id = kom.Oppgave_id;
                Oppgave oppg = context.Oppgaver.Where(o => o.Oppgave_id == oppg_id).First();

                if (lbOppgave.Items.Contains(lbOppgave.Items.FindByValue(oppg_id.ToString()))){}
               
                else
                {
                    lbOppgave.Items.Add(new ListItem(oppg.Tittel, oppg.Oppgave_id.ToString()));
                }
            }
        }
    }
}
