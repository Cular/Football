// <copyright file="PlayersController.cs" company="Yarik Home">
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
    using Models.Data.FriendshipComparer;
    using Models.Dto;
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
        [Route("friends/{friendid}/request")]
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

        /// <summary>
        /// Approves the friend.
        /// </summary>
        /// <param name="friendId">The friend identifier.</param>
        /// <returns>The action result.</returns>
        [HttpPut]
        [Route("friends/{friendId}/approve")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> ApproveFriend([FromRoute]string friendId)
        {
            if (string.IsNullOrEmpty(friendId))
            {
                return this.BadRequest($"friendId shoud not be empty.");
            }

            await this.playerService.ApproveFriendshipAsync(this.User.Identity.Name, friendId);

            return this.Ok();
        }

        /// <summary>
        /// Removes the friend.
        /// </summary>
        /// <param name="friendId">The friend identifier.</param>
        /// <returns>The action result.</returns>
        [HttpDelete]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [Route("friends/{friendId}")]
        public async Task<IActionResult> RemoveFriend([FromRoute]string friendId)
        {
            if (string.IsNullOrEmpty(friendId))
            {
                return this.BadRequest($"friendId shoud not be empty.");
            }

            await this.playerService.RemoveFriendshipAsync(this.User.Identity.Name, friendId);

            return this.Ok();
        }

        /// <summary>
        /// Gets all friends.
        /// </summary>
        /// <returns>The list of friends with approved status.</returns>
        [HttpGet]
        [Route("friends")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<FriendDto>))]
        public async Task<IActionResult> GetAllFriends()
        {
            var friendships = await this.playerService.GetFriendshipsAsync(this.User.Identity.Name);

            var result = friendships
                .OrderBy(fs => fs.PlayerId)
                .GroupBy(fs => new FriendKey { PlayerId = fs.PlayerId, FriendId = fs.FriendId }, new FriendshipComparer())
                .Select(gfs =>
                {
                    var playerSide = gfs.First(fs => fs.PlayerId == this.User.Identity.Name);
                    var friendSide = gfs.First(fs => fs != playerSide);

                    if (playerSide.IsApproved)
                    {
                        return friendSide.IsApproved
                            ? new FriendDto { Alias = friendSide.PlayerId, Status = FriendshipStatus.Approved }
                            : new FriendDto { Alias = friendSide.PlayerId, Status = FriendshipStatus.Requested };
                    }

                    return new FriendDto { Alias = friendSide.PlayerId, Status = FriendshipStatus.Pending };
                });

            return this.Ok(result);
        }
    }
}
