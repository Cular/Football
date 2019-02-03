// <copyright file="GamesController.Vote.cs" company="Yarik Home">
// All rights maybe reserved. © Yarik
// </copyright>

namespace Football.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// The part of vote management in game entity.
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.ControllerBase" />
    public partial class GamesController
    {
        /// <summary>
        /// Votes the game.
        /// </summary>
        /// <param name="gameId">The game identifier.</param>
        /// <param name="meetingtimeId">The meetingtime identifier.</param>
        /// <returns>Action result.</returns>
        [HttpPost]
        [ProducesResponseType(400)]
        [ProducesResponseType(200)]
        [ProducesResponseType(403)]
        [Route("{gameId}/meetingtimes/{meetingtimeId}/_vote")]
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
        [Route("{gameId}/meetingtimes/{meetingtimeId}/_vote")]
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
