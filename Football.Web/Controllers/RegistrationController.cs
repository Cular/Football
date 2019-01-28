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
    /// <seealso cref="Microsoft.AspNetCore.Mvc.ControllerBase" />
    [Route("api/registration")]
    [ApiController]
    [AllowAnonymous]
    public class RegistrationController : ControllerBase
    {
        private readonly IPlayerRepository playerRepository;
        private readonly IMapper mapper;
        private readonly IRegisterNotifier notifier;
        private readonly IPlayerActivationRepository activationRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="RegistrationController"/> class.
        /// </summary>
        /// <param name="playerRepository">The player repository.</param>
        /// <param name="mapper">The mapper.</param>
        /// <param name="notifier">The notify service.</param>
        /// <param name="activationRepository">activation repository.</param>
        public RegistrationController(IPlayerRepository playerRepository, IMapper mapper, IRegisterNotifier notifier, IPlayerActivationRepository activationRepository)
        {
            this.playerRepository = playerRepository ?? throw new ArgumentNullException(nameof(playerRepository));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            this.notifier = notifier ?? throw new ArgumentNullException(nameof(notifier));
            this.activationRepository = activationRepository ?? throw new ArgumentNullException(nameof(activationRepository));
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
            // TODO: Change work with Facebook auth.
            var player = this.mapper.Map<Player>(dtoCreate);
            var result = await this.playerRepository.CreateAsync(player);

            // await this.notifier.SendRegisterInfo(result);
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
        [ProducesResponseType(404)]
        public async Task<IActionResult> Activate([FromRoute] string code)
        {
            var activation = await this.activationRepository.GetAsync(code);

            if (activation == null)
            {
                return this.NotFound();
            }

            activation.Player.Active = true;

            await this.playerRepository.UpdateAsync(activation.Player);
            await this.activationRepository.DeleteAsync(activation);

            return this.Ok();
        }
    }
}
