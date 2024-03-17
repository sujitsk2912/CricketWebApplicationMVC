namespace CricketWebApplicationMVC.Models
{
    public class CreateMatchModel
    {
        public int MatchID { get; set; }

        public int TeamID { get; set; }

        public string TeamName { get; set; }
        public string TeamLogo { get; set; }

        public string Match_DateTime { get; set; }

        public string MatchType { get; set;}
        public string Other_MatchType { get; set; }

        public string BattingTeam { get; set; }

        public string BowlingTeam { get; set; }

    }
}
