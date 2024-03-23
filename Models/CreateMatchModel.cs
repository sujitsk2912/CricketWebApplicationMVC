using Microsoft.AspNetCore.Mvc.Rendering;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;


namespace CricketWebApplicationMVC.Models
{
    public class CreateMatchModel
    {
        public int MatchID { get; set; }
        public int TeamA_ID { get; set; }
        public int TeamB_ID { get; set; }
        public string Match_DateTime { get; set; }
        public string Venue { get; set; }
        public string MatchType { get; set; }
        public string Status { get; set; }

    }
}
