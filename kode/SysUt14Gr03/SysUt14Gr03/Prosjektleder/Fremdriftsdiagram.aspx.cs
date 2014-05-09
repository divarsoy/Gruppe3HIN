using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.DataVisualization.Charting;
using SysUt14Gr03.Classes;
using SysUt14Gr03.Models;

namespace SysUt14Gr03
{
    public partial class Fremdriftsdiagram : System.Web.UI.Page
    {
        private int faseID;
        private int prosjektID;

        private TimeSpan estimat;
        private TimeSpan bruktTid;
        private TimeSpan tidIgjen;
        private TimeSpan resterendeTidUtenTillegg;
        private TimeSpan resterendeTid;
        private TimeSpan resterendeTidPaaOppgave;
        private TimeSpan estimatForOppgave;
//        private TimeSpan nullTimeSpan = new TimeSpan(0);
        private DateTime startDato;
        private DateTime sluttDato;

        private Fase fase;
        private List<Fase> faser;
        private List<Oppgave> oppgaver;
        private List<Time> registrerteTimer;
        private List<float> yVerdier = new List<float>();
        private List<DateTime> xVerdier = new List<DateTime>();

        protected void Page_Load(object sender, EventArgs e)
        {
            //SessionSjekk.sjekkForProsjekt_id();
            //SessionSjekk.sjekkForBruker_id();

            if (!IsPostBack)
            {
                resterendeTid = new TimeSpan();
                tidIgjen = new TimeSpan();
                estimat = new TimeSpan();
                bruktTid = new TimeSpan();

                prosjektID = 1;// Validator.KonverterTilTall(Session["prosjekt_id"].ToString());
                faser = Queries.GetFaseForProsjekt(prosjektID);

                for (int i = 0; i < faser.Count; i++)
                    ddlFaser.Items.Add(new ListItem(faser[i].Navn, faser[i].Fase_id.ToString()));

                this.fillGraph();
            }
        }

        protected void ddlFaser_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.fillGraph();
        }
        private void fillGraph()
        {
            /*for (int i = 0; i < oppgaver.Count; i++)
            {
                tidPaaOppgaver.Add(Validator.KonverterTilTall(oppgaver[i].Estimat.ToString()));
                List<Time> t = Queries.GetTimerForOppgave(oppgaver[i].Oppgave_id);
                foreach (Time time in t)
                {
                    registrerteTimer.Add(time);
                }

            } */

            // Diagramm egenskaper
            this.ChartHolder.ChartAreas["ChartArea1"].AxisX.Interval = 1; // makes sure each point has a label
            this.ChartHolder.Series["Brukte tid"].BorderWidth = 3; // changes the width of the burn-up-line
            this.ChartHolder.Series["Estimert tid"].BorderWidth = 3; // changes the width of the burn-up-ceiling
            this.ChartHolder.Series["Estimert tid"].Color = System.Drawing.Color.Red; // changes the color of the burn-up-ceiling to red
            this.ChartHolder.Series["Brukte tid"].YValuesPerPoint = 2;
            this.ChartHolder.ChartAreas["ChartArea1"].AxisX.LabelStyle.Angle = -45;
            this.ChartHolder.ChartAreas["ChartArea1"].Area3DStyle.Enable3D = false;
            this.ChartHolder.Legends.Add(new Legend("Legend"));
            this.ChartHolder.Legends["Legend"].Enabled = true;

            faseID = Convert.ToInt32(ddlFaser.SelectedValue);
            fase = Queries.GetFase(faseID);
            
            startDato = Convert.ToDateTime(fase.Start);
            sluttDato = Convert.ToDateTime(fase.Stopp);
            
            oppgaver = Queries.getOppgaverIFase(faseID);
            for (int i = 0; i < oppgaver.Count; i++)
            {
                estimat += (TimeSpan)oppgaver[i].Estimat;
                bruktTid += (TimeSpan)oppgaver[i].BruktTid;
<<<<<<< HEAD
            } 
=======
            }
>>>>>>> 7f7242721fbe663ffbb2eb44c24a9c199fd703e8

