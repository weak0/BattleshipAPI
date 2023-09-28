using System.ComponentModel.DataAnnotations;

namespace BattleShipAPI.Models
{
    public class ShotDto
    {
        [Required]
        public int  GameBoardId { get; set; }
        [Required]
        public int Cordinate { get; set; }
    }
}
