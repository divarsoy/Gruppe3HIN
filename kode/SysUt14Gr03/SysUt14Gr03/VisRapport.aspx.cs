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
            
            if (rettighet.Rettighet_id == 2) {
                Rapport rapport = new Rapport(Rapport.PROSJEKTRAPPORT, Validator.KonverterTilTall(ddlProsjekter.SelectedValue));
                lblTest.Text = rapport.ToString();
                double[] yValues = { Convert.ToDouble(rapport.GetEstimatForProsjekt()),
                                       Convert.ToDouble(rapport.GetBruktTidPaProsjekt()),
                                       Convert.ToDouble(rapport.GetEstimatForProsjekt() -  rapport.GetBruktTidPaProsjekt())};
                string[] xValues = { "Estimert tid", "Brukt tid", "Gjenstående tid" };
                crtKake.Series["Default"].Points.DataBindXY(xValues, yValues);

                crtKake.Series["Default"].Points[0].Color = Color.MediumSeaGreen;
                crtKake.Series["Default"].Points[1].Color = Color.PaleGreen;
                crtKake.Series["Default"].Points[2].Color = Color.LawnGreen;

                crtKake.Series["Default"].ChartType = SeriesChartType.Pie;

                crtKake.Series["Default"]["PieLabelStyle"] = "Disabled";

                crtKake.ChartAreas["ChartArea1"].Area3DStyle.Enable3D = true;

                crtKake.Legends[0].Enabled = true;

                // http://www.thebestcsharpprogrammerintheworld.com/blogs/how-to-create-a-pie-chart-using-aspnet-and-c-sharp.aspx
   
            }
            else
            {
                Rapport rapport = new Rapport(Rapport.INDIVIDRAPPORT, bruker_id);
                lblTest.Text = rapport.VisProsjektRapportForBruker();
                double[] yValues = { 300d,
                                       160d,
                                       80d};
                string[] xValues = { "Estimert tid", "Brukt tid", "Gjenstående tid" };
                crtKake.Series["Default"].Points.DataBindXY(xValues, yValues);

                crtKake.Series["Default"].Points[0].Color = Color.MediumSeaGreen;
                crtKake.Series["Default"].Points[1].Color = Color.PaleGreen;
                crtKake.Series["Default"].Points[2].Color = Color.LawnGreen;

                crtKake.Series["Default"].ChartType = SeriesChartType.Pie;

                crtKake.Series["Default"]["PieLabelStyle"] = "Disabled";

                crtKake.ChartAreas["ChartArea1"].Area3DStyle.Enable3D = true;

                crtKake.Legends[0].Enabled = true;

                // http://www.thebestcsharpprogrammerintheworld.com/blogs/how-to-create-a-pie-chart-using-aspnet-and-c-sharp.aspx
            }


                
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
        }
    }
}