using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Data.Repository.Interfaces;
using Football.Exceptions;
using Models.Data;
using Models.Data.GameState;

namespace Services.Game
{
    public partial class GameService : IGameService
    {
        private readonly IGameRepository gameRepository;
        private readonly IPlayerRepository playerRepository;

        public GameService(IGameRepository gameRepository, IPlayerRepository playerRepository)
        {
            this.gameRepository = gameRepository;
            this.playerRepository = playerRepository;
        }

        public Task<Models.Data.Game> CreateAsync(Models.Data.Game game)
        {
            return gameRepository.CreateAsync(game);
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
            return gameRepository.GetAllAsync(g => g.AdminId == playerId);
        }

        public async Task DeleteGameAsync(Guid gameId, string playerId)
        {
            var game = await this.gameRepository.GetAsync(gameId) ?? throw new NotFoundException($"Game with id {gameId} not exists.");

            if (game.AdminId != playerId)
            {
                throw new ForbiddenException($"Player with alias {playerId} does not admin in game.");
            }

            if (game.State.CanDelete)
            {
                await this.gameRepository.DeleteAsync(gameId);
            }
        }

        public async Task<bool> TryInvitePlayerToGameAsync(Guid gameId, string playerId)
        {
            var game = await this.gameRepository.GetAsync(gameId) ?? throw new NotFoundException($"Game with id {gameId} not exists.");
            
            if (game.TryAddPlayer(new PlayerGame { GameId = gameId, PlayerId = playerId }))
            {
                var player = await this.playerRepository.GetAsync(playerId) ?? throw new NotFoundException($"Player with id {playerId} not exists.");
                await this.gameRepository.UpdateAsync(game);
                return true;
            }

            return false;
        }
    }
}
