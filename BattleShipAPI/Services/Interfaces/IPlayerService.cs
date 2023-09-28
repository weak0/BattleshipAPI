using BattleShipAPI.Entities;
using BattleShipAPI.Models;

namespace BattleShipAPI.Services.Interfaces
{
    public interface IPlayerService
    {
        Player CreateOrFindPlayer(string playerName);
    }
}