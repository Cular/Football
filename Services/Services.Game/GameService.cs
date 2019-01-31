using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data.Repository.Interfaces;
using Football.Core.Exceptions;
using Models.Data;
using Models.Data.GameState;

namespace Services.Game
{
    public class GameService : IGameService
    {
        private readonly IGameRepository gameRepository;
        private readonly IPlayerRepository playerRepository;

        public GameService(IGameRepository gameRepository, IPlayerRepository playerRepository)
        {
            this.gameRepository = gameRepository;
            this.playerRepository = playerRepository;
        }

        public async Task AddMeetingTimeAsync(MeetingTime meetingTime)
        {
            var game = await this.gameRepository.GetAsync(meetingTime.GameId) ?? throw new NotFoundException($"Game with id:{meetingTime.GameId} not exists.");

            if(game.MeetingTimes.Any(mt => mt.TimeOfMeet == meetingTime.TimeOfMeet))
            {
                return;
            }

            game.MeetingTimes.Add(meetingTime);
            await gameRepository.UpdateAsync(game);
        }

        public Task<Models.Data.Game> CreateAsync(Models.Data.Game game)
        {
            return gameRepository.CreateAsync(game);
        }

        public Task<List<Models.Data.Game>> GetAllGamesAsync(string playerId, int page, int count, GameStateEnum gameState)
        {
            return gameRepository.GetPagedAsync(playerId, page, count, gameState);
        }

        public Task<List<Models.Data.Game>> GetMyGamesAsync(string playerId)
        {
            return gameRepository.GetAllAsync(g => g.AdminId == playerId);
        }

        public async Task<bool> TryInvitePlayerToGameAsync(Guid gameId, string playerId)
        {
            var game = await this.gameRepository.GetAsync(gameId) ?? throw new NotFoundException($"Game with id:{gameId} not exists.");

            if (game.PlayerGames.Any(pg => pg.PlayerId == playerId))
            {
                return false;
            }

            var player = await this.playerRepository.GetAsync(playerId) ?? throw new NotFoundException($"Player with id:{playerId} not exists.");
            game.PlayerGames.Add(new PlayerGame { GameId = gameId, PlayerId = playerId });

            await this.gameRepository.UpdateAsync(game);

            return true;
        }
    }
}
