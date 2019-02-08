using Football.Chat.Models.Dal;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Football.Chat.Repository
{
    public interface IChatRepostitory
    {
        Task<List<Message>> GetMessagesAsync(Guid gameId, Guid lastMessageId, int count);

        Task<List<Message>> GetMessagesAsync(Guid gameId, int count);

        Task SaveMessageAsync(Message message);

        Task RemoveMessagesAsync(Guid gameId);
    }
}
