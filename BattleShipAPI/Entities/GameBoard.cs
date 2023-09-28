namespace BattleShipAPI.Entities
{
    public class GameBoard
    {
        public int Id { get; set; }
        public Player Player { get; set; }
        public int PlayerId { get; set; }
        public List<Ship> Ships { get; set; } = new List<Ship>();
        public Game Game { get; set; }
        public int? GameId { get; set; }

    }
}