            List<DateTime> xRange = Enumerable.Range(0, (sluttDato - startDato).Days + 1)
                .Select(i => startDato.AddDays(i))
                .ToList();
            
<<<<<<< HEAD
            this.ChartHolder.Series["Brukte tid"].Points.AddXY(0, estimat.TotalHours);
            resterendeTid = estimat;
            foreach (var d in range)
            {
                for (int i = 0; i < oppgaver.Count; i++)
                {

                    estimatForOppgave = (TimeSpan)oppgaver[i].Estimat;
                    resterendeTidPaaOppgave = estimatForOppgave;
=======
            List<Double> yRange = new List<Double>();
            //yRange.Add(estimat.TotalHours);
            //this.ChartHolder.Series["Brukte tid"].Points.AddXY(0, estimat.TotalHours);
            registrertTid = estimat;
            TimeSpan h = new TimeSpan();
            foreach (var d in xRange)
            {
                for (int i = 0; i < oppgaver.Count; i++)
                {
>>>>>>> 7f7242721fbe663ffbb2eb44c24a9c199fd703e8
                    registrerteTimer = Queries.GetTimerForOppgave(oppgaver[i].Oppgave_id);

                    for (int j = 0; j < registrerteTimer.Count; j++)
                    {

                        /*if (d.Date == registrerteTimer[j].Start.Value.Date && registrerteTimer[j]. oppgaver[i].RemainingTime)
                        {
                            registrertTid = registrertTid + ((TimeSpan)oppgaver[i].BruktTid - (TimeSpan)oppgaver[i].Estimat);
                            ChartHolder.Series["Brukte tid"].Points.AddXY(d.Date, registrertTid.TotalHours);
                        }
                        else */if (d.Date == registrerteTimer[j].Start.Value.Date)
                        {
<<<<<<< HEAD
                            resterendeTid = resterendeTid - registrerteTimer[j].Tid;
=======
                            registrertTid = registrertTid - registrerteTimer[j].Tid;
                            yRange.Add(registrertTid.TotalHours);
>>>>>>> 7f7242721fbe663ffbb2eb44c24a9c199fd703e8
                        }
                    }

//                    resterendeTidUtenTillegg = resterendeTidUtenTillegg + resterendeTidPaaOppgave;

                    if (resterendeTidPaaOppgave.TotalHours < 0)
                    {
                        resterendeTid = resterendeTidUtenTillegg - resterendeTidPaaOppgave;
                    }
                }
<<<<<<< HEAD
//                yVerdier.Add((float)resterendeTidUtenTillegg.TotalHours);
//                yVerdier.Add((float)(resterendeTid.TotalHours));
//                xVerdier.Add(d);
//                xVerdier.Add(d); 
                this.ChartHolder.Series["Brukte tid"].Points.AddXY(d.DayOfWeek + " " + d.ToShortDateString(), resterendeTid.TotalHours);
            }

            this.ChartHolder.Series["Brukte tid"].Points.DataBindXY(xVerdier, yVerdier);
=======
                //this.ChartHolder.Series["Brukte tid"].Points.AddXY(d.DayOfWeek + " " + d.ToShortDateString(), registrertTid.TotalHours);
            }
            ChartHolder.Series["Brukte tid"].Points.DataBindXY(xRange, yRange);
>>>>>>> 7f7242721fbe663ffbb2eb44c24a9c199fd703e8
            /*
            //tidIgjen = estimat;
            TimeSpan h = new TimeSpan();
            foreach (var d in range)
            {
                
                for (int i = 0; i < oppgaver.Count; i++)
                {
                     yVerdier[i, 0] = Validator.KonverterTilTall(oppgaver[i].BruktTid.ToString());
                     if (i > 0)
                         yVerdier[i, 0] = yVerdier[i - 1, 0] + yVerdier[i, 0]; 

                    if ((TimeSpan)oppgaver[i].BruktTid > (TimeSpan)oppgaver[i].Estimat)
                    {
                        h = tidIgjen + ((TimeSpan)oppgaver[i].BruktTid - (TimeSpan)oppgaver[i].Estimat);
                    }

                    if (d.Date == oppgaver[i].Startet.Value.Date && oppgaver[i].Status_id == 3)
                    {

                        tidIgjen = tidIgjen - (TimeSpan)oppgaver[i].Estimat;
                    }
                    else if (d.Date == oppgaver[i].Startet.Value.Date)
                    {
                        tidIgjen = tidIgjen - (TimeSpan)oppgaver[i].BruktTid;
                    }
                }
             if (h != null)
                {
                    Object[] yvalues = new Object[2];
                    yvalues[0] = h.TotalHours;
                    yvalues[1] = tidIgjen.TotalHours;
                    this.ChartHolder.Series["Brukte tid"].Points.AddXY(d.DayOfWeek + " " + d.ToShortDateString(), yvalues);
                }
                else 
                    this.ChartHolder.Series["Estimert tid"]. 

                this.ChartHolder.Series["Brukte tid"].Points.AddXY(d.DayOfWeek + " " + d.ToShortDateString(), tidIgjen.TotalHours);
                this.ChartHolder.Series["Brukte tid"].Points.AddXY(d.DayOfWeek + " " + d.ToShortDateString(), tidIgjen.TotalHours);

                this.ChartHolder.Series["Estimert tid"].Points.AddXY(d.DayOfWeek + " " + d.ToShortDateString(), estimat.TotalHours);
            }*/
        }
    }
}
