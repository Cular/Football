// <copyright file="GamesController.MeetingTime.cs" company="Yarik Home">
// All rights maybe reserved. © Yarik
// </copyright>

namespace Football.Web.Controllers
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Models.Data;
    using Models.Dto;

    /// <summary>
    /// Part of meeting time management of game.
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.ControllerBase" />
    public partial class GamesController
    {
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
