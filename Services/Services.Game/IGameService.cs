using Models.Data;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services.Game
{
    public interface IGameService
    {
        Task<Models.Data.Game> CreateAsync(Models.Data.Game game);

        Task<List<Models.Data.Game>> GetMyGamesAsync(string alias);

        Task<List<Models.Data.Game>> GetAllGamesAsync(string alias);
    }
}
