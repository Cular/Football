using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data.Repository.Interfaces;
using Football.Chat.Repository;
using Football.Exceptions;
using Models.Data;
using Models.Data.GameState;
using Services.Notification.Interfaces;

namespace Services.Game
{
    public partial class GameService : IGameService
    {
        private readonly IGameRepository gameRepository;
        private readonly IPlayerRepository playerRepository;
        private readonly IChatRepostitory chatRepository;
        private readonly IPushNotificationService pushNotificationService;
        private readonly IMeetingTimeRepository meetingTimeRepository;

        public GameService(IGameRepository gameRepository, 
            IPlayerRepository playerRepository, 
            IChatRepostitory chatRepository, 
            IPushNotificationService pushNotificationService,
            IMeetingTimeRepository meetingTimeRepository)
        {
            this.gameRepository = gameRepository;
            this.playerRepository = playerRepository;
            this.chatRepository = chatRepository;
            this.pushNotificationService = pushNotificationService;
            this.meetingTimeRepository = meetingTimeRepository;
        }

        public async Task CreateAsync(Models.Data.Game game)
        {
            await gameRepository.CreateAsync(game);
            await gameRepository.AddPlayerAsync(game.PlayerGames[0]);
        }

        public Task<List<Models.Data.Game>> GetAllGamesAsync(string playerId, int page, int count, GameStateEnum gameState)
        {
            return gameRepository.GetPagedAsync(playerId, page, count, gameState);
        }

        public Task<Models.Data.Game> GetGameAsync(Guid gameId)
        {
            return gameRepository.GetAsync(gameId);
        }

        public Task<List<Models.Data.Game>> GetMyGamesAsync(string playerId)
        {
            return gameRepository.GetAllAsync($"WHERE adminid = '{playerId}'");
        }

        public async Task<bool> TryDeleteGameAsync(Guid gameId, string playerId)
        {
            var game = await this.gameRepository.GetAsync(gameId) ?? throw new NotFoundException($"Game with id {gameId} not exists.");

            if (game.IsCanBeDeleted(playerId))
            {
                await Task.WhenAll(this.gameRepository.DeleteAsync(gameId), this.chatRepository.RemoveMessagesAsync(gameId));
                return true;
            }

            return false;
        }

        public async Task InvitePlayerToGameAsync(Guid gameId, string playerId)
        {
            var game = await this.gameRepository.GetAsync(gameId) ?? throw new NotFoundException($"Game with id {gameId} not exists.");
            var player = await this.playerRepository.GetAsync(playerId) ?? throw new NotFoundException($"Player with id {playerId} not exists.");

            if (game.CanAddPlayer(playerId))
            {
                await this.gameRepository.AddPlayerAsync(new PlayerGame { GameId = gameId, PlayerId = playerId });
            }
        }

        public async Task<bool> TryChangeGameStateAsync(Guid gameId, string playerId, GameStateEnum gameStateEnum)
        {
            var game = await this.gameRepository.GetAsync(gameId) ?? throw new NotFoundException($"Game with id {gameId} not exists.");

            if (game.TryChangeGameState(playerId, gameStateEnum))
            {
                await this.gameRepository.UpdateAsync(game);
                return true;
            }

            return false;
        }
    }
}
