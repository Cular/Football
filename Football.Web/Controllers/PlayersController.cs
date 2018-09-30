// <copyright file="PlayersController.cs" company="Yarik Home">
// All rights maybe reserved. © Yarik
// </copyright>

namespace Football.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Threading.Tasks;
    using AutoMapper;
    using Data.Repository.Interfaces;
    using Football.Web.Validation;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Models.Data;
    using Models.Dto;

    /// <summary>
    /// The players endpoint.
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.ControllerBase" />
    [Route("api/players")]
    [ApiController]
    [Authorize]
    public class PlayersController : ControllerBase
    {
        private readonly IPlayerRepository playerRepository;
        private readonly IMapper mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="PlayersController"/> class.
        /// </summary>
        /// <param name="playerRepository">The player repository.</param>
        /// <param name="mapper">The mapper.</param>
        /// <exception cref="ArgumentNullException">playerRepository</exception>
        public PlayersController(IPlayerRepository playerRepository, IMapper mapper)
        {
            this.playerRepository = playerRepository ?? throw new ArgumentNullException(nameof(playerRepository));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        /// <summary>
        /// Gets the player.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>The player or null.</returns>
        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(200, Type = typeof(PlayerDto))]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetPlayer([FromRoute]string id)
        {
            var result = await this.playerRepository.GetPlayerByAlias(id);
            if (result != null)
            {
                return this.Ok(this.mapper.Map<PlayerDto>(result));
            }

            return this.NotFound();
        }
    }
}
