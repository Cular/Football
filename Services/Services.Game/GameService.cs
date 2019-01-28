using System.Collections.Generic;
using System.Threading.Tasks;
using Data.Repository.Interfaces;
using Models.Data;

namespace Services.Game
{
    public class GameService : IGameService
    {
        private readonly IGameRepository gameRepository;

        public GameService(IGameRepository gameRepository)
        {
            this.gameRepository = gameRepository;
        }

        public Task<Models.Data.Game> CreateAsync(Models.Data.Game game)
        {
            return gameRepository.CreateAsync(game);
        }

        public Task<List<Models.Data.Game>> GetAllGamesAsync(string alias)
        {
            // TODO: implement Many to many and find.
            return gameRepository.GetAllAsync();
        }

        public Task<List<Models.Data.Game>> GetMyGamesAsync(string alias)
        {
            return gameRepository.GetAllAsync(g => g.AdminId == alias);
        }
    }
}
