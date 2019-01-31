﻿// <copyright file="GamesController.cs" company="Yarik Home">
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
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class GamesController : ControllerBase
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
        [ProducesResponseType(200)]
        [HttpPost]
        public async Task<IActionResult> CreateGame([FromBody]GameCreateDto dto)
        {
            var entity = this.mapper.Map<Game>(dto);

            entity.AdminId = this.User.Identity.Name;
            entity.PlayerGames.Add(new PlayerGame { PlayerId = this.User.Identity.Name, GameId = entity.Id });

            await this.gameService.CreateAsync(entity);

            return this.Ok();
        }

        /// <summary>
        /// Find all created games by user.
        /// </summary>
        /// <returns>Array of games.</returns>
        [Route("my")]
        [HttpGet]
        [ProducesResponseType(200, Type=typeof(List<GameDto>))]
        public async Task<IActionResult> GetMyGames()
        {
            var result = await this.gameService.GetMyGamesAsync(this.User.Identity.Name);

            return this.Ok(result.Select(r => this.mapper.Map<GameDto>(r)));
        }

        /// <summary>
        /// Find all games where player is involved.
        /// </summary>
        /// <param name="page">page number.</param>
        /// <param name="count">count number.</param>
        /// <param name="gameState">state of game.</param>
        /// <returns>List of <see cref="GameDto"/></returns>
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(List<GameDto>))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetGames([FromQuery] int page = 1, [FromQuery] int count = 20, [FromQuery] GameStateEnum gameState = GameStateEnum.Public)
        {
            if (page < 1)
            {
                return this.BadRequest("Page number can not be less than 1.");
            }

            var result = await this.gameService.GetAllGamesAsync(this.User.Identity.Name, page, count, gameState);

            return this.Ok(result.Select(r => this.mapper.Map<GameDto>(r)));
        }

        /// <summary>
        /// Adding player by alias to game
        /// </summary>
        /// <param name="gameId">The game identifier.</param>
        /// <param name="alias">The player alias.</param>
        /// <returns>The action result.</returns>
        [HttpPut]
        [Route("{gameId}/invite")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> InvitePlayer([FromRoute] Guid gameId, [FromQuery] string alias)
        {
            if (gameId == Guid.Empty || string.IsNullOrEmpty(alias))
            {
                return this.BadRequest("GameId or alias can not be default or empty.");
            }

            if (await this.gameService.TryInvitePlayerToGameAsync(gameId, alias))
            {
                // ToDo: add push sending call.
            }

            return this.Ok();
        }

        /// <summary>
        /// Tries to add time of meet in game.
        /// </summary>
        /// <param name="gameId">Game identifier.</param>
        /// <param name="meetingTimeDto">Specialized time.</param>
        /// <returns>The action result.</returns>
        [HttpPost]
        [Route("{gameId}/meetingtime")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
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

            var meetingTime = this.mapper.Map<MeetingTime>(meetingTimeDto);
            meetingTime.GameId = gameId;
            meetingTime.Id = Guid.NewGuid();

            await this.gameService.AddMeetingTimeAsync(meetingTime);

            return this.Ok();
        }
    }
}