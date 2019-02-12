using Football.Chat.Models;
using Football.Chat.Models.Dal;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;

namespace Football.Chat.Repository.Extensions
{
    public static class ChatRepositorySetup
    {
        public static IServiceCollection AddChatRepository(this IServiceCollection services)
        {
            return services
                .AddSingleton<IMongoClient>(sp => new MongoClient(sp.GetRequiredService<IConfiguration>().GetConnectionString("MongodbConnection")))
                .AddSingleton(sp =>
                {
                    var clinet = sp.GetRequiredService<IMongoClient>();
                    var db = clinet.GetDatabase("footballChat", new MongoDatabaseSettings { GuidRepresentation = MongoDB.Bson.GuidRepresentation.Standard });

                    CreateIndex(db.GetCollection<Message>(ChatRepository.COLLECTION));

                    return db;
                })
                .AddScoped<IChatRepostitory, ChatRepository>();
        }

        private static void CreateIndex(IMongoCollection<Message> collection)
        {
            var game = new CreateIndexModel<Message>(Builders<Message>.IndexKeys.Ascending(m => m.GameId));

            collection.Indexes.CreateOne(game);
        }
    }
}
