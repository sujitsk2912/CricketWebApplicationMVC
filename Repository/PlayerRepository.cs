using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System;
using CricketWebApplicationMVC.Constants;
using CricketWebApplicationMVC.Models;
using CricketWebApplicationMVC.Dto;
using System.Globalization;

namespace CricketWebApplicationMVC.Repository
{
    public class PlayerRepository
    {
        SqlConnection con;
        private void Connection()
        {
            con = new SqlConnection(ApplicationConstants.ConnectionString);
        }

        public bool InsertPlayer(AddPlayerModel PList)
        {
            Connection();
            con.Open();
/*            string encodedImage = Convert.ToBase64String(PList.PlayerImg);
*/            string checkQuery = "SELECT COUNT(*) FROM PlayerDetails WHERE PlayerName = @PlayerName";
            SqlCommand checkCmd = new SqlCommand(checkQuery, con);
            checkCmd.Parameters.AddWithValue("@PlayerName", PList.PlayerName);
            int existingCount = (int)checkCmd.ExecuteScalar();

            if (existingCount > 0)
            {
                con.Close();
                return false; // Player already exists, return false
            }

            int CurrentYear = DateTime.UtcNow.Year;
            DateTime PlayerDOB = Convert.ToDateTime(PList.Born);
            int Age = CurrentYear - PlayerDOB.Year;


            string insertQuery = "INSERT INTO PlayerDetails VALUES (@PlayerName, @Born, @City, @Age, @BattingStyle, @BowlingStyle, @PlayingRole, @PlayerImg)";
            SqlCommand insertCmd = new SqlCommand(insertQuery, con);
            insertCmd.Parameters.AddWithValue("@PlayerName", PList.PlayerName);
            insertCmd.Parameters.AddWithValue("@Born", PList.Born);
            insertCmd.Parameters.AddWithValue("@City", PList.City != null ? PList.City:"") ;
            insertCmd.Parameters.AddWithValue("@Age", Age);
            insertCmd.Parameters.AddWithValue("@BattingStyle", PList.BattingStyle != null ? PList.BattingStyle:"");
            insertCmd.Parameters.AddWithValue("@BowlingStyle", PList.BowlingStyle != null ? PList.BowlingStyle : "");
            insertCmd.Parameters.AddWithValue("@PlayingRole", PList.PlayingRole != null ? PList.PlayingRole : "");
            insertCmd.Parameters.AddWithValue("@PlayerImg", /*encodedImage != null ? PList.PlayerImg :*/ "");

            int res = insertCmd.ExecuteNonQuery();
            con.Close();

            return res > 0;
        }


        public List<AddPlayerRequestDto> GetRecords()
        {
            List<AddPlayerRequestDto> players = new List<AddPlayerRequestDto>(); // Use descriptive name

            try
            {
                Connection(); // Assuming Connection() method establishes the connection
                con.Open();

                string query = "SELECT PlayerID, PlayerName, Born, City, Age, BattingStyle, BowlingStyle, PlayingRole, Team, PlayerImg FROM PlayerDetails";  // Specify needed columns
                SqlDataAdapter adapter = new SqlDataAdapter(query, con);
                DataSet ds = new DataSet();
                adapter.Fill(ds, "PlayerDs");

                foreach (DataRow dr in ds.Tables["PlayerDs"].Rows)
                {
                    int playerID = Convert.ToInt32(dr["PlayerID"]);
                    string playerName = dr["PlayerName"].ToString();
                    string born = dr["Born"].ToString();
                    string city = dr["City"].ToString();
                    int age = Convert.ToInt32(dr["Age"]);
                    string battingStyle = dr["BattingStyle"].ToString();
                    string bowlingStyle = dr["BowlingStyle"].ToString();
                    string playingRole = dr["PlayingRole"].ToString();
                    string team = dr["Team"].ToString();

                    // Check if PlayerImg is not null before casting
                    byte[] playerImgBytes = dr["PlayerImg"] == DBNull.Value ? null : (byte[])dr["PlayerImg"];

                    players.Add(new AddPlayerRequestDto
                    {
                        PlayerName = playerName,
                        DateOfBirth = born,
                        City = city,
                        BattingStyle = battingStyle,
                        BowlingStyle = bowlingStyle,
                        PlayingRole = playingRole,
                        PlayerImg = /*playerImgBytes*/ ""
                    });
                }
            }
            catch (Exception ex)
            {
                
                Console.WriteLine("Error getting player records: " + ex.Message);
            }
            finally
            {
                if (con != null && con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }

            return players;
        }


        public bool UpdateRecord(AddPlayerRequestDto player) 
        {
            try
            {
                Connection(); 
                con.Open();

                string query = "UPDATE PlayerDetails SET PlayerName = @PlayerName, Born = @Born, City = @City, Age = @Age, BattingStyle = @BattingStyle, BowlingStyle = @BowlingStyle, PlayingRole = @PlayingRole, Team = @Team";

                if (player.PlayerImg != null)
                {
                    query += ", PlayerImg = @PlayerImg";
                }

                query += " WHERE PlayerID = @PlayerID";

                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@PlayerName", player.PlayerName);
                cmd.Parameters.AddWithValue("@Born", player.DateOfBirth);
                cmd.Parameters.AddWithValue("@City", player.City);
                cmd.Parameters.AddWithValue("@BattingStyle", player.BattingStyle);
                cmd.Parameters.AddWithValue("@BowlingStyle", player.BowlingStyle);
                cmd.Parameters.AddWithValue("@PlayingRole", player.PlayingRole);

                if (player.PlayerImg != null)
                {
                    if (player.PlayerImg is byte[])
                    {
                        
                        cmd.Parameters.AddWithValue("@PlayerImg", player.PlayerImg);
                    }
                    else
                    {
                      
                        throw new ArgumentException("Invalid PlayerImg type");
                    }
                }

                int rowsAffected = cmd.ExecuteNonQuery();
                return rowsAffected > 0;
            }
            catch (Exception ex)
            {
                
                Console.WriteLine("Error updating player record: " + ex.Message);
                return false;
            }
            finally
            {
                if (con != null && con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
        }



        public bool DeleteRecord(int PlayerId)
        {
            Connection();
            con.Open();
            string Query = "delete from PlayerDetails where PlayerID = '" + PlayerId + "'";
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
