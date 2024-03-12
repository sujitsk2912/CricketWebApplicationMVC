using System.ComponentModel.DataAnnotations;

namespace CricketWebApplicationMVC.Models
{
    public class AddTeamModel
    {
        public string TeamName { get; set; }
        public string TeamLogo { get; set; }

        [Required(ErrorMessage = "Team players are required.")]
        public string[] TeamPlayerName { get; set; } = new string[11];
    }
}
