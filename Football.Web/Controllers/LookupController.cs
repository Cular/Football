// <copyright file="LookupController.cs" company="Yarik Home">
// All rights maybe reserved. © Yarik
// </copyright>

namespace Football.Web.Controllers
{
    using System;
    using System.Linq;
    using Microsoft.AspNetCore.Mvc;
    using Models.Dto;

    /// <summary>
    /// The lookup controller for enums.
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.ControllerBase" />
    [Route("api/[controller]")]
    [ApiController]
    public class LookupController : ControllerBase
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
    }
}
