using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SysUt14Gr03.Classes;
using SysUt14Gr03.Models;

namespace SysUt14Gr03
{
    public partial class OpprettTidsfristPaaOppgave : System.Web.UI.Page
    {
        private List<Oppgave> oppgaveListe;
        private int index;

        protected void Page_Load(object sender, EventArgs e)
        {

            oppgaveListe = Queries.GetAlleAktiveOppgaver();
            //oppgaveListe = new List<Oppgave>();
           // oppgaveListe.Add(new Oppgave( { Title = "Oppgave 1" }));
            //Oppgave test1 = new Oppgave();
            //Oppgave test2 = new Oppgave();
           // test1.Tittel = "Oppgave 1";
           // test1.Krav = "Skriv 800 sider om scrum";
            //test2.Tittel = "Oppgave 2";
            //test2.Krav = "Skriv 500 sider om planning poker";
            //oppgaveListe.Add(test1);
            //oppgaveListe.Add(test2);

            if (lsbOppgaver.Items.Count == 0)
            {
                for (int i = 0; i < oppgaveListe.Count(); i++)
                {
                    Oppgave oppgave = oppgaveListe[i];
                    lsbOppgaver.Items.Add(new ListItem(oppgave.Tittel, oppgave.Oppgave_id.ToString()));
                }
            }

        }

        protected void btnFrist_Click(object sender, EventArgs e)
        {
            Feilmelding.Visible = false;
            index = lsbOppgaver.SelectedIndex;
            // Vis en kalender for å velge dato/tid
            DateTime dato = calCalendar.SelectedDate;
            if (dato != DateTime.MinValue)
            {
                //dato.Hour = Convert.ToInt32(txtTime.Text);
                oppgaveListe[index].Tidsfrist = dato;
                int time = Convert.ToInt32(ddlTime.SelectedItem.ToString());
                int minutt = Convert.ToInt32(ddlMinutt.SelectedItem.ToString());
                TimeSpan timespan = new TimeSpan(time, minutt, 0);
                dato = dato.Add(timespan);
                

                using (var context = new Context())
                {
                    int oppgave_id = Convert.ToInt32(lsbOppgaver.SelectedValue);

                    Oppgave oppgave = context.Oppgaver.FirstOrDefault(o => o.Oppgave_id == oppgave_id);

                    oppgave.Tidsfrist = dato;
                    context.SaveChanges();

                    FristOK.Text = "Frist satt til " + dato.ToString() + " på " + oppgaveListe[index].Tittel;
                    FristOK.Visible = true;

                }
            }
            else
            {
                Feilmelding.Text = "Velg en dato";
                Feilmelding.Visible = true;
            }
            

        }

        protected void lsbOppgaver_SelectedIndexChanged(object sender, EventArgs e)
        {
            Feilmelding.Visible = false;
            // Vis valgt oppgave på siden
            index = lsbOppgaver.SelectedIndex;
            
            pnlDato.Visible = true;
            if (ddlTime.Items.Count == 0)
            {
                for (int i = 0; i < 24; i++)
                {
                    if (i < 10)
                        ddlTime.Items.Add("0" + i);
                    else
                        ddlTime.Items.Add("" + i);
                }

                for (int i = 0; i < 60; i += 5)
                {
                    if (i < 10)
                        ddlMinutt.Items.Add("0" + i);
                    else
                        ddlMinutt.Items.Add("" + i);
                }
            }

        }

        protected void btnDetaljer_Click(object sender, EventArgs e)
        {
            Oppgave oppgave = oppgaveListe[index];
            txtInfo.Text = oppgave.Tittel + "\n" + oppgave.Krav + "\n" + oppgave.Tidsfrist;
            txtInfo.Visible = true;
        }
    }
}