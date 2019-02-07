// <copyright file="RegistrationController.cs" company="Yarik Home">
// All rights maybe reserved. © Yarik
// </copyright>

namespace Football.Web.Controllers
{
    using System;
    using System.Threading.Tasks;
    using AutoMapper;
    using Data.Repository.Interfaces;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Models.Data;
    using Models.Dto;
    using Services.Registration;

    /// <summary>
    /// Registration controller.
    /// </summary>
    [Route("api/registration")]
    [ApiController]
    [AllowAnonymous]
    public class RegistrationController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IRegistrationService registrationService;

        /// <summary>
        /// Initializes a new instance of the <see cref="RegistrationController"/> class.
        /// </summary>
        /// <param name="mapper">The mapper.</param>
        /// <param name="registrationService">The registration service.</param>
        public RegistrationController(IMapper mapper, IRegistrationService registrationService)
        {
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            this.registrationService = registrationService ?? throw new ArgumentNullException(nameof(registrationService));
        }

        /// <summary>
        /// Registers the player.
        /// </summary>
        /// <param name="dtoCreate">The dto create.</param>
        /// <returns>OK</returns>
        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(409)]
        public async Task<IActionResult> RegisterPlayer([FromBody] PlayerDtoCreate dtoCreate)
        {
            var player = this.mapper.Map<Player>(dtoCreate);
            await this.registrationService.CreatePlayerAsync(player);
            return this.Ok();
        }

        /// <summary>
        /// Activates the specified code.
        /// </summary>
        /// <param name="code">The code.</param>
        /// <returns>Void result.</returns>
        [HttpPost]
        [Route("activate/{code}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Activate([FromRoute] string code)
        {
            if (string.IsNullOrEmpty(code))
            {
                return this.BadRequest("Code should not be null or empty.");
            }

            await this.registrationService.ActivateAccountAsync(code);

            return this.Ok();
        }
    }
}
