using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using Microsoft.AspNetCore.Mvc;

namespace CricketWebApplicationMVC.Models
{
    public class CreateMatchDBHandler
    {
        SqlConnection con;

        private void Connection()
        {
            string connection = "Data Source = LAPTOP-CNVSH31R\\SQLEXPRESS01; Integrated Security=True; Database = Cricket_App";
            con = new SqlConnection(connection);
        }

        public List<AddTeamModel> GetTeams()
        {
            List<AddTeamModel> teams = new List<AddTeamModel>(); 
            Connection();
            con.Open();
            string Query = "select TeamID, TeamName, TeamLogo from AddTeams";
            SqlDataAdapter adapter = new SqlDataAdapter(Query, con);
            DataSet ds = new DataSet();
            adapter.Fill(ds, "TeamsDs");

            foreach (DataRow dr in ds.Tables["TeamsDs"].Rows)
            {
                AddTeamModel team = new AddTeamModel();
                team.TeamID = Convert.ToInt32(dr["TeamID"]);
                team.TeamName = dr["TeamName"].ToString();
                team.TeamLogo = dr["TeamLogo"].ToString(); // Assuming TeamLogo is the file name

                teams.Add(team);
            }

            con.Close(); // Close the connection when done

            return teams;
        }
    }
}
