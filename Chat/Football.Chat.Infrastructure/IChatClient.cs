using Football.Chat.Models;
using Football.Chat.Models.Dto;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Football.Chat.Infrastructure
{
    public interface IChatClient
    {
        Task ReceiveMessage(MessageDto message);
        Task FillChatOnLoad(object messages);
    }
}
