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

        public List<CreateMatchModel> GetTeams()
        {
            List<CreateMatchModel> li = new List<CreateMatchModel>();
            Connection();
            con.Open();
            string Query = "select TeamID, TeamName, TeamLogo from Teams";
            SqlDataAdapter adapter = new SqlDataAdapter(Query, con);
            DataSet ds = new DataSet();
            adapter.Fill(ds, "TeamsDs");

            foreach (DataRow dr in ds.Tables["TeamsDs"].Rows)
            {
                li.Add(new CreateMatchModel
                {
                    TeamID = Convert.ToInt32(dr["TeamID"]),
                    TeamName = dr["TeamName"].ToString(),
                    TeamLogo = dr["TeamLogo"].ToString(), // Only filename
                });
            }

            return li;
        }



    }
}
