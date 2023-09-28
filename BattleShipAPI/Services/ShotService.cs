using BattleShipAPI.Entities;
using BattleShipAPI.Exceptions;
using BattleShipAPI.Models;
using BattleShipAPI.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
namespace BattleShipAPI.Services;


public class ShotService : IShotService
{
    private readonly BattleShipDbContext _dbContext;

    public ShotService(BattleShipDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public ShotResultDto Shot(ShotDto dto)
    {
        IsCoordinateInBoard(dto.Cordinate);

        var gameBoard = GetShips(dto.GameBoardId);

        IsItYourTurn(gameBoard.Game, dto.GameBoardId);

        var hitShip = gameBoard.Ships
            .Where(ship => !ship.IsSunk)
            .FirstOrDefault(ship => ship.Cordinates.Any(c => c.Cord == dto.Cordinate));

        var getShotType = GetShotType(hitShip, dto.Cordinate, gameBoard);

        var shipLength = hitShip?.Length ?? 0;

        var result = new ShotResultDto() { ShotResult = getShotType, ShipLength =  shipLength };

        return result;

    }
    
    private ShotType GetShotType(Ship hitShip, int cord, GameBoard gameBoard)
    {
        if (hitShip == null) return ShotType.Miss;
        var cordToRemove = hitShip.Cordinates.First(c => c.Cord == cord);
        hitShip.Cordinates.Remove(cordToRemove);
        _dbContext.SaveChanges();
        
        if (hitShip.Cordinates.Count != 0) return ShotType.Hit;
        hitShip.IsSunk = true;
        _dbContext.SaveChanges();
        
        var result = IsGameOver(gameBoard);
        return result;
    }
    private ShotType IsGameOver(GameBoard gameBoard)
    {
        var hasUnSunkShips = gameBoard.Ships.Any(sh => !sh.IsSunk);
        if (hasUnSunkShips)
            return ShotType.Sunk;

        _dbContext.GameBoards.Remove(gameBoard);
        _dbContext.SaveChanges();
        return ShotType.GameOver;

        

    }
    private GameBoard GetShips(int id)
    {
        var listOfShips = _dbContext.GameBoards
        .Where(gb => gb.Id == id)
        .Include(gb => gb.Game)
        .Include(gb => gb.Ships)
            .ThenInclude(s => s.Cordinates)
            .FirstOrDefault()
        ?? throw new NotFoundException("This game board not exists");

        return listOfShips;
    }

 

    private void IsItYourTurn(Game game, int gameBoardId)
    {
        
        if (game.LastMoveBoardId == gameBoardId) 
            throw new NotYourTurnException("It is not your turn");
        
        game.LastMoveBoardId = gameBoardId;
        _dbContext.SaveChanges();
    }

    private static void IsCoordinateInBoard(int cord)
    {
        if (cord is < 0 or > 99)
        {
            throw new OutOfGameBoardException("Your coordinates are not valid");
        }
    }
}


