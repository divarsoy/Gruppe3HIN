using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SysUt14Gr03.Classes;
using SysUt14Gr03.Models;
using System.Drawing;
using System.Web.UI.DataVisualization.Charting;

namespace SysUt14Gr03
{
    public partial class VisRapport : System.Web.UI.Page
    {
        private int bruker_id;
        private Rettighet rettighet;
        private Chart crtKake = new Chart();

        protected void Page_Load(object sender, EventArgs e)
        {
            SessionSjekk.sjekkForBruker_id();
            bruker_id = Validator.KonverterTilTall(Session["bruker_id"].ToString());
            if (bruker_id != -1)
            {
                Bruker bruker = Queries.GetBruker(bruker_id);
                rettighet = Queries.GetRettighet(bruker_id);
                if (rettighet.Rettighet_id == 2)
                {
                    if (!IsPostBack)
                    {
                        List<Bruker> brukerListe = new List<Bruker>();
                        List<Prosjekt> prosjektListe = Queries.GetAlleAktiveProsjekterForProsjektLeder(bruker_id);
                        foreach (Prosjekt p in prosjektListe)
                        {
                            ddlProsjekter.Items.Add(new ListItem(p.Navn, p.Prosjekt_id.ToString()));
                            foreach (Bruker b in Queries.GetAlleBrukereIEtProjekt(p.Prosjekt_id))
                            {
                                brukerListe.Add(b);
                            }
                        }

                        foreach (Bruker b in brukerListe)
                        {
                            ddlBrukere.Items.Add(new ListItem(b.ToString(), b.Bruker_id.ToString()));
                        }
                    }
                    ddlBrukere.Visible = true;
                    ddlProsjekter.Visible = true;
                    btnIndivid.Visible = true;
                    btnProsjekt.Visible = true;
                }

                if (rettighet.Rettighet_id == 3)
                {
                    if (!IsPostBack)
                    {
                        List<Prosjekt> prosjektListe = Queries.GetAlleAktiveProsjekterForBruker(bruker_id);
                        foreach (Prosjekt p in prosjektListe)
                        {
                            ddlProsjekter.Items.Add(new ListItem(p.Navn, p.Prosjekt_id.ToString()));

                        }

                        btnIndivid.Visible = true;
                        btnProsjekt.Visible = true;

                    }
                }
            }
        }

        protected void btnTeam_Click(object sender, EventArgs e)
        {

        }

