using System.Data;
using System.Data.SqlClient;
using System.Configuration;
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

            string checkQuery = "SELECT COUNT(*) FROM PlayerDetails WHERE PlayerName = @PlayerName";
            SqlCommand checkCmd = new SqlCommand(checkQuery, con);
            checkCmd.Parameters.AddWithValue("@PlayerName", PList.PlayerName);
            int existingCount = (int)checkCmd.ExecuteScalar();

            if (existingCount > 0)
            {
                con.Close();
                return false; // Player already exists, return false
            }

            string insertQuery = "INSERT INTO PlayerDetails VALUES (@PlayerName, @Born, @City, @Age, @BattingStyle, @BowlingStyle, @PlayingRole, @Team, @PlayerImg)";
            SqlCommand insertCmd = new SqlCommand(insertQuery, con);
            insertCmd.Parameters.AddWithValue("@PlayerName", PList.PlayerName);
            insertCmd.Parameters.AddWithValue("@Born", PList.Born);
            insertCmd.Parameters.AddWithValue("@City", PList.City);
            insertCmd.Parameters.AddWithValue("@Age", PList.Age);
            insertCmd.Parameters.AddWithValue("@BattingStyle", PList.BattingStyle);
            insertCmd.Parameters.AddWithValue("@BowlingStyle", PList.BowlingStyle);
            insertCmd.Parameters.AddWithValue("@PlayingRole", PList.PlayingRole);
            insertCmd.Parameters.AddWithValue("@Team", PList.Team);
            insertCmd.Parameters.AddWithValue("@PlayerImg", PList.PlayerImg);

            int res = insertCmd.ExecuteNonQuery();
            con.Close();

            return res > 0;
        }


        public List<AddPlayerModel> GetRecords()
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
            con.Close();
            return li;
        }



        public bool UpdateRecord(AddPlayerModel iList)
        {
            Connection();
            con.Open();

            string Query = "Update PlayerDetails set PlayerName = @PlayerName, Born = @Born, City = @City, Age = @Age, BattingStyle = @BattingStyle, BowlingStyle = @BowlingStyle, PlayingRole = @PlayingRole, Team = @Team";

            // Check if a new image file is selected
            if (iList.PlayerImg != null)
            {
                Query += ", PlayerImg = @PlayerImg";
            }

            Query += " where PlayerID = @PlayerID";

            SqlCommand cmd = new SqlCommand(Query, con);
            cmd.Parameters.AddWithValue("@PlayerName", iList.PlayerName);
            cmd.Parameters.AddWithValue("@Born", iList.Born);
            cmd.Parameters.AddWithValue("@City", iList.City);
            cmd.Parameters.AddWithValue("@Age", iList.Age);
            cmd.Parameters.AddWithValue("@BattingStyle", iList.BattingStyle);
            cmd.Parameters.AddWithValue("@BowlingStyle", iList.BowlingStyle);
            cmd.Parameters.AddWithValue("@PlayingRole", iList.PlayingRole);
            cmd.Parameters.AddWithValue("@Team", iList.Team);
            cmd.Parameters.AddWithValue("@PlayerID", iList.PlayerID);

            if (iList.PlayerImg != null)
            {
                cmd.Parameters.AddWithValue("@PlayerImg", iList.PlayerImg);
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
