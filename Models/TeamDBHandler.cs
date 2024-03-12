using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using Microsoft.AspNetCore.Mvc;

namespace CricketWebApplicationMVC.Models
{
    public class TeamDBHandler
    {
        SqlConnection con;

        private void Connection()
        {
            string connection = "Data Source = LAPTOP-CNVSH31R\\SQLEXPRESS01; Integrated Security=True; Database = Cricket_App";
            con = new SqlConnection(connection);
        }

        public bool AddTeam(AddTeamModel team)
        {
            Connection();
            con.Open();

            // Construct the dynamic query for variable number of players
            StringBuilder queryBuilder = new StringBuilder("INSERT INTO Teams (TeamName, TeamLogo");
            StringBuilder valuesBuilder = new StringBuilder(") VALUES (@TeamName, @TeamLogo");

            // Add parameters for players dynamically
            for (int i = 0; i < team.TeamPlayerName.Length; i++)
            {
                string parameterName = $"@Player{i + 1}";
                queryBuilder.Append($", {parameterName}");
                valuesBuilder.Append($", {parameterName}");
            }

            string query = queryBuilder.ToString() + valuesBuilder.ToString() + ")";
            SqlCommand cmd = new SqlCommand(query, con);

            // Use parameters to avoid SQL injection and handle blank values
            cmd.Parameters.AddWithValue("@TeamName", team.TeamName);
            cmd.Parameters.AddWithValue("@TeamLogo", team.TeamLogo);

            for (int i = 0; i < team.TeamPlayerName.Length; i++)
            {
                string parameterName = $"@Player{i + 1}";
                string playerName = team.TeamPlayerName[i];

                // Use DBNull.Value for blank values
                cmd.Parameters.AddWithValue(parameterName, string.IsNullOrWhiteSpace(playerName) ? (object)DBNull.Value : playerName);
            }

            int res = cmd.ExecuteNonQuery();
            con.Close();

            return res > 0;
        }


        public List<string> GetPlayerSuggestions(string query)
        {
            Connection();
            con.Open();

            // Use parameterized query to avoid SQL injection
            string queryString = "SELECT PlayerName FROM PlayerDetails WHERE PlayerName LIKE @searchTerm";
            SqlCommand cmd = new SqlCommand(queryString, con);
            cmd.Parameters.AddWithValue("@searchTerm", "%" + query + "%");

            SqlDataReader reader = cmd.ExecuteReader();
            List<string> playerSuggestions = new List<string>();

            while (reader.Read())
            {
                playerSuggestions.Add(reader["PlayerName"].ToString());
            }

            con.Close();
            return playerSuggestions;
        }
    }
}
