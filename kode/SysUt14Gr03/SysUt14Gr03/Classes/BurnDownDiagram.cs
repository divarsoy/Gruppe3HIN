using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SysUt14Gr03.Classes;
using SysUt14Gr03.Models;
using System.Web.UI.DataVisualization.Charting;

namespace SysUt14Gr03.Classes
{
    public class BurnDownDiagram
    {
        private static TimeSpan estimatForFase;
        private static TimeSpan estimatForOppgave;
        private static TimeSpan resterendeTidForFase;
        private static TimeSpan resterendeTidForFaseMedDagensTillegg;
        private static TimeSpan resterendeTidForOppgave;
        private static TimeSpan bruktTidForOppgave;
        private static TimeSpan tillegsTidForFase;
        private static TimeSpan bruktTid;
        private static TimeSpan nullTimeSpan = new TimeSpan(0);
        private static TimeSpan tillegsTidForDato;

        private static List<float> yVerdier = new List<float>();
        private static List<float> yVerdierTotal = new List<float>();
        private static List<DateTime> xVerdier = new List<DateTime>();

        public static Chart getChartForFase(int fase_id)
        {
            Fase fase = Queries.GetFase(fase_id);
            DateTime sluttDato = fase.Stopp;
            DateTime startDato = fase.Start;
            List<Oppgave> oppgaverForFase = Queries.getOppgaverIFase(fase_id);
            List<TimeSpan> bruktTidForOppgaver = new List<TimeSpan>();
      
            Chart chart = new Chart();
            chart.BackColor = System.Drawing.Color.LightGray;
            chart.Width = 800;
            chart.Height = 350;
            chart.Titles.Add("Burndowndiagram for fase " + fase.Navn);

            ChartArea area = new ChartArea();
            area.Name = "BurnDownArea";

            chart.ChartAreas.Add(area);
            chart.Series.Add(new Series("Burndown"));
            chart.Series.Add(new Series("Beregnet Totaltid (fase)"));
            chart.Series["Burndown"].ChartType = SeriesChartType.Line;
            chart.Series["Burndown"].Color = System.Drawing.ColorTranslator.FromHtml("#FF0000FF");
            chart.Series["Beregnet Totaltid (fase)"].ChartType = SeriesChartType.Line;
            chart.Series["Beregnet Totaltid (fase)"].Color = System.Drawing.ColorTranslator.FromHtml("#FFFF0000");

       //     chart.Series["Burndown"].Points.AddXY(5, 5);
        //    chart.Series["Burndown"].Points.AddXY(10, 10);

            chart.Legends.Add(new Legend("Legend"));
            chart.Legends["Legend"].Enabled = true;

            List<DateTime> range = Enumerable.Range(0, (sluttDato - startDato).Days + 1)
                .Select(i => startDato.AddDays(i))
                .ToList();

            for (int i = 0; i < oppgaverForFase.Count; i++)
            {
                estimatForFase += (TimeSpan)oppgaverForFase[i].Estimat;
                bruktTidForOppgaver.Add(new TimeSpan(0));
            }

                foreach (DateTime d in range)
                {
                    resterendeTidForFase = new TimeSpan();
                    tillegsTidForFase = new TimeSpan();
 //                   estimatForFase = new TimeSpan();
                    tillegsTidForDato = new TimeSpan();

                    //     resterendeTidForFase = estimatForFase;
                    for (int i = 0; i < oppgaverForFase.Count; i++)
                    {
                        estimatForOppgave = (TimeSpan)oppgaverForFase[i].Estimat;
    //                    estimatForFase += estimatForOppgave;
                        resterendeTidForOppgave = estimatForOppgave;
                        List<Time> registrerteTimerPaaOppgave = Queries.GetTimerForOppgave(oppgaverForFase[i].Oppgave_id);

                        for (int j = 0; j < registrerteTimerPaaOppgave.Count; j++)
                        {
                            if (registrerteTimerPaaOppgave[j].Stopp != null)
                            {
                                DateTime stopDatoForTimeregistrering = (DateTime)registrerteTimerPaaOppgave[j].Stopp;
                              
                                if (stopDatoForTimeregistrering.Date == d.Date) {
                                    bruktTidForOppgaver[i] = bruktTidForOppgaver[i] + registrerteTimerPaaOppgave[j].Tid;
                                    bruktTid = bruktTid + registrerteTimerPaaOppgave[j].Tid;
                                }
                            }

                            //  TimeSpan ex = resterendeTidForOppgave;


                        }

//                        resterendeTidForFase = resterendeTidForFase + resterendeTidForOppgave;

                        TimeSpan ex = resterendeTidForOppgave;
                        if (bruktTidForOppgaver[i] > estimatForOppgave)
                        {
 //                           tillegsTidForFase -= resterendeTidForOppgaver[i];
                            tillegsTidForDato = bruktTidForOppgaver[i] - estimatForOppgave;

                            bruktTidForOppgaver[i] = estimatForOppgave;
                        }
                        //                   resterendeTidForFase = estimatForFase - bruktTid + tillegsTidForFase;
                        //resterendeTidForFase = resterendeTidForFase - bruktTid;

                    }
                    resterendeTidForFase = estimatForFase - bruktTid;

                    resterendeTidForFaseMedDagensTillegg = resterendeTidForFase + tillegsTidForDato;

                    yVerdier.Add((float)resterendeTidForFase.TotalHours);
                    yVerdier.Add((float)resterendeTidForFaseMedDagensTillegg.TotalHours);
                    xVerdier.Add(d);
                    xVerdier.Add(d);
                    yVerdierTotal.Add((float)estimatForFase.TotalHours);

                    estimatForFase += tillegsTidForDato;

                    yVerdierTotal.Add((float)estimatForFase.TotalHours);
                }

//            List<float> ex1 = yVerdier;
//            List<DateTime> ex2 = xVerdier;

            chart.Series["Burndown"].Points.DataBindXY(xVerdier, yVerdier);
            chart.Series["Beregnet Totaltid (fase)"].Points.DataBindXY(xVerdier, yVerdierTotal);
//            chart.Visible = true;
            return chart;
           // chart.Series.Add();
        }
    }
}