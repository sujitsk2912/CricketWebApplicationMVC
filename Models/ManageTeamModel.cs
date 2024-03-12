namespace CricketWebApplicationMVC.Models
{
    public class ManageTeamModel
    {
        public string TeamName { get; set; }
        public string TeamLogo { get; set; }

        public string[] TeamPlayerName { get; set; } = new string[11];
    }
}
