using BattleShipAPI.Models;
using BattleShipAPI.Services;
using BattleShipAPI.Services.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace BattleShipAPI.Controllers
{
    [ApiController]
    public class GameController : ControllerBase
    {
        private readonly IGameService _gameService;
        private readonly IShotService _shotService;
        private readonly ISettingsService _settingsService;

        public GameController(IGameService gameService, IShotService shotService, ISettingsService settingsService)
        {
            _gameService = gameService;
            _shotService = shotService;
            _settingsService = settingsService;
        }

        [HttpPost("start")]
        public ActionResult<CreateGameResultDto> GenerateBoard([FromBody] CreateGameDto dto)
        {
            var playerOneGameBoard = _gameService.CreateGameBoard(dto.PlayerOneName);
            var playerTwoGameBoard = _gameService.CreateGameBoard(dto.PlayerTwoName);
            var result = _gameService.CreateGame(playerOneGameBoard, playerTwoGameBoard);
            return Created("",result);
        }
        [HttpPut("shot")]
        public ActionResult<ShotType> Shot([FromQuery] ShotDto shotDto)
        {
            var shotResult = _shotService.Shot(shotDto);
            return Ok(shotResult);
        }
        [HttpPut("configure")]
        public ActionResult Configure([FromBody] ConfigureShipsDto configureShipsDto )
        {
            _settingsService.Configure(configureShipsDto);
            return NoContent();
        }
    }
}
