using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using Microsoft.AspNetCore.Mvc;

namespace CricketWebApplicationMVC.Models
{
    public class ManageTeamDBHandler
    {
        SqlConnection con;

        private void Connection()
        {
            string connection = "Data Source = LAPTOP-CNVSH31R\\SQLEXPRESS01; Integrated Security=True; Database = Cricket_App";
            con = new SqlConnection(connection);
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
            string Query = "select * from Teams";
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

        public bool UpdateRecord(AddTeamModel iList)
        {
            Connection();
            con.Open();

            string Query = "Update Teams set TeamName = @TeamName, Player1 = @Player1, Player2 = @Player2, Player3 = @Player3, Player4 = @Player4, Player5 = @Player5, Player6 = @Player6, Player7 = @Player7, Player8 = @Player8, Player9 = @Player9, Player10 = @Player10, Player11 = @Player11";

            if (iList.TeamLogo != null)
            {
                Query += ", TeamLogo = @TeamLogo";
            }

            Query += " where TeamID = @TeamID";

            SqlCommand cmd = new SqlCommand(Query, con);
            cmd.Parameters.AddWithValue("@TeamName", iList.TeamName);
            cmd.Parameters.AddWithValue("@Player1", iList.Player1);
            cmd.Parameters.AddWithValue("@Player2", iList.Player2);
            cmd.Parameters.AddWithValue("@Player3", iList.Player3);
            cmd.Parameters.AddWithValue("@Player4", iList.Player4);
            cmd.Parameters.AddWithValue("@Player5", iList.Player5);
            cmd.Parameters.AddWithValue("@Player6", iList.Player6);
            cmd.Parameters.AddWithValue("@Player7", iList.Player7);
            cmd.Parameters.AddWithValue("@Player8", iList.Player8);
            cmd.Parameters.AddWithValue("@Player9", iList.Player9);
            cmd.Parameters.AddWithValue("@Player10", iList.Player10);
            cmd.Parameters.AddWithValue("@Player11", iList.Player11);
            cmd.Parameters.AddWithValue("@TeamID", iList.TeamID);

            if (iList.TeamLogo != null)
            {
                cmd.Parameters.AddWithValue("@TeamLogo", iList.TeamLogo);
            }

            int res = cmd.ExecuteNonQuery();
            con.Close();

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
