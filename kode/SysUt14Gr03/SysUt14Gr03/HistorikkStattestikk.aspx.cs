using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Providers.Entities;
using System.Web.UI;
using System.Web.UI.WebControls;
using SysUt14Gr03.Models;
using SysUt14Gr03.Classes;
using System.Web.Security;

namespace SysUt14Gr03
{
    public partial class HistorikkStattestikk : System.Web.UI.Page
    {
        private int brukerID;
        private Login getInfo;
        private Models.Bruker user;
        private Models.Moete moete;
        private Models.Oppgave assignment;
        private Models.Prosjekt prosjekt;
        private Models.Context context;
        private Models.Status status;

        protected void Page_Load(object sender, EventArgs e)
        {
            getInfo = new Login();
            user = new Models.Bruker();
            moete = new Models.Moete();
            assignment = new Models.Oppgave();
            prosjekt = new Models.Prosjekt();
            context = new Models.Context();
            status = new Models.Status();

            brukerID = 1;
            //brukerID = Convert.ToInt32(Membership.GetUser(HttpContext.Current.User.Identity.Name).ProviderUserKey);

            this.gragh();
        }
        protected void btn_nav_changeUser_Click(object sender, EventArgs e)
        {
            Response.Redirect("http://localhost:60154/changeUser.aspx");
            //Server.Transfer("/changeUser.aspx");
        }
        public void gragh()
        {

        }

        // The return type can be changed to IEnumerable, however to support
        // paging and sorting, the following parameters must be added:
        //     int maximumRows
        //     int startRowIndex
        //     out int totalRowCount
        //     string sortByExpression
        public IQueryable lvStatistics_GetData()
        {
            return null;
        }
        public IQueryable lvHistory_GetData()
        {
            return null;
        }
        public IQueryable lvActivity_GetData()
        {
            return null;
        }

        protected void Projects_SelectedIndexChanged(object sender, EventArgs e)
        {
            List<Prosjekt> prosjektList = Queries.GetAlleProsjektEnBrukerErMedI(brukerID);

            if (Project.Items.Count == 0)
            {
                for (int i = 0; i < prosjektList.Count(); i++)
                {
                    Prosjekt prosjekt = prosjektList[i];

                    Project.Items.Add("Navn: " + prosjekt.Navn);
                }
            }
        }

        protected void Meetings_SelectedIndexChanged(object sender, EventArgs e)
        {
            List<Moete> moeteList = Queries.GetAlleMoeterFraBrukerErMedI(brukerID);

            if (Meeting.Items.Count == 0)
            {
                for (int i = 0; i < moeteList.Count(); i++)
                {
                    Moete moete = moeteList[i];

                    Meeting.Items.Add("Navn: " + prosjekt.Navn);
                }
            }
        }

        protected void Team_SelectedIndexChanged(object sender, EventArgs e)
        {
            List<Team> teamList = Queries.GetAlleTeamsEnBrukerErMedI(brukerID);

            if (Team.Items.Count == 0)
            {
                for (int i = 0; i < teamList.Count(); i++)
                {
                    Team team = teamList[i];

                    Team.Items.Add("Navn: " + team.Navn);
                }
            }
        }

        /*protected ArrayList team()
        {
            using (SqlCommand command = new SqlCommand())
            {
                string query = "SELECT * FROM Team WHERE Brukere = " + username + "'";
                command.Connection = new SqlConnection(ConfigurationManager.ConnectionStrings["sysUt14Gr03"].ConnectionString);

                var list = new ArrayList();
                var reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        string team = reader.GetString(4);
                        list.Add(team);
                    }
                }
                else
                {
                    string respons = "Fikk ikke hentet ut informasjon fra tabellen Bruker";
                    list.Add(respons);
                }

                reader.Close();
                command.Connection.Close();
                return list;
            }
        }*/
    }
}