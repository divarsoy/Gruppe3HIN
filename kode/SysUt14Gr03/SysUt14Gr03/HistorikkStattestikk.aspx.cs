using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SysUt14Gr03
{
    public partial class HistorikkStattestikk : System.Web.UI.Page
    {
        private string username = "Cindy Larkin";
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

            brukerID = 1; // getInfo.getBrukerID();

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
        protected IQueryable lbProjects()
        {
            return null;
        }
        protected void lbMeetings()
        {

        }
        protected ArrayList team()
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
        }
        protected void lbGruppe(object sender, EventArgs e)
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
    }
}