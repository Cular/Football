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
    using Football.Core.Extensions;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Models.Data;
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
        private readonly IRefreshTokenRepository refreshTokenRepository;
        private readonly TokenConfiguration configuration;

        /// <summary>
        /// Initializes a new instance of the <see cref="IdentityController"/> class.
        /// </summary>
        /// <param name="playerRepository">The player repository.</param>
        /// <param name="refreshTokenRepository">The refresh token repository.</param>
        /// <param name="configuration">Token configuration.</param>
        public IdentityController(IPlayerRepository playerRepository, IRefreshTokenRepository refreshTokenRepository, TokenConfiguration configuration)
        {
            this.playerRepository = playerRepository;
            this.refreshTokenRepository = refreshTokenRepository;
            this.configuration = configuration;
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
            var player = await this.playerRepository.GetAsync(model.UserName);

            if (player == null)
            {
                return this.NotFound();
            }

            if (!player.Active)
            {
                return this.BadRequest("Player with requested Id is inactive.");
            }

            if (!string.Equals(player.PasswordHash, PasswordHasher.GetPasswordHash(model.Password), StringComparison.InvariantCulture))
            {
                return this.BadRequest("userName or password is invalid!");
            }

            var token = JwtAuthorization.GenerateToken(player, this.configuration);
            var refresh = JwtAuthorization.GenerateRefreshToken(player, this.configuration);

            await this.refreshTokenRepository.RevokeUsersTokensAsync(player.Id);
            await this.refreshTokenRepository.CreateAsync(new RefreshToken
            {
                Id = refresh.refreshTokenKey,
                Active = true,
                Token = refresh.refreshTokenValue,
                UserId = player.Id
            });

            var result = new TokenModel
            {
                Token = token,
                RefreshToken = refresh.refreshTokenKey,
                Expiration = (int)this.configuration.Expiration.TotalSeconds
            };

            return this.Ok(result);
        }
    }
}
