using System.ComponentModel.DataAnnotations;

namespace CricketWebApplicationMVC.Models
{
    public class AddPlayerModel
    {
        [Key]
        public int PlayerID { get; set; }
        public string PlayerName { get; set; }
        public string Born {  get; set; }
        public string Country { get; set; }
        public int Age { get; set; }
        public string BattingStyle { get; set; }
        public string BowlingStyle { get; set; }
        public string PlayingRole { get; set; }
        public string Team { get; set; }
        public string PlayerImg { get; set; }

    }
}
