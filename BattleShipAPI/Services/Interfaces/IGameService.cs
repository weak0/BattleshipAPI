using BattleShipAPI.Entities;
using BattleShipAPI.Models;

namespace BattleShipAPI.Services.Interfaces;

public interface IGameService
{
    GameBoard CreateGameBoard(string playerName);
    CreateGameResultDto CreateGame(GameBoard board1, GameBoard board2);

}