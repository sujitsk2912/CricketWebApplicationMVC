namespace CricketWebApplicationMVC.Models
{
    public class CreateMatchModel
    {
        public string MatchID { get; set; }

            public string TeamID { get; set; }
            public string TeamName { get; set; }
            public string TeamLogo { get; set; }
  

     

        public string MatchType { get; set; }
        public string BattingTeam { get; set; }
        public string MatchDateTime { get; set; }

    }
}
