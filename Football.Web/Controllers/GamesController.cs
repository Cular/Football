// <copyright file="GamesController.cs" company="Yarik Home">
// All rights maybe reserved. © Yarik
// </copyright>

namespace Football.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using AutoMapper;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Models.Data;
    using Models.Data.GameState;
    using Models.Dto;
    using Services.Game;

    /// <summary>
    /// The game controller.
    /// </summary>
    [Route("api/games")]
    [ApiController]
    [Authorize]
    public partial class GamesController : ControllerBase
    {
        private readonly IGameService gameService;
        private readonly IMapper mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="GamesController"/> class.
        /// </summary>
        /// <param name="gameService">The game service.</param>
        /// <param name="mapper">The mapper.</param>
        public GamesController(IGameService gameService, IMapper mapper)
        {
            this.gameService = gameService;
            this.mapper = mapper;
        }

        /// <summary>
        /// Creates game.
        /// </summary>
        /// <param name="dto">Model of game for creation.</param>
        /// <returns>Task</returns>
        [ProducesResponseType(200, Type = typeof(Guid))]
        [HttpPost]
        public async Task<IActionResult> CreateGame([FromBody]GameCreateDto dto)
        {
            var entity = this.mapper.Map<Game>(dto);

            entity.AdminId = this.User.Identity.Name;
            entity.CanAddPlayer(this.User.Identity.Name);

            await this.gameService.CreateAsync(entity);

            return this.Ok(entity.Id);
        }

        /// <summary>
        /// Deletes the game.
        /// </summary>
        /// <param name="gameId">The game identifier.</param>
        /// <returns>The action result.</returns>
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(403)]
        [HttpDelete]
        [Route("{gameId}")]
        public async Task<IActionResult> DeleteGame([FromRoute]Guid gameId)
        {
            if (gameId == Guid.Empty)
            {
                return this.BadRequest("Game identifier can not be default.");
            }

            if (await this.gameService.TryDeleteGameAsync(gameId, this.User.Identity.Name))
            {
                return this.Ok();
            }

            return this.BadRequest("Game can not be deleted.");
        }

        /// <summary>
        /// Changes state of the game.
        /// </summary>
        /// <param name="gameId">The game identifier.</param>
        /// <param name="gameStateEnum">New state of game.</param>
        /// <returns>The action result.</returns>
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(403)]
        [HttpPut]
        [Route("{gameId}/state")]
        public async Task<IActionResult> ChangeGameState([FromRoute]Guid gameId, [FromQuery] GameStateEnum gameStateEnum)
        {
            if (gameId == Guid.Empty)
            {
                return this.BadRequest("Game identifier can not be default.");
            }

            if (await this.gameService.TryChangeGameStateAsync(gameId, this.User.Identity.Name, gameStateEnum))
            {
                return this.Ok();
            }

            return this.BadRequest($"Game {gameId} can not be chaned to state {gameStateEnum}.");
        }

        /// <summary>
        /// Find all created games by user.
        /// </summary>
        /// <returns>Array of games.</returns>
        [Route("my")]
        [HttpGet]
        [ProducesResponseType(200, Type=typeof(List<GameListItemDto>))]
        public async Task<IActionResult> GetMyGames()
        {
            var result = await this.gameService.GetMyGamesAsync(this.User.Identity.Name);

            return this.Ok(result.Select(r => this.mapper.Map<GameListItemDto>(r)));
        }

        /// <summary>
        /// Find all games where player is involved.
        /// </summary>
        /// <param name="page">page number.</param>
        /// <param name="count">count number.</param>
        /// <param name="gameState">state of game.</param>
        /// <returns>List of <see cref="GameListItemDto"/></returns>
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(List<GameListItemDto>))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetGames([FromQuery] int page = 1, [FromQuery] int count = 20, [FromQuery] GameStateEnum gameState = GameStateEnum.Open)
        {
            if (page < 1)
            {
                return this.BadRequest("Page number can not be less than 1.");
            }

            var result = await this.gameService.GetAllGamesAsync(this.User.Identity.Name, page, count, gameState);

            return this.Ok(result.Select(r => this.mapper.Map<GameListItemDto>(r)));
        }

        /// <summary>
        /// Find game by id.
        /// </summary>
        /// <param name="gameId">the game identifier.</param>
        /// <returns>Emtity <see cref="GameDto"/></returns>
        [HttpGet]
        [Route("{gameId}")]
        [ProducesResponseType(200, Type = typeof(GameDto))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetGame([FromRoute] Guid gameId)
        {
            if (gameId == Guid.Empty)
            {
                return this.BadRequest("Game identifier can not be default.");
            }

            var result = await this.gameService.GetGameAsync(gameId);

            if (result == null)
            {
                return this.NotFound();
            }

            return this.Ok(this.mapper.Map<GameDto>(result));
        }

        /// <summary>
        /// Adding player by alias to game
        /// </summary>
        /// <param name="gameId">The game identifier.</param>
        /// <param name="alias">The player alias.</param>
        /// <returns>The action result.</returns>
        [HttpPut]
        [Route("{gameId}/_invite")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> InvitePlayer([FromRoute] Guid gameId, [FromQuery] string alias)
        {
            if (gameId == Guid.Empty || string.IsNullOrEmpty(alias))
            {
                return this.BadRequest("GameId or alias can not be default or empty.");
            }

            await this.gameService.InvitePlayerToGameAsync(gameId, alias);

            return this.Ok();
        }

        /// <summary>
        /// Sets the chosen time.
        /// </summary>
        /// <param name="gameId">The game identifier.</param>
        /// <param name="meetingtimeId">The meetingtime identifier.</param>
        /// <returns>Action result.</returns>
        [HttpPost]
        [Route("{gameId}/meetingtimes/{meetingtimeId}/_setchosen")]
        [ProducesResponseType(400)]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(403)]
        public async Task<IActionResult> SetChosenTime([FromRoute] Guid gameId, [FromRoute] Guid meetingtimeId)
        {
            if (gameId == Guid.Empty || meetingtimeId == Guid.Empty)
            {
                return this.BadRequest("GameId or meetingtimeId can not be empty or null.");
            }

            await this.gameService.SetChosenTimeAsync(gameId, meetingtimeId, this.User.Identity.Name);

            return this.Ok();
        }

        /// <summary>
        /// Tries to add time of meet in game.
        /// </summary>
        /// <param name="gameId">Game identifier.</param>
        /// <param name="meetingTimeDto">Specialized time.</param>
        /// <returns>The action result.</returns>
        [HttpPost]
        [Route("{gameId}/meetingtimes")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(409)]
        public async Task<IActionResult> AddMeetTime([FromRoute] Guid gameId, [FromBody] MeetingTimeCreateDto meetingTimeDto)
        {
            if (gameId == Guid.Empty || meetingTimeDto == null)
            {
                return this.BadRequest("GameId or meetingTimeDto can not be empty or null.");
            }

            if (meetingTimeDto.TimeOfMeet.ToUniversalTime() < DateTimeOffset.UtcNow.Add(TimeSpan.FromHours(1)))
            {
                return this.BadRequest("MeetingTime should be more than UtcNow + 1 hour.");
            }

            await this.gameService.AddMeetingTimeAsync(meetingTimeDto.TimeOfMeet, gameId);

            return this.Ok();
        }

        /// <summary>
        /// Votes the game.
        /// </summary>
        /// <param name="gameId">The game identifier.</param>
        /// <param name="meetingtimeId">The meetingtime identifier.</param>
        /// <returns>Action result.</returns>
        [HttpPut]
        [ProducesResponseType(400)]
        [ProducesResponseType(200)]
        [ProducesResponseType(403)]
        [Route("{gameId}/meetingtimes/{meetingtimeId}/votes")]
        public async Task<IActionResult> VoteGame([FromRoute] Guid gameId, [FromRoute] Guid meetingtimeId)
        {
            if (gameId == Guid.Empty || meetingtimeId == Guid.Empty)
            {
                return this.BadRequest("GameId or meetingtimeId can not be empty or null.");
            }

            await this.gameService.AddVoteAsync(gameId, meetingtimeId, this.User.Identity.Name);

            return this.Ok();
        }

        /// <summary>
        /// Removes the vote.
        /// </summary>
        /// <param name="gameId">The game identifier.</param>
        /// <param name="meetingtimeId">The meetingtime identifier.</param>
        /// <returns>The action result.</returns>
        [HttpDelete]
        [ProducesResponseType(400)]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(403)]
        [Route("{gameId}/meetingtimes/{meetingtimeId}/votes")]
        public async Task<IActionResult> RemoveVote([FromRoute] Guid gameId, [FromRoute] Guid meetingtimeId)
        {
            if (gameId == Guid.Empty || meetingtimeId == Guid.Empty)
            {
                return this.BadRequest("GameId or meetingtimeId can not be empty or null.");
            }

            await this.gameService.RemoveVoteAsync(gameId, meetingtimeId, this.User.Identity.Name);

            return this.Ok();
        }
    }
}
