// <copyright file="IdentityController.cs" company="Yarik Home">
// All rights maybe reserved. © Yarik
// </copyright>

namespace Football.Web.Controllers
{
    using System;
    using System.Threading.Tasks;
    using Football.Core.Extensions;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Models.Data;
    using Models.Dto;
    using Services.Identity;

    /// <summary>
    /// The token controller. For authenticate users.
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.ControllerBase" />
    [Route("token")]
    [AllowAnonymous]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class IdentityController : ControllerBase
    {
        private readonly IIdentityService identity;

        /// <summary>
        /// Initializes a new instance of the <see cref="IdentityController"/> class.
        /// </summary>
        /// <param name="identity">The identity service.</param>
        public IdentityController(IIdentityService identity)
        {
            this.identity = identity;
        }

        /// <summary>
        /// Generates token by users form data.
        /// </summary>
        /// <returns>Token</returns>
        [HttpPost]
        public async Task<IActionResult> Post()
        {
            if (!this.HttpContext.Request.Form.TryGetValue("grant_type", out var grantType))
            {
                return this.BadRequest("Request does not contains grant_type parameter.");
            }

            if (grantType == "password" && this.HttpContext.Request.Form.TryGetValue("username", out var username)
                && this.HttpContext.Request.Form.TryGetValue("password", out var password))
            {
                var reponse = await this.identity.GetTokenAsync(username, password);
                return this.Ok(new
                {
                    access_token = reponse.access,
                    refreshToken = reponse.refresh,
                    expires_in = reponse.expiration
                });
            }

            if (grantType == "refreshtoken" && this.HttpContext.Request.Form.TryGetValue("refresh_token", out var refresh))
            {
                var reponse = await this.identity.GetTokenAsync(refresh);
                return this.Ok(new
                {
                    access_token = reponse.access,
                    refreshToken = reponse.refresh,
                    expires_in = reponse.expiration
                });
            }

            return this.BadRequest("Wrong grant_type parameter or user parameters for recognizing.");
        }
    }
}
