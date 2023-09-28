namespace BattleShipAPI.Entities
{
    public class Game
    {
        public int Id { get; set; }
        public  List<GameBoard> GameBoard { get; set; } = new List<GameBoard>();
        public int LastMoveBoardId { get; set; }

    }
}
