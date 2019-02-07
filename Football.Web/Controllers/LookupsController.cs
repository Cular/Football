// <copyright file="LookupsController.cs" company="Yarik Home">
// All rights maybe reserved. © Yarik
// </copyright>

namespace Football.Web.Controllers
{
    using System;
    using System.Linq;
    using Microsoft.AspNetCore.Mvc;
    using Models.Data.GameState;
    using Models.Dto;

    /// <summary>
    /// The lookup controller for enums.
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.ControllerBase" />
    [Route("api/lookups")]
    [ApiController]
    public class LookupsController : ControllerBase
    {
        /// <summary>
        /// Gets the friendship status.
        /// </summary>
        /// <returns>List of friendship statuses.</returns>
        [HttpGet]
        [Route("friendshipstatus")]
        public IActionResult GetFriendshipStatus()
        {
            return this.Ok(Enum.GetValues(typeof(FriendshipStatus)).Cast<FriendshipStatus>().Select(fs => new { Id = (int)fs, Name = fs.ToString() }));
        }

        /// <summary>
        /// Gets the friendship status.
        /// </summary>
        /// <returns>List of friendship statuses.</returns>
        [HttpGet]
        [Route("gamestates")]
        public IActionResult GetGameStates()
        {
            return this.Ok(Enum.GetValues(typeof(GameStateEnum)).Cast<GameStateEnum>().Select(fs => new { Id = (int)fs, Name = fs.ToString() }));
        }
    }
}
