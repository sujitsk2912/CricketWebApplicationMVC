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

        public bool Create(CreateMatchModel match)
        {
            try
            {
                Connection();
                con.Open();

                string Query = @"INSERT INTO Matches (TeamA_ID, TeamB_ID, Match_DateTime, Venue, MatchType, Status) 
                                VALUES (@TeamA_ID, @TeamB_ID, @Match_DateTime, @Venue, @MatchType, @Status)";

                SqlCommand cmd = new SqlCommand(Query, con);

                cmd.Parameters.AddWithValue("@TeamA_ID", match.TeamA_ID);
                cmd.Parameters.AddWithValue("@TeamB_ID", match.TeamB_ID);
                cmd.Parameters.AddWithValue("@Match_DateTime", match.Match_DateTime);
                cmd.Parameters.AddWithValue("@Venue", match.Venue);
                cmd.Parameters.AddWithValue("@MatchType", match.MatchType);
                cmd.Parameters.AddWithValue("@Status", match.Status);
               
                int rowsAffected = cmd.ExecuteNonQuery();

                con.Close();

                return rowsAffected > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in Create Match: " + ex.Message);
                return false; 
            }
        }
    }
}
