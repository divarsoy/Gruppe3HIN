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


        public static Chart getChartForFase(int fase_id)
        {
            TimeSpan estimatForFase = new TimeSpan(0);
            TimeSpan estimatForOppgave;
            TimeSpan resterendeTidForFase;
            TimeSpan resterendeTidForFaseMedDagensTillegg;
            TimeSpan resterendeTidForOppgave;
            TimeSpan bruktTid = new TimeSpan(0);
            TimeSpan nullTimeSpan = new TimeSpan(0);
            TimeSpan tillegsTidForDato;
            DateTime ferdigstiltDato;
            TimeSpan fratrekk;
            TimeSpan ideellTidsbruk = new TimeSpan(0);

            List<float> yVerdier = new List<float>();
            List<float> yVerdierTotal = new List<float>();
            List<float> idelleYVerdier = new List<float>();
            List<DateTime> xVerdier = new List<DateTime>();

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
            chart.Series.Add(new Series("Ideell Tidsbruk"));
            chart.Series["Burndown"].ChartType = SeriesChartType.Line;
            chart.Series["Beregnet Totaltid (fase)"].ChartType = SeriesChartType.Line;
            chart.Series["Ideell Tidsbruk"].ChartType = SeriesChartType.Line;
            chart.Series["Burndown"].Color = System.Drawing.ColorTranslator.FromHtml("#FF0000FF");
            chart.Series["Beregnet Totaltid (fase)"].Color = System.Drawing.ColorTranslator.FromHtml("#FFFF0000");
            chart.Series["Ideell Tidsbruk"].Color = System.Drawing.ColorTranslator.FromHtml("#FF808080");


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


            double estimatSomDouble = (double)estimatForFase.TotalHours;
            double ideellTid = estimatSomDouble;
            double ideellTidRest;
            int ideelleTimer;
            int ideelleMinutter;

            idelleYVerdier.Add((float)estimatForFase.TotalHours);
            idelleYVerdier.Add((float)estimatForFase.TotalHours);
            for (int i = 0; i < (range.Count - 1); i++)
            {
                ideellTid = ideellTid - (estimatSomDouble / (range.Count - 1));
                ideelleTimer = (int)ideellTid;
                ideellTidRest = ideellTid - (double)ideelleTimer;
                ideelleMinutter = (int)(ideellTidRest * 60);

                ideellTidsbruk = new TimeSpan(ideelleTimer, ideelleMinutter, 0);

                idelleYVerdier.Add((float)ideellTidsbruk.TotalHours);
                idelleYVerdier.Add((float)ideellTidsbruk.TotalHours);

            }

            foreach (DateTime d in range)
            {
                resterendeTidForFase = new TimeSpan();
                tillegsTidForDato = new TimeSpan();
                fratrekk = new TimeSpan();

                for (int i = 0; i < oppgaverForFase.Count; i++)
                {
                    estimatForOppgave = (TimeSpan)oppgaverForFase[i].Estimat;

                    resterendeTidForOppgave = estimatForOppgave;
                    List<Time> registrerteTimerPaaOppgave = Queries.GetTimerForOppgave(oppgaverForFase[i].Oppgave_id);

                    for (int j = 0; j < registrerteTimerPaaOppgave.Count; j++)
                    {
                        if (registrerteTimerPaaOppgave[j].Stopp != null)
                        {
                            DateTime stopDatoForTimeregistrering = (DateTime)registrerteTimerPaaOppgave[j].Stopp;

                            if (stopDatoForTimeregistrering.Date == d.Date)
                            {
                                bruktTidForOppgaver[i] = bruktTidForOppgaver[i] + registrerteTimerPaaOppgave[j].Tid;
                                bruktTid = bruktTid + registrerteTimerPaaOppgave[j].Tid;
                            }
                        }
                    }


                    //legger til tid for fasen dersom det er brukt lenger tid på en oppgave
                    if (bruktTidForOppgaver[i] > estimatForOppgave)
                    {
                        tillegsTidForDato = bruktTidForOppgaver[i] - estimatForOppgave;

                        bruktTidForOppgaver[i] = estimatForOppgave;
                    }

                    //trekker ifra tid for fase dersom en oppgave er ferdigstilt 
                    if (oppgaverForFase[i].Status_id == 3)
                    {
                        ferdigstiltDato = (DateTime)oppgaverForFase[i].Avsluttet;
                        if (ferdigstiltDato.Date == d.Date)
                        {
                            TimeSpan ubruktTid = estimatForOppgave - bruktTidForOppgaver[i];
                            fratrekk += ubruktTid;
                        }
                    }

                }
                resterendeTidForFase = estimatForFase - bruktTid;

                resterendeTidForFaseMedDagensTillegg = resterendeTidForFase + tillegsTidForDato - fratrekk;

                yVerdier.Add((float)resterendeTidForFase.TotalHours);
                yVerdier.Add((float)resterendeTidForFaseMedDagensTillegg.TotalHours);
                xVerdier.Add(d);
                xVerdier.Add(d);
                yVerdierTotal.Add((float)estimatForFase.TotalHours);

                estimatForFase += tillegsTidForDato;
                estimatForFase -= fratrekk;

                yVerdierTotal.Add((float)estimatForFase.TotalHours);


            }

            chart.Series["Ideell Tidsbruk"].Points.DataBindXY(xVerdier, idelleYVerdier);
            chart.Series["Burndown"].Points.DataBindXY(xVerdier, yVerdier);
            chart.Series["Beregnet Totaltid (fase)"].Points.DataBindXY(xVerdier, yVerdierTotal);


            return chart;
        }
    }
}