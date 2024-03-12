namespace CricketWebApplicationMVC.Models
{
    public class AddRecordModel
    {
        public int MatchID { get; set; }

        public string MatchType { get; set; }

        public string TeamName { get; set; }

        public string TeamLogo { get; set; }

        public int PlayerID { get; set; }
        public int Overs {  get; set; }

        public string CurrentScore { get; set; }
        public int Balls { get; set; }
        public int Runs { get; set; }

        public int Wickets { get; set; }

        public string Description { get; set; }
    }
}
