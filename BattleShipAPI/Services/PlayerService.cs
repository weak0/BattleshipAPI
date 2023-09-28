using BattleShipAPI.Entities;
using BattleShipAPI.Models;
using BattleShipAPI.Services.Interfaces;
using Microsoft.EntityFrameworkCore;


namespace BattleShipAPI.Services
{
    public class PlayerService : IPlayerService
    {
        private readonly BattleShipDbContext _dbContext;

        public PlayerService(BattleShipDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public Player CreateOrFindPlayer(string playerName)
        {
            var player = _dbContext.Players
                .Include(p => p.GameBoard)
                .ThenInclude(gb => gb.Ships)
                .FirstOrDefault(p => p.Name == playerName);
            
            player ??= CreatePlayer(playerName);
            player.GameBoard ??= new GameBoard();
            return player;
        }
        private Player CreatePlayer(string playerName)
        {
            var player = new Player() { Name = playerName};
            _dbContext.Players.Add(player);
            _dbContext.SaveChanges();
            return player;
        }
    }
}
