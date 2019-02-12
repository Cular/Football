// <copyright file="ChatHub.cs" company="Yarik Home">
// All rights maybe reserved. © Yarik
// </copyright>

namespace Football.Web.Hubs
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using AutoMapper;
    using Football.Chat.Infrastructure;
    using Football.Chat.Models.Dal;
    using Football.Chat.Models.Dto;
    using Football.Chat.Repository;
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.SignalR;

    /// <summary>
    /// The hub for managing chat. For call server method from client and vise-versa.
    /// </summary>
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ChatHub : Hub<IChatClient>
    {
        private readonly IChatRepostitory chatRepostitory;
        private readonly IMapper mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="ChatHub"/> class.
        /// </summary>
        /// <param name="chatRepostitory">The chat repostitory.</param>
        /// <param name="mapper">The mapper.</param>
        public ChatHub(IChatRepostitory chatRepostitory, IMapper mapper)
        {
            this.chatRepostitory = chatRepostitory;
            this.mapper = mapper;
        }

        /// <summary>
        /// Joins the game chat.
        /// </summary>
        /// <param name="gameId">The game identifier.</param>
        /// <returns>Void result.</returns>
        public Task JoinGameChat(string gameId)
        {
            return this.Groups.AddToGroupAsync(this.Context.ConnectionId, gameId);
        }

        /// <summary>
        /// Leaves the game chat.
        /// </summary>
        /// <param name="gameId">The game identifier.</param>
        /// <returns>Void result.</returns>
        public Task LeaveGameChat(string gameId)
        {
            return this.Groups.RemoveFromGroupAsync(this.Context.ConnectionId, gameId);
        }

        /// <summary>
        /// Sends the message asynchronous.
        /// </summary>
        /// <param name="messageDto">The message dto.</param>
        /// <returns>Void result.</returns>
        public async Task SendMessage(MessageCreateDto messageDto)
        {
            var message = this.mapper.Map<Message>(messageDto);
            message.Id = Guid.NewGuid();
            message.PlayerId = this.Context.User.Identity.Name;
            message.Time = DateTimeOffset.UtcNow;

            await this.chatRepostitory.SaveMessageAsync(message);
            await this.Clients.Group(message.GameId.ToString()).ReceiveMessage(this.mapper.Map<MessageDto>(message));
        }

        /// <summary>
        /// Returns last <paramref name="count"/> messages from lastMessageId.
        /// </summary>
        /// <param name="gameId">Game identifier.</param>
        /// <param name="lastMessageId">Last message identifier. Can be null.</param>
        /// <param name="count">Count of message for a pagination.</param>
        /// <returns>List of messages in game chat.</returns>
        public async Task GetMessages(Guid gameId, Guid? lastMessageId, int count)
        {
            var messages = lastMessageId.HasValue
                ? await this.chatRepostitory.GetMessagesAsync(gameId, lastMessageId.Value, count)
                : await this.chatRepostitory.GetMessagesAsync(gameId, count);

            await this.Clients.Caller.FillChatOnLoad(this.mapper.Map<List<Message>, List<MessageDto>>(messages));
        }
    }
}
