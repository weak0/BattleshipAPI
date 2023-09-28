using BattleShipAPI.Models;

namespace BattleShipAPI.Services.Interfaces;

public interface ISettingsService
{
    void Configure(ConfigureShipsDto dto);
}