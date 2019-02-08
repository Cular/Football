// <copyright file="ChatController.cs" company="Yarik Home">
// All rights maybe reserved. © Yarik
// </copyright>

namespace Football.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Football.Chat.Models;
    using Football.Chat.Repository;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;

    [Route("api/Chat")]
    [ApiController]
    public class ChatController : ControllerBase
    {
        private readonly IChatRepostitory chatRepository;

        public ChatController(IChatRepostitory chatRepostitory)
        {
            this.chatRepository = chatRepostitory;
        }

        [HttpGet]
        [Route("messages")]
        public async Task<IActionResult> GetMessages([FromQuery]Guid gameId, [FromQuery]Guid? lastMessageId, int count = 20)
        {
            var messages = lastMessageId.HasValue ? chatRepository.GetMessagesAsync(gameId, lastMessageId.Value, count) : chatRepository.GetMessagesAsync(gameId, count);

            return this.Ok(await messages);
        }

        [HttpPost]
        [Route("messages")]
        public async Task<IActionResult> CreateMessage([FromBody]Message message)
        {
            await chatRepository.SaveMessageAsync(message);

            return this.Ok();
        }


        [HttpDelete]
        [Route("messages")]
        public async Task<IActionResult> DeleteMessages([FromQuery]Guid gameId)
        {
            await this.chatRepository.RemoveMessagesAsync(gameId);
            return this.Ok();
        }
    }
}