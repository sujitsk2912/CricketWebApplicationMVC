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
            try
            {
                Connection();
                con.Open();

                // INSERT query for adding a new team
                string Query = @"INSERT INTO AddTeams (TeamName, TeamLogo, Player1, Player2, Player3, Player4, Player5, Player6, Player7, Player8, Player9, Player10, Player11)
                 VALUES (@TeamName, @TeamLogo, @Player1, @Player2, @Player3, @Player4, @Player5, @Player6, @Player7, @Player8, @Player9, @Player10, @Player11)";

                SqlCommand cmd = new SqlCommand(Query, con);

                // Set parameter values for team name and logo
                cmd.Parameters.AddWithValue("@TeamName", team.TeamName);
                cmd.Parameters.AddWithValue("@TeamLogo", team.TeamLogo);
                cmd.Parameters.AddWithValue("@Player1", team.Player1);
                cmd.Parameters.AddWithValue("@Player2", team.Player2);
                cmd.Parameters.AddWithValue("@Player3", team.Player3);
                cmd.Parameters.AddWithValue("@Player4", team.Player4);
                cmd.Parameters.AddWithValue("@Player5", team.Player5);
                cmd.Parameters.AddWithValue("@Player6", team.Player6);
                cmd.Parameters.AddWithValue("@Player7", team.Player7);
                cmd.Parameters.AddWithValue("@Player8", team.Player8);
                cmd.Parameters.AddWithValue("@Player9", team.Player9);
                cmd.Parameters.AddWithValue("@Player10", team.Player10);
                cmd.Parameters.AddWithValue("@Player11", team.Player11);

                // Execute the query
                int rowsAffected = cmd.ExecuteNonQuery();

                con.Close();

                return rowsAffected > 0; // Return true if at least one row is affected
            }
            catch (Exception ex)
            {
                // Handle exceptions, log, or rethrow
                Console.WriteLine("Error in AddTeam: " + ex.Message);
                return false; // Return false to indicate failure
            }
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

        public List<AddTeamModel> GetRecords()
        {
            List<AddTeamModel> li = new List<AddTeamModel>();
            Connection();
            con.Open();
            string Query = "select * from AddTeams";
            SqlDataAdapter adapter = new SqlDataAdapter(Query, con);
            DataSet ds = new DataSet();
            adapter.Fill(ds, "TeamsDs");

            foreach (DataRow dr in ds.Tables["TeamsDs"].Rows)
            {
                li.Add(new AddTeamModel
                {
                    TeamID = Convert.ToInt32(dr["TeamID"]),
                    TeamName = dr["TeamName"].ToString(),
                    TeamLogo = dr["TeamLogo"].ToString(),
                    Player1 = dr["Player1"].ToString(),
                    Player2 = dr["Player2"].ToString(),
                    Player3 = dr["Player3"].ToString(),
                    Player4 = dr["Player4"].ToString(),
                    Player5 = dr["Player5"].ToString(),
                    Player6 = dr["Player6"].ToString(),
                    Player7 = dr["Player7"].ToString(),
                    Player8 = dr["Player8"].ToString(),
                    Player9 = dr["Player9"].ToString(),
                    Player10 = dr["Player10"].ToString(),
                    Player11 = dr["Player11"].ToString()
                });
            }

            return li;
        }

        public List<AddPlayerModel> GetPlayerRecords()
        {
            List<AddPlayerModel> li = new List<AddPlayerModel>();
            Connection();
            con.Open();
            string Query = "select * from PlayerDetails";
            SqlDataAdapter adapter = new SqlDataAdapter(Query, con);
            DataSet ds = new DataSet();
            adapter.Fill(ds, "PlayerDs");

            foreach (DataRow dr in ds.Tables["PlayerDs"].Rows)
            {
                li.Add(new AddPlayerModel
                {
                    PlayerID = Convert.ToInt32(dr["PlayerID"]),
                    PlayerName = dr["PlayerName"].ToString(),
                    Born = dr["Born"].ToString(),
                    City = dr["City"].ToString(),
                    Age = Convert.ToInt32(dr["Age"]),
                    BattingStyle = dr["BattingStyle"].ToString(),
                    BowlingStyle = dr["BowlingStyle"].ToString(),
                    PlayingRole = dr["PlayingRole"].ToString(),
                    Team = dr["Team"].ToString(),
                    PlayerImg = dr["PlayerImg"].ToString()
                });
            }

            return li;
        }

        public bool DeleteRecord(int TeamID)
        {
            Connection();
            con.Open();
            string Query = "delete from AddTeams where TeamID = '" + TeamID + "'";
            SqlCommand cmd = new SqlCommand(Query, con);
            int res = cmd.ExecuteNonQuery();

            if (res > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

       
    }
}
