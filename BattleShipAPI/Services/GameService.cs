using BattleShipAPI.Configuration;
using BattleShipAPI.Entities;
using BattleShipAPI.Models;
using BattleShipAPI.Services.Interfaces;
using Microsoft.EntityFrameworkCore;


namespace BattleShipAPI.Services;

public class GameService : IGameService
{
    private readonly BattleShipDbContext _dbContext;
    private readonly GameConfig _config;
    private readonly IPlayerService _playerService;

    public GameService(BattleShipDbContext dbContext, GameConfig gameConfig, IPlayerService playerService)
    {
        _dbContext = dbContext;
        _config = gameConfig;
        _playerService = playerService;
    }

    public GameBoard CreateGameBoard(string playerName)
    {
        var player = _playerService.CreateOrFindPlayer(playerName);
        var newShips = new List<Ship>();
        var gameBoard = player.GameBoard;

        gameBoard.Ships.Clear();
        _dbContext.SaveChanges();

        foreach (var shipLength in _config.ShipCountsByLength.Keys)
        {
            int numberOfShips = _config.ShipCountsByLength[shipLength];

            for (int i = 0; i < numberOfShips; i++)
            {
                newShips.Add(new Ship { Length = shipLength });
            }
        }

        RandomlyPlaceShip(newShips, gameBoard);
        return gameBoard;
    }

    public CreateGameResultDto CreateGame(GameBoard board1, GameBoard board2)
    {
        var gameBoards = new List<GameBoard>() { board1, board2 };
        var game = new Game() { GameBoard = gameBoards };
        _dbContext.Games.Add(game);
        _dbContext.SaveChanges();

        var result = new CreateGameResultDto()
        {
            PlayerOneOponentBoardId = board2.Id,
            PlayerTwoOponentBoardId = board1.Id,
        };

        return result;
    }

    #region

    private void RandomlyPlaceShip(List<Ship> newShips, GameBoard gameBoard)
    {
        var random = new Random();
        List<Ship> remainingShips = new(newShips);

        foreach (var ship in newShips)
        {
            int shipLength = ship.Length;
            bool isHorizontal = random.Next(2) == 0;

            int startX = random.Next(_config.BoardMaxAxis + 1 - (isHorizontal ? shipLength : 0));
            int startY = random.Next(_config.BoardMaxAxis + 1 - (isHorizontal ? 0 : shipLength));

            bool isPositionValid = ValidateShipPosition(gameBoard.Id, isHorizontal, startX, startY, ship);
            if (isPositionValid)
            {
                PlaceShipOnBoard(gameBoard, isHorizontal, startX, startY, ship);
                remainingShips.Remove(ship);
            }
            else
            {
                RandomlyPlaceShip(remainingShips, gameBoard);
                return;
            }
        }
    }

    private bool ValidateShipPosition(int id, bool isHorizontal, int startX, int startY, Ship ship)
    {
        var ships = _dbContext.Ships
            .Include(s => s.Cordinates)
            .Where(s => s.BoardId == id)
            .SelectMany(s => s.Cordinates).ToList();
        var cords = ships.Select(cords => cords.Cord).ToList();

        for (int i = 0; i < ship.Length; i++)
        {
            int x = isHorizontal ? startX + i : startX;
            int y = isHorizontal ? startY : startY + i;

            if (cords.Any(cord =>
                    cord == x * 10 + y ||
                    cord == (x + 1) * 10 + y ||
                    cord == (x - 1) * 10 + y ||
                    cord == x * 10 + (y + 1) ||
                    cord == x * 10 + (y - 1) ||
                    cord == (x + 1) * 10 + (y + 1) ||
                    cord == (x - 1) * 10 + (y + 1) ||
                    cord == (x + 1) * 10 + (y - 1) ||
                    cord == (x - 1) * 10 + (y - 1)))
            {
                return false;
            }
        }

        return true;
    }

    private void PlaceShipOnBoard(GameBoard gameBoard, bool isHorizontal, int startX, int startY, Ship ship)
    {
        for (int i = 0; i < ship.Length; i++)
        {
            int x = isHorizontal ? startX + i : startX;
            int y = isHorizontal ? startY : startY + i;
            ship.Cordinates.Add(new Cordiantes() { Cord = x * 10 + y });
        }

        gameBoard.Ships.Add(ship);
        _dbContext.SaveChanges();
    }
}

#endregion