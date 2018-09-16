// <copyright file="RegistrationController.cs" company="Yarik Home">
// All rights maybe reserved. © Yarik
// </copyright>

namespace Football.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using AutoMapper;
    using Data.Repository.Interfaces;
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
    public class RegistrationController : ControllerBase
    {
        private readonly IPlayerRepository playerRepository;
        private readonly IMapper mapper;
        private readonly IRegisterNotifier notifier;

        /// <summary>
        /// Initializes a new instance of the <see cref="RegistrationController"/> class.
        /// </summary>
        /// <param name="playerRepository">The player repository.</param>
        /// <param name="mapper">The mapper.</param>
        /// <param name="notifier">The notify service.</param>
        public RegistrationController(IPlayerRepository playerRepository, IMapper mapper, IRegisterNotifier notifier)
        {
            this.playerRepository = playerRepository ?? throw new ArgumentNullException(nameof(playerRepository));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            this.notifier = notifier ?? throw new ArgumentNullException(nameof(notifier));
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
            if (await this.playerRepository.IsExist(dtoCreate.Alias, dtoCreate.Email))
            {
                return this.Conflict("Email or alias is registered!");
            }

            var player = this.mapper.Map<Player>(dtoCreate);
            var result = await this.playerRepository.CreateAsync(player);

            await this.notifier.SendRegisterInfo(result);

            return this.Ok();
        }
    }
}
