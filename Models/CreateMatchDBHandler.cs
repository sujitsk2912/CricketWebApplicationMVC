﻿using System.Collections.Generic;
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

        public bool GetTeams(CreateMatchModel team)
        {
            Connection();
            con.Open();

            string Query = "Select TeamName from Teams";
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
    }
}
