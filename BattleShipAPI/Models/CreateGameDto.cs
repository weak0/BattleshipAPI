using System.ComponentModel.DataAnnotations;

namespace BattleShipAPI.Models
{
    public class CreateGameDto
    {
        [Required]
        public string PlayerOneName { get; set; }
        [Required]
        public string PlayerTwoName { get; set;}
    }
}
