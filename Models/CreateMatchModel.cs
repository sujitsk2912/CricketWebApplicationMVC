using Microsoft.AspNetCore.Mvc.Rendering;

namespace CricketWebApplicationMVC.Models
{
    public class CreateMatchModel
    {
        public int MatchID { get; set; }
        public string TeamA_Name { get; set; }
        public int TeamA_ID { get; set; }
        public string TeamB_Name { get; set; }
        public int TeamB_ID { get; set; }
        public string TeamA_Logo { get; set; }
        public string TeamB_Logo { get; set; }
        public string Match_DateTime { get; set; }
        public string Venue { get; set; }
        public string MatchType { get; set; }
        public string BattingTeam { get; set; }
        public string BowlingTeam { get; set; }
    }
}
