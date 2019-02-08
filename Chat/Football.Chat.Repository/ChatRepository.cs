using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Football.Chat.Models;
using Football.Chat.Models.Dal;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace Football.Chat.Repository
{
    public class ChatRepository : IChatRepostitory
    {
        public const string COLLECTION = "Chat";
        private readonly IMongoCollection<Message> messagesCollection;

        public ChatRepository(IMongoDatabase mongoDatabase)
        {
            if (mongoDatabase == null)
            {
                throw new ArgumentNullException(nameof(mongoDatabase));
            }

            messagesCollection = mongoDatabase.GetCollection<Message>(COLLECTION);
        }

        //TODO: SHOUD BE TESTED WITH LOAD TESTING.
        public async Task<List<Message>> GetMessagesAsync(Guid gameId, Guid lastMessageId, int count)
        {
            var builder = Builders<Message>.Filter;
            var filter = builder.Eq(m => m.GameId, gameId);

            var result = new List<Message>();
            bool isFound = false;

            using (var cursor = await messagesCollection.FindAsync(filter))
            {
                //Перебираем пачками, чтобы не бегать в БД за каждым сообщением отдельно
                while (result.Count < count && await cursor.MoveNextAsync())
                {
                    var batch = cursor.Current;

                    if (!isFound)
                    {
                        var enumerator = batch.GetEnumerator();
                        while (enumerator.MoveNext())
                        {
                            //Ищем пока не найдем в коллекции интересующее сообщение.
                            if (enumerator.Current.Id == lastMessageId)
                            {
                                isFound = true;
                                //Пропускаем искомое сообщение и начинаем с следующего.
                                while (enumerator.MoveNext() && result.Count < count)
                                {
                                    result.Add(enumerator.Current);
                                }
                            }
                        }

                        continue;
                    }

                    result.AddRange(batch.Take(count - result.Count));
                }
            }

            return result;
        }

        public Task<List<Message>> GetMessagesAsync(Guid gameId, int count)
        {
            var builder = Builders<Message>.Filter;
            var filter = builder.Eq(m => m.GameId, gameId);

            return messagesCollection.AsQueryable().Where(m => m.GameId == gameId).Take(count).ToListAsync();
        }

        public Task RemoveMessagesAsync(Guid gameId)
        {
            return messagesCollection.DeleteManyAsync(m => m.GameId == gameId);
        }

        public Task SaveMessageAsync(Message message)
        {
            return messagesCollection.InsertOneAsync(message);
        }
    }
}
