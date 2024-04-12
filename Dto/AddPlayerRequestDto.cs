using System.ComponentModel.DataAnnotations;

namespace CricketWebApplicationMVC.Dto
{
    public class AddPlayerRequestDto
    {
        public string PlayerName { get; set; }
        public string DateOfBirth {  get; set; }
        public string City { get; set; }
        public string BattingStyle { get; set; }
        public string BowlingStyle { get; set; }
        public string PlayingRole { get; set; }
        public string PlayerImg { get; set; }
        /* public byte PlayerImg { get; set; }*/

    }
}
