// <copyright file="Game.Domain.cs" company="Yarik Home">
// All rights maybe reserved. © Yarik
// </copyright>

namespace Models.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Football.Exceptions;
    using Models.Data.GameState;

    /// <summary>
    /// The domain part.
    /// </summary>
    public partial class Game
    {
        /// <summary>
        /// Tries the add player.
        /// </summary>
        /// <param name="playerId">The player identifier.</param>
        /// <returns>Successnes of result.</returns>
        public bool TryAddPlayer(string playerId)
        {
            if (!this.PlayerGames.Any(pg => pg.PlayerId == playerId))
            {
                this.PlayerGames.Add(new PlayerGame { GameId = this.Id, PlayerId = playerId });
                return true;
            }

            return false;
        }

        /// <summary>
        /// Adds the vote.
        /// </summary>
        /// <param name="gameId">The game identifier.</param>
        /// <param name="meetingtimeId">The meetingtime identifier.</param>
        /// <param name="playerId">The player identifier.</param>
        /// <exception cref="ForbiddenException">
        /// Game with id {gameId} is in state {this.State.ToEnum()}
        /// or
        /// Player with alias {playerId} not invited to game with Id {gameId}
        /// </exception>
        /// <exception cref="NotFoundException">MeetingTime with Id {meetingtimeId}</exception>
        public void AddVote(Guid gameId, Guid meetingtimeId, string playerId)
        {
            if (!this.State.CanVote)
            {
                throw new ForbiddenException($"Game with id {gameId} is in state {this.State.ToEnum()}.");
            }

            if (!this.PlayerGames.Any(pg => pg.PlayerId == playerId))
            {
                throw new ForbiddenException($"Player with alias {playerId} not invited to game with Id {gameId}.");
            }

            var meetingtime = this.MeetingTimes.FirstOrDefault(mt => mt.Id == meetingtimeId) ?? throw new NotFoundException($"MeetingTime with Id {meetingtimeId} not exists.");

            if (meetingtime.PlayerVotes.Any(pv => pv.PlayerId == playerId))
            {
                return;
            }

            meetingtime.PlayerVotes.Add(new PlayerVote { Id = Guid.NewGuid(), MeetingTimeId = meetingtimeId, PlayerId = playerId });
        }

        /// <summary>
        /// Removes the vote.
        /// </summary>
        /// <param name="meetingtimeId">The meetingtime identifier.</param>
        /// <param name="playerId">The player identifier.</param>
        /// <returns>Successness result.</returns>
        /// <exception cref="ForbiddenException">Game with id {this.Id} is in state {this.State.ToEnum()}</exception>
        /// <exception cref="NotFoundException">MeetingTime with Id {meetingtimeId}</exception>
        public bool TryRemoveVote(Guid meetingtimeId, string playerId)
        {
            if (!this.State.CanVote)
            {
                throw new ForbiddenException($"Game with id {this.Id} is in state {this.State.ToEnum()}.");
            }

            var meetingtime = this.MeetingTimes.FirstOrDefault(mt => mt.Id == meetingtimeId) ?? throw new NotFoundException($"MeetingTime with Id {meetingtimeId} not exists.");
            var playerVote = meetingtime.PlayerVotes.FirstOrDefault(pv => pv.PlayerId == playerId);

            return playerVote == null ? false : meetingtime.PlayerVotes.Remove(playerVote);
        }

        /// <summary>
        /// Determines whether [is can be deleted] [the specified admin].
        /// </summary>
        /// <param name="admin">The admin.</param>
        /// <returns>
        ///   <c>true</c> if [is can be deleted] [the specified admin]; otherwise, <c>false</c>.
        /// </returns>
        /// <exception cref="ForbiddenException">Player with alias {admin}</exception>
        public bool IsCanBeDeleted(string admin)
        {
            if (this.AdminId != admin)
            {
                throw new ForbiddenException($"Player with alias {admin} does not admin in game.");
            }

            return this.State.CanDelete;
        }

        /// <summary>
        /// Tries to change game state.
        /// </summary>
        /// <param name="admin">The admin.</param>
        /// <param name="gameState">New state of game.</param>
        /// <returns>Successness of action.</returns>
        /// <exception cref="ForbiddenException">Player with alias {admin}</exception>
        public bool TryChangeGameState(string admin, GameStateEnum gameState)
        {
            if (this.AdminId != admin)
            {
                throw new ForbiddenException($"Player with alias {admin} does not admin in game.");
            }

            return this.State.TryChangeState(gameState.ToState(), this);
        }
    }
}
