namespace BattleShipAPI.Configuration
{
    public interface IGameConfig
    {
         Dictionary<int, int> ShipCountsByLength { get; set; }
         int BoardMaxAxis { get; }
         int BoardCells { get; }
    }
}