using Football.Core.Exceptions;
using Models.Data;
using Models.Data.GameState;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Game
{
    public partial class GameService
    {
        public async Task AddVoteAsync(Guid gameId, Guid meetingtimeId, string playerId)
        {
            var game = await this.gameRepository.GetAsync(gameId) ?? throw new NotFoundException($"Game with Id {gameId} not exists.");

            if (!game.State.CanVote)
            {
                throw new ForbiddenException($"Game with id {gameId} is in state {game.State.ToEnum()}.");
            }

            if (!game.PlayerGames.Any(pg => pg.PlayerId == playerId))
            {
                throw new ForbiddenException($"Player with alias {playerId} not invited to game with Id {gameId}.");
            }

            var meetingtime = game.MeetingTimes.FirstOrDefault(mt => mt.Id == meetingtimeId) ?? throw new NotFoundException($"MeetingTime with Id {meetingtimeId} not exists.");

            if (meetingtime.PlayerVotes.Any(pv => pv.PlayerId == playerId))
            {
                return;
            }

            meetingtime.PlayerVotes.Add(new PlayerVote { Id = Guid.NewGuid(), MeetingTimeId = meetingtimeId, PlayerId = playerId });

            await this.gameRepository.UpdateAsync(game);
        }

        public async Task RemoveVoteAsync(Guid gameId, Guid meetingtimeId, string playerId)
        {
            var game = await this.gameRepository.GetAsync(gameId) ?? throw new NotFoundException($"Game with Id {gameId} not exists.");

            if (!game.State.CanVote)
            {
                throw new ForbiddenException($"Game with id {gameId} is in state {game.State.ToEnum()}.");
            }

            var meetingtime = game.MeetingTimes.FirstOrDefault(mt => mt.Id == meetingtimeId) ?? throw new NotFoundException($"MeetingTime with Id {meetingtimeId} not exists.");
            var playerVote = meetingtime.PlayerVotes.FirstOrDefault(pv => pv.PlayerId == playerId);

            if (playerVote != null)
            {
                meetingtime.PlayerVotes.Remove(playerVote);
                await this.gameRepository.UpdateAsync(game);
            }
        }
    }
}
