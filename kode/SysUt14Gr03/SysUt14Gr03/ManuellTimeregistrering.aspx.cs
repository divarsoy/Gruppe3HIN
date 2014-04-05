using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SysUt14Gr03.Models;
using SysUt14Gr03.Classes;

namespace SysUt14Gr03
{
    public partial class ManuellTimeregistrering : System.Web.UI.Page
    {
        private int pauseTeller = 0;
        List<Label> lblPauseStart = new List<Label>();
        List<Label> lblPauseSlutt = new List<Label>();
        List<TextBox> txtPauseStart = new List<TextBox>();
        List<TextBox> txtPauseSlutt = new List<TextBox>();

        protected void Page_Load(object sender, EventArgs e)
        {
            DateTime dato = DateTime.Now;
            ddlDag.Items.Add(new ListItem("I dag (" + dato.ToShortDateString() + ")", dato.ToShortDateString()));
            ddlDag.Items.Add(new ListItem("I går (" + dato.AddDays(-1).ToShortDateString() + ")", dato.AddDays(-1).ToShortDateString()));
            if (ViewState["paneltest"] != null)
            {
                pnlPauser = ViewState["paneltest"] as Panel;
            }

        }

        protected void btnAddPause_Click(object sender, EventArgs e)
        {
            // Legger til nytt felt for pauser
            // http://www.codeproject.com/Articles/35360/ViewState-in-Dynamic-Control

            Label lblPST = new Label();
            lblPST.Text = "Pause start";
            Label lblPSL = new Label();
            lblPSL.Text = "Pause slutt";
            lblPauseStart.Add(lblPST);
            lblPauseSlutt.Add(lblPSL);




            TextBox txtPST = new TextBox();
            TextBox txtPSL = new TextBox();
            txtPST.TextMode = TextBoxMode.Time;
            txtPSL.TextMode = TextBoxMode.Time;
            txtPST.ID = "txtPauseStart" + pauseTeller;
            txtPSL.ID = "txtPauseSlutt" + pauseTeller;
            txtPauseStart.Add(txtPST);
            txtPauseSlutt.Add(txtPSL);

            for (int i = 0; i < lblPauseSlutt.Count; i++)
            {
                pnlPauser.Controls.Add(lblPauseStart[pauseTeller]);
                pnlPauser.Controls.Add(txtPauseStart[pauseTeller]);
                pnlPauser.Controls.Add(lblPauseSlutt[pauseTeller]);
                pnlPauser.Controls.Add(txtPauseSlutt[pauseTeller]);
            }
            
            pauseTeller++;

            ViewState["paneltest"] = pnlPauser;

        }

        protected void btnLagre_Click(object sender, EventArgs e)
        {
            // Lagre timer på oppgave
        }

    }
}