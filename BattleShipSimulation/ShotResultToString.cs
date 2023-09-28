using BattleShipAPI.Models;

namespace BattleShipAPI.Ustils
{
    public static class ShotResultToString
    {
        public static string GetShotResultDescription(ShotType shotResult)
        {
            return shotResult switch
            {
                ShotType.Miss => "Missed!",
                ShotType.Hit => "Hit!",
                ShotType.Sunk => "Sunk!",
                ShotType.GameOver => "Game Over!",
                _ => "Unknown Result",
            };
        }
    }
}
