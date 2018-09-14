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

    [Route("api/registration")]
    [ApiController]
    public class RegistrationController : ControllerBase
    {
        private readonly IPlayerRepository playerRepository;
        private readonly IMapper mapper;

        public RegistrationController(IPlayerRepository playerRepository, IMapper mapper)
        {
            this.playerRepository = playerRepository ?? throw new ArgumentNullException(nameof(playerRepository));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpPost]
        [ProducesResponseType(200)]
        public async Task<IActionResult> RegisterPlayer([FromBody] PlayerDtoCreate dtoCreate)
        {
            if (await this.playerRepository.IsExist(dtoCreate.Alias, dtoCreate.Email))
            {
                return this.Conflict("Email or alias is registered!");
            }

            var player = this.mapper.Map<Player>(dtoCreate);
            var result = await this.playerRepository.CreateAsync(player);

            return this.Ok();
        }
    }
}
