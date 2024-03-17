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

       /* public List<Team> GetTeams()
        {
            Connection();
            con.Open();
            List<Team> teams = new List<Team>();
            string Query = "Select TeamName, TeamLogo from Teams";
            SqlCommand cmd = new SqlCommand(Query, con);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Team team = new Team();
                team.TeamName = reader["TeamName"].ToString();
                team.TeamLogo = reader["TeamLogo"].ToString();
                teams.Add(team);
            }
            con.Close();
            return teams;
        }*/
    }
}
