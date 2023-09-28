using BattleShipAPI.Configuration;
using BattleShipAPI.Entities;
using BattleShipAPI.Models;
using BattleShipAPI.Services;
using BattleShipAPI.Services.Interfaces;
using BattleShipAPI.Ustils;


namespace BattleShipSimulation
{
    internal class Simulation
    {
        private readonly Random _random = new Random();
        private readonly BattleShipDbContext _dbContext;
        private readonly GameConfig _config;
        private readonly IPlayerService _playerService;
        private readonly IGameService _gameService;
        private readonly IShotService _shotService;
        public string PlayerOneName { get; set; }
        public string PlayerTwoName { get; set; }

        public Simulation(BattleShipDbContext context, string playerOneName, string playerTwoName, GameConfig config)
        {
            PlayerOneName = playerOneName;
            PlayerTwoName = playerTwoName;
            _dbContext = context;
            _config = config;
            _playerService = new PlayerService(context);
            _gameService = new GameService(context, config, _playerService) ;
            _shotService = new ShotService(context) ;
        }

        public void StartSimulation()
        {
            var gb1 = _gameService.CreateGameBoard(PlayerTwoName);
            var gb2 = _gameService.CreateGameBoard(PlayerOneName);
            var game = _gameService.CreateGame(gb1, gb2 );
            var shotResult = new ShotResultDto();
            bool playerOneTurn = true;

            while (shotResult.ShotResult != ShotType.GameOver)
            {
                if (playerOneTurn)
                {
                    shotResult = SimulateShot(PlayerOneName, game.PlayerOneOponentBoardId); ;
                }
                else
                {
                    shotResult = SimulateShot(PlayerTwoName, game.PlayerTwoOponentBoardId);
                }

                playerOneTurn = !playerOneTurn;
            }
        }
        private ShotResultDto SimulateShot(string playerName, int boardId)
        {
            var randomX = _random.Next(_config.BoardMaxAxis + 1);
            var randomY = _random.Next(_config.BoardMaxAxis + 1);
            ShotDto shotDto = new()
            {
                GameBoardId = boardId,
                Cordinate = randomX * 10 + randomY
            };
            var result = _shotService.Shot(shotDto);
            var description = ShotResultToString.GetShotResultDescription(result.ShotResult);
            Console.WriteLine($"{playerName} shoots in x: {randomX}, y: {randomY} and {description} {result.ShipLength}");
            return result;
        }
    }
}
