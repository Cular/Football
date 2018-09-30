// <copyright file="IdentityController.cs" company="Yarik Home">
// All rights maybe reserved. © Yarik
// </copyright>

namespace Football.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Data.Repository.Interfaces;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Models.Dto;
    using Models.Infrastructure;

    /// <summary>
    /// The token controller. For authenticate users.
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.ControllerBase" />
    [Route("token")]
    [AllowAnonymous]
    public class IdentityController : ControllerBase
    {
        private readonly IPlayerRepository playerRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="IdentityController"/> class.
        /// </summary>
        /// <param name="playerRepository">The player repository.</param>
        public IdentityController(IPlayerRepository playerRepository)
        {
            this.playerRepository = playerRepository;
        }

        /// <summary>
        /// Posts the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>Token model.</returns>
        [ProducesResponseType(400)]
        [ProducesResponseType(200, Type = typeof(TokenModel))]
        [ProducesResponseType(404)]
        [HttpPost("password")]
        public async Task<IActionResult> Post([FromBody] PasswordTypeModel model)
        {
            if (string.IsNullOrEmpty(model.Password) || string.IsNullOrEmpty(model.UserName))
            {
                return this.BadRequest("userName or password is empty!");
            }

            var player = await this.playerRepository.GetAsync(model.UserName);

            if (player == null)
            {
                return this.NotFound();
            }

            return this.Ok();
        }
    }
}
