using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System;
namespace CricketWebApplicationMVC.Models
{
    public class PlayerDBHandler
    {
        SqlConnection con;
        private void Connection()
        {
            string connection = "Data Source = LAPTOP-CNVSH31R\\SQLEXPRESS01; Integrated Security=True; Database = Cricket_App";
            con = new SqlConnection(connection);
        }

        public bool InsertRecord(AddPlayerModel PList)
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

            string insertQuery = "INSERT INTO PlayerDetails VALUES (@PlayerName, @Born, @City, @Age, @BattingStyle, @BowlingStyle, @PlayingRole, @PlayerImg)";
            SqlCommand insertCmd = new SqlCommand(insertQuery, con);
            insertCmd.Parameters.AddWithValue("@PlayerName", PList.PlayerName);
            insertCmd.Parameters.AddWithValue("@Born", PList.Born);
            insertCmd.Parameters.AddWithValue("@City", PList.City);
            insertCmd.Parameters.AddWithValue("@Age", PList.Age);
            insertCmd.Parameters.AddWithValue("@BattingStyle", PList.BattingStyle);
            insertCmd.Parameters.AddWithValue("@BowlingStyle", PList.BowlingStyle);
            insertCmd.Parameters.AddWithValue("@PlayingRole", PList.PlayingRole);
            /* insertCmd.Parameters.AddWithValue("@Team", PList.Team);*/
            insertCmd.Parameters.AddWithValue("@PlayerImg", /*encodedImage*/ "img");

            int res = insertCmd.ExecuteNonQuery();
            con.Close();

            return res > 0;
        }


        public List<AddPlayerModel> GetRecords()
        {
            List<AddPlayerModel> players = new List<AddPlayerModel>(); // Use descriptive name

            try
            {
                Connection(); // Assuming Connection() method establishes the connection
                con.Open();

                string query = "SELECT PlayerID, PlayerName, Born, City, Age, BattingStyle, BowlingStyle, PlayingRole, PlayerImg FROM PlayerDetails";  // Specify needed columns
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
                   /* string team = dr["Team"].ToString();*/

/*                    byte[] playerImgBytes = dr["PlayerImg"] == DBNull.Value ? null : (byte[])dr["PlayerImg"];
*/
                    players.Add(new AddPlayerModel
                    {
                        PlayerID = playerID,
                        PlayerName = playerName,
                        Born = born,
                        City = city,
                        Age = age,
                        BattingStyle = battingStyle,
                        BowlingStyle = bowlingStyle,
                        PlayingRole = playingRole,
                      /*  Team = team,*/
                       /* PlayerImg = playerImgBytes*/
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


        public bool UpdateRecord(AddPlayerModel player)
        {
            try
            {
                Connection();
                con.Open();

                string query = "UPDATE PlayerDetails SET PlayerName = @PlayerName, Born = @Born, City = @City, Age = @Age, BattingStyle = @BattingStyle, BowlingStyle = @BowlingStyle, PlayingRole = @PlayingRole";

                if (player.PlayerImg != null)
                {
                    query += ", PlayerImg = @PlayerImg";
                }

                query += " WHERE PlayerID = @PlayerID";

                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@PlayerName", player.PlayerName);
                cmd.Parameters.AddWithValue("@Born", player.Born);
                cmd.Parameters.AddWithValue("@City", player.City);
                cmd.Parameters.AddWithValue("@Age", player.Age);
                cmd.Parameters.AddWithValue("@BattingStyle", player.BattingStyle);
                cmd.Parameters.AddWithValue("@BowlingStyle", player.BowlingStyle);
                cmd.Parameters.AddWithValue("@PlayingRole", player.PlayingRole);
          /*      cmd.Parameters.AddWithValue("@Team", player.Team);*/
                cmd.Parameters.AddWithValue("@PlayerID", player.PlayerID);

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
