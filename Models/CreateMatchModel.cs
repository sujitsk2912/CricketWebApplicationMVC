namespace CricketWebApplicationMVC.Models
{
    public class CreateMatchModel
    {
        public string MatchID { get; set; }

        // Define a nested class to represent team data
        public class Team
        {
            public string TeamID { get; set; }
            public string TeamName { get; set; }
            public string TeamLogo { get; set; }
        }

        public Team SelectedTeam { get; set; } // Represents the selected team

        public string MatchType { get; set; }
        public string BattingTeam { get; set; }
        public string MatchDateTime { get; set; }

    }
}
