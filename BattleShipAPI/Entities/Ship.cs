namespace BattleShipAPI.Entities
{
    public class Ship
    {
        public int Id { get; set; }
        public int BoardId { get; set; } 
        public int Length { get; set; }
        public List<Cordiantes> Cordinates { get; set; } = new List<Cordiantes>();
        public bool IsSunk { get; set; } = false;
        public GameBoard Board { get; set; }
    }
}
