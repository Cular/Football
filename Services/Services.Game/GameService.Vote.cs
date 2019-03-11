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

            if (game.CanBeAddVote(meetingtimeId, playerId))
            {
                await this.gameRepository.AddVoteAsync(new Models.Data.PlayerVote { Id = Guid.NewGuid(), MeetingTimeId = meetingtimeId, PlayerId = playerId });
            }
        }

        public async Task RemoveVoteAsync(Guid gameId, Guid meetingtimeId, string playerId)
        {
            var game = await this.gameRepository.GetAsync(gameId) ?? throw new NotFoundException($"Game with Id {gameId} not exists.");
                        
            if (game.CanRemoveVote(meetingtimeId, playerId))
            {
                await this.gameRepository.RemoveVoteAsync(meetingtimeId, playerId);
            }
        }
    }
}
