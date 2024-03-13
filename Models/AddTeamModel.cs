using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CricketWebApplicationMVC.Models
{
    public class AddTeamModel
    {
        public string TeamName { get; set; }
        public string TeamLogo { get; set; }

        public List<string> Player { get; set; }
    }
}
