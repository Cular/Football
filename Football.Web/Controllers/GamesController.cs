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
    using Microsoft.AspNetCore.Http;
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
    }
}