using BattleShipAPI.Entities;
using BattleShipAPI.Exceptions;
using BattleShipAPI.Models;
using BattleShipAPI.Services;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace BattleShipAPI.Tests.Services;

public class ShotServiceTests
{
    private static readonly DbContextOptions<BattleShipDbContext> Options = new DbContextOptionsBuilder<BattleShipDbContext>()
        .UseInMemoryDatabase(databaseName: "DbForTesting")
        .Options;
    

    [Theory]
    [InlineData(-1)]
    [InlineData(100)]    
    public void Shot_WhenCoordinateIsNotInBoard_ThrowsException(int coordinate)
    {
        using var context = new BattleShipDbContext(Options);
        var service = new ShotService(context);
        var shotDto = new ShotDto() { Cordinate = coordinate, GameBoardId = 1 };
        Assert.Throws<OutOfGameBoardException>(() => service.Shot(shotDto));
    }

    [Fact]
    public void Shot_IsNotYourTurn_ThrowsException()
    {
        using var context = new BattleShipDbContext(Options);
        var service = new ShotService(context);
        var shotDto = new ShotDto() {GameBoardId = 1 , Cordinate = 42};
        var gameBoard = new GameBoard()
        {
            Id = 1,
            Game = new Game() { LastMoveBoardId = 1 }
        };
        context.GameBoards.Add(gameBoard);
        context.SaveChanges();
        Assert.Throws<NotYourTurnException>(() => service.Shot(shotDto));
    }
    [Fact]
    public void Shot_ShotMiss_ReturnsMiss()
    {
        using var context = new BattleShipDbContext(Options);
        var service = new ShotService(context);
        var shotDto = new ShotDto() {GameBoardId = 2 , Cordinate = 42};
        var gameBoard = new GameBoard()
        {
            Id = 2,
            Game = new Game() { LastMoveBoardId = 1 }
        };
        context.GameBoards.Add(gameBoard);
        context.SaveChanges();
        var result = service.Shot(shotDto);
        Assert.Equal(ShotType.Miss, result.ShotResult);
    }

    [Fact]
    public void Shot_ShotHit_ReturnsHit()
    {
        using var context = new BattleShipDbContext(Options);
        var service = new ShotService(context);
        var shotDto = new ShotDto() { GameBoardId = 3, Cordinate = 42 };
        var gameBoard = new GameBoard()
        {
            Id = 3,
            Game = new Game() { LastMoveBoardId = 2 },
            Ships = new List<Ship>()
            {
                new Ship()
                {
                    Cordinates = new List<Cordiantes>()
                    {
                        new Cordiantes() { Cord = 42 },
                        new Cordiantes() { Cord = 43 }
                    }
                }
            }
        };
        context.GameBoards.Add(gameBoard);
        context.SaveChanges();
        var result = service.Shot(shotDto);
        Assert.Equal(ShotType.Hit, result.ShotResult);
    }
    
    [Fact]
    public void Shot_ShotSunk_ReturnsSunk()
    {
        using var context = new BattleShipDbContext(Options);
        var service = new ShotService(context);
        var shotDto = new ShotDto() { GameBoardId = 4, Cordinate = 42 };
        var gameBoard = new GameBoard()
        {
            Id = 4,
            Game = new Game() { LastMoveBoardId = 2 },
            Ships = new List<Ship>()
            {
                new Ship()
                {
                    Cordinates = new List<Cordiantes>()
                    {
                        new Cordiantes() { Cord = 42 },
                    }
                },
                new Ship()
                {
                    Cordinates = new List<Cordiantes>()
                    {
                        new Cordiantes() { Cord = 1 }
                    }
                }
            }
        };
        context.GameBoards.Add(gameBoard);
        context.SaveChanges();
        var result = service.Shot(shotDto);
        Assert.Equal(ShotType.Sunk, result.ShotResult);
    }
    
    [Fact]
    public void Shot_ShotGameOver_ReturnsGameOver()
    {
        using var context = new BattleShipDbContext(Options);
        var service = new ShotService(context);
        var shotDto = new ShotDto() {GameBoardId = 5 , Cordinate = 42};
        var gameBoard = new GameBoard()
        {
            Id = 5,
            Game = new Game() { LastMoveBoardId = 2 },
            Ships = new List<Ship>()
            {
                new Ship()
                {
                    Cordinates = new List<Cordiantes>()
                    {
                        new Cordiantes() {Cord = 42},
                    }
                },
            }
        };
        context.GameBoards.Add(gameBoard);
        context.SaveChanges();
        var result = service.Shot(shotDto);
        Assert.Equal(ShotType.GameOver, result.ShotResult);
    }
    
}