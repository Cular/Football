// <copyright file="PlayersController.cs" company="Yarik Home">
// All rights maybe reserved. © Yarik
// </copyright>

namespace Football.Web.Controllers
{
    using System;
    using System.Threading.Tasks;
    using AutoMapper;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Services.Players;

    /// <summary>
    /// The players endpoint.
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.ControllerBase" />
    [Route("api/players")]
    [ApiController]
    [Authorize]
    public class PlayersController : ControllerBase
    {
        private readonly IPlayerService playerService;
        private readonly IMapper mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="PlayersController"/> class.
        /// </summary>
        /// <param name="playerService">The player repository.</param>
        /// <param name="mapper">The mapper.</param>
        /// <exception cref="ArgumentNullException">playerRepository</exception>
        public PlayersController(IPlayerService playerService, IMapper mapper)
        {
            this.playerService = playerService ?? throw new ArgumentNullException(nameof(playerService));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        /// <summary>
        /// Requests the friend.
        /// </summary>
        /// <param name="friendid">The friendid.</param>
        /// <returns>The action result.</returns>
        [HttpPut]
        [Route("friend/request/{friendid}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> RequestFriend([FromRoute] string friendid)
        {
            if (string.IsNullOrEmpty(friendid))
            {
                return this.BadRequest($"friendid shoud not be empty.");
            }

            await this.playerService.RequestFriendshipAsync(this.User.Identity.Name, friendid);

            return this.Ok();
        }

        [HttpPut]
        [Route("friend/approve/{friendshipId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> ApproveFriend([FromRoute]Guid friendshipId)
        {
            if (friendshipId == Guid.Empty)
            {
                return this.BadRequest($"friendshipId shoud not be empty.");
            }

            await this.playerService.
        }
    }
}
