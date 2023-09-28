using BattleShipAPI.Configuration;
using BattleShipAPI.Exceptions;
using BattleShipAPI.Models;
using BattleShipAPI.Services.Interfaces;
namespace BattleShipAPI.Services
{
    public class SettingsService : ISettingsService
    {
        private GameConfig _currentConfig;

        public SettingsService(GameConfig config)
        {
            _currentConfig = config;
        }
        public void Configure(ConfigureShipsDto dto)
        {
            var newSettings = new Dictionary<int, int>()
            {
                {2, dto.NumberOfShips2Length },
                {3, dto.NumberOfShips3Length },
                {4, dto.NumberOfShips4Length },
                {5, dto.NumberOfShips5Length }
            };

            if (ValidNumberOfShips(newSettings)) {
                _currentConfig.ShipCountsByLength = newSettings;
            }
        }

        private bool ValidNumberOfShips(Dictionary <int, int> dict)
        {
            var neededCells = 0;
            foreach (var kvp in dict)
            {
                neededCells += kvp.Key * kvp.Value * 3;
            }
            if (neededCells >= _currentConfig.BoardCells)
            {
                throw new WrongConfigException("To many ships");
            }
            return true;
                
        }
    }
}
