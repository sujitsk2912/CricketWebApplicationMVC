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
            string Query = "Insert into PlayerDetails values ('" + PList.PlayerName + "','" + PList.Born + "','" + PList.City + "','" + PList.Age + "','" + PList.BattingStyle + "','" + PList.BowlingStyle + "','" + PList.PlayingRole + "','" + PList.Team + "','" + PList.PlayerImg + "')";
            SqlCommand cmd = new SqlCommand(Query, con);
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

            return li;
        }



        public bool UpdateRecord(AddPlayerModel iList)
        {
            Connection();
            con.Open();
            string Query = "Update PlayerDetails set PlayerName = '" + iList.PlayerName + "', Born = '" + iList.Born + "', City = '" + iList.City + "', Age = '" + iList.Age + "', BattingStyle = '" + iList.BattingStyle + "', BowlingStyle = '" + iList.BowlingStyle + "', PlayingRole = '" + iList.PlayingRole + "', Team = '" + iList.Team + "',  PlayerImg = '" + iList.PlayerImg + "' where PlayerID = '" + iList.PlayerID + "'";
            SqlCommand cmd = new SqlCommand(Query, con);
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
