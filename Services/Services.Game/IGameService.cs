using Models.Data;
using Models.Data.GameState;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services.Game
{
    public interface IGameService
    {
        Task<Models.Data.Game> CreateAsync(Models.Data.Game game);

        Task DeleteGameAsync(Guid gameId, string playerId);

        Task<List<Models.Data.Game>> GetMyGamesAsync(string playerId);

        Task<List<Models.Data.Game>> GetAllGamesAsync(string playerId, int page, int count, GameStateEnum gameState);

        Task<Models.Data.Game> GetGameAsync(Guid gameId);

        Task<bool> TryInvitePlayerToGameAsync(Guid gameId, string playerId);

        Task AddMeetingTimeAsync(MeetingTime meetingTime);

        Task AddVoteAsync(Guid gameId, Guid meetingtimeId, string playerId);

        Task RemoveVoteAsync(Guid gameId, Guid meetingtimeId, string playerId);

        Task SetChosenTimeAsync(Guid gameId, Guid meetingtimeId, string playerId);
    }
}
