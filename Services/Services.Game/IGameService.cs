using Models.Data;
using Models.Data.GameState;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services.Game
{
    public interface IGameService
    {
        Task CreateAsync(Models.Data.Game game);

        Task<bool> TryDeleteGameAsync(Guid gameId, string playerId);

        Task<bool> TryChangeGameStateAsync(Guid gameId, string playerId, GameStateEnum gameStateEnum);

        Task<List<Models.Data.Game>> GetMyGamesAsync(string playerId);

        Task<List<Models.Data.Game>> GetAllGamesAsync(string playerId, int page, int count, GameStateEnum gameState);

        Task<Models.Data.Game> GetGameAsync(Guid gameId);

        Task InvitePlayerToGameAsync(Guid gameId, string playerId);

        Task AddMeetingTimeAsync(DateTimeOffset meetingtime, Guid gameId);

        Task AddVoteAsync(Guid gameId, Guid meetingtimeId, string playerId);

        Task RemoveVoteAsync(Guid gameId, Guid meetingtimeId, string playerId);

        Task SetChosenTimeAsync(Guid gameId, Guid meetingtimeId, string playerId);
    }
}
