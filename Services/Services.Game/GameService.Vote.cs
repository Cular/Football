using Football.Exceptions;
using Models.Data.GameState;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Services.Game
{
    public partial class GameService
    {
        public async Task AddVoteAsync(Guid gameId, Guid meetingtimeId, string playerId)
        {
            var game = await this.gameRepository.GetAsync(gameId) ?? throw new NotFoundException($"Game with Id {gameId} not exists.");

            game.AddVote(gameId, meetingtimeId, playerId);

            await this.gameRepository.UpdateAsync(game);
        }

        public async Task RemoveVoteAsync(Guid gameId, Guid meetingtimeId, string playerId)
        {
            var game = await this.gameRepository.GetAsync(gameId) ?? throw new NotFoundException($"Game with Id {gameId} not exists.");
                        
            if (game.TryRemoveVote(meetingtimeId, playerId))
            {
                await this.gameRepository.UpdateAsync(game);
            }
        }
    }
}
