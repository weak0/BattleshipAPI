namespace BattleShipAPI.Configuration
{
    public class GameConfig : IGameConfig
    {
        public Dictionary<int, int> ShipCountsByLength { get; set; } = new()
        {
            { 2, 2 },
            { 3, 1 },
            { 4, 1 },
            { 5, 1 }
        };
        public int BoardMaxAxis { get; } = 9;

        public  int BoardCells { get; } = 100;
    }
}