        protected void btnProsjekt_Click(object sender, EventArgs e)
        {

            crtKake.Series.Add("Default");
            crtKake.ChartAreas.Add("ChartArea1");

            if (rettighet.Rettighet_id == 2) {
                Rapport rapport = new Rapport(Rapport.PROSJEKTRAPPORT, Validator.KonverterTilTall(ddlProsjekter.SelectedValue));
                lblTest.Text = rapport.ToString();

                TimeSpan test1 = rapport.GetEstimatForProsjekt();
                TimeSpan test2 = rapport.GetBruktTidPaProsjekt();

                string estimert = "Estimert tid: ";
                estimert += (int) rapport.GetEstimatForProsjekt().TotalHours + " timer ";
                estimert += rapport.GetEstimatForProsjekt().Minutes + " minutter ";

                string brukt = "Brukt tid: ";
                brukt += (int) rapport.GetBruktTidPaProsjekt().TotalHours + " timer ";
                brukt += rapport.GetBruktTidPaProsjekt().Minutes + " minutter ";

                string gjenstaende = "Gjenstående tid: ";
                gjenstaende += (int)(rapport.GetEstimatForProsjekt() - rapport.GetBruktTidPaProsjekt()).TotalHours + " timer ";
                gjenstaende += (rapport.GetEstimatForProsjekt() - rapport.GetBruktTidPaProsjekt()).Minutes + " minutter ";


                double[] yValues = {Convert.ToDouble(rapport.GetBruktTidPaProsjekt().TotalMilliseconds),
                                       Convert.ToDouble(rapport.GetEstimatForProsjekt().TotalMilliseconds -  rapport.GetBruktTidPaProsjekt().TotalMilliseconds)};
                string[] xValues = {brukt, gjenstaende};
                

                crtKake.Titles.Add("Estimert tid: " + rapport.GetEstimatForProsjekt().TotalHours);

                crtKake.Series["Default"].Points.DataBindXY(xValues, yValues);

                crtKake.Series["Default"].Points[0].Color = Color.MediumSpringGreen;
                crtKake.Series["Default"].Points[1].Color = Color.LightYellow;

                crtKake.Series["Default"].ChartType = SeriesChartType.Pie;

                // crtKake.Series["Default"]["PieLabelStyle"] = "Disabled";

                crtKake.ChartAreas["ChartArea1"].Area3DStyle.Enable3D = true;

                // crtKake.Legends[0].Enabled = true;

                // http://www.thebestcsharpprogrammerintheworld.com/blogs/how-to-create-a-pie-chart-using-aspnet-and-c-sharp.aspx
   
            }
            else
            {
                Rapport rapport = new Rapport(Rapport.INDIVIDRAPPORT, bruker_id);
                lblTest.Text = rapport.VisProsjektRapportForBruker();

                double[] yValues = {Convert.ToDouble(rapport.GetAntallFerdigeOppgaverForBruker()),
                                       Convert.ToDouble(rapport.GetAntallOppgaverForBruker() -  rapport.GetAntallFerdigeOppgaverForBruker())};

                TimeSpan test1 = rapport.GetEstimatForProsjekt();
                TimeSpan test2 = rapport.GetBruktTidPaProsjekt();

                string[] xValues = { "Fullførte oppgaver: " + rapport.GetAntallFerdigeOppgaverForBruker(), 
                                       "Gjenstående oppgaver: " + (rapport.GetAntallOppgaverForBruker() -  rapport.GetAntallFerdigeOppgaverForBruker())};

                crtKake.Titles.Add("Antall oppgaver: " + rapport.GetAntallOppgaverForBruker());

                crtKake.Series["Default"].Points.DataBindXY(xValues, yValues);

                crtKake.Series["Default"].Points[0].Color = Color.MediumSeaGreen;
                crtKake.Series["Default"].Points[1].Color = Color.PaleGreen;

                crtKake.Series["Default"].ChartType = SeriesChartType.Pie;

                // crtKake.Series["Default"]["PieLabelStyle"] = "Disabled";

                crtKake.ChartAreas["ChartArea1"].Area3DStyle.Enable3D = true;


                // crtKake.Legends[0].Enabled = true;

                Chart testChart = new Chart();

                double[] yValues2 = {Convert.ToDouble(rapport.GetSumFullforteTimerForBruker().TotalHours),
                                     Convert.ToDouble(rapport.GetSumTimerForBruker().TotalHours)};


                string[] xValues2 = { "Brukte timer: " + rapport.GetSumFullforteTimerForBruker(), 
                                       "Gjenstående timer: " + (rapport.GetSumTimerForBruker())};


                testChart.Titles.Add("Total tid " + (int)(rapport.GetSumTimerForBruker().TotalHours + rapport.GetSumFullforteTimerForBruker().TotalHours)
                    + ":" + (rapport.GetSumTimerForBruker() + rapport.GetSumFullforteTimerForBruker()).Minutes);

                testChart.Series.Add("Default2");
                testChart.ChartAreas.Add("ChartArea2");

                testChart.Series["Default2"].Points.DataBindXY(xValues2, yValues2);

                testChart.Series["Default2"].Points[0].Color = Color.MediumSeaGreen;
                testChart.Series["Default2"].Points[1].Color = Color.PaleGreen;

                testChart.Series["Default2"].ChartType = SeriesChartType.Pie;

                // testChart.Series["Default2"]["PieLabelStyle"] = "Disabled";

                testChart.ChartAreas["ChartArea2"].Area3DStyle.Enable3D = true;
                testChart.Width = 300;
                testChart.Height = 300;

                // testChart.Legends[0].Enabled = true;

                pnlGrafikk.Controls.Add(testChart);
                


                // http://www.thebestcsharpprogrammerintheworld.com/blogs/how-to-create-a-pie-chart-using-aspnet-and-c-sharp.aspx
            }


            crtKake.Width = 300;
            crtKake.Height = 300;
            crtKake.Visible = true;
            lblTest.Visible = true;
            pnlGrafikk.Visible = true;
            pnlGrafikk.Controls.Add(crtKake);
                
        }

        protected void btnIndivid_Click(object sender, EventArgs e)
        {
            if (rettighet.Rettighet_id == 2)
            {
                Rapport rapport = new Rapport(Rapport.INDIVIDRAPPORT, Validator.KonverterTilTall(ddlBrukere.SelectedValue));
                lblTest.Text = rapport.ToString();
            }
            else
            {
                Rapport rapport = new Rapport(Rapport.INDIVIDRAPPORT, bruker_id);
                lblTest.Text = rapport.ToString();
            }

            lblTest.Visible = true;
        }
    }
}