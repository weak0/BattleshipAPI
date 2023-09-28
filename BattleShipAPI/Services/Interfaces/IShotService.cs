using BattleShipAPI.Entities;
using BattleShipAPI.Models;

namespace BattleShipAPI.Services.Interfaces;

public interface IShotService
{
    ShotResultDto Shot(ShotDto dto);
}