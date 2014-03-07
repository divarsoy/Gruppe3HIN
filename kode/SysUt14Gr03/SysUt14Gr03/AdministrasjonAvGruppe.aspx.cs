using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SysUt14Gr03.Classes;
using SysUt14Gr03.Models;

namespace SysUt14Gr03
{
    public partial class AdministrasjonAvGruppe : System.Web.UI.Page
    {
        private List<Gruppe> gruppeListe;
        private ArrayList userInTeam;
        private MailMessage msg;
        private Classes.sendEmail sendMsg;

        protected void Page_Load(object sender, EventArgs e)
        {
            gruppeListe = Queries.GetAlleAktiveGrupper();
            for (int i = 0; i < gruppeListe.Count; i++)
            {
                lsbGrupper.Items.Add(gruppeListe[i].Navn);
            }
        }

        
        protected void lsbGrupper_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Gjør noe med valgt gruppe
            int index = lsbGrupper.SelectedIndex;
            //gruppeListe = Queries.GetAlleAktiveGrupper();
            List<Team> teamList = gruppeListe[index].Teams.ToList<Team>();
            for (int i = 0; i < teamList.Count; i++)
            {
                lsbTeam.Items.Add(teamList[i].Navn);
            }
            lsbTeam.Visible = true;
        }
         

        
        protected void btnVisTeam_Click(object sender, EventArgs e)
        {
            int index = lsbGrupper.SelectedIndex;
            
            List<Team> teamList = gruppeListe[index].Teams.ToList<Team>();
            for (int i = 0; i < teamList.Count; i++)
            {
                lsbTeam.Items.Add(teamList[i].Navn);
            }
        }
        public void sendEpost()
        {

            msg.Subject = "Bekreftelses epost for konto aktivering";
            for (int i = 0; i < gruppeListe.Count; i++)
            {
                msg.Body = "Ditt team " + gruppeListe[i] + "";
            }

            sendMsg.sendEpost(null, msg.Body, msg.Subject, null, null, userInTeam);
        }
        public ArrayList getUserInTeam()
        {
            using (SqlCommand command = new SqlCommand())
            {
                string query = "SELECT * FROM Bruker WHERE Teams = " + gruppeListe.ToString() + "'";
                command.Connection = new SqlConnection(ConfigurationManager.ConnectionStrings["sysUt14Gr03"].ConnectionString);

                userInTeam = new ArrayList();
                var reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        string epost = reader.GetString(4);
                        string teamName = reader.GetString(18);
                        userInTeam.Add(epost);
                        userInTeam.Add(teamName);
                    }
                }
                else
                {
                    string respons = "Fikk ikke hentet ut informasjon fra tabellen Bruker";
                    userInTeam.Add(respons);
                }

                reader.Close();
                command.Connection.Close();
                return userInTeam;
            }
        }
    }
}