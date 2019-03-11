// <copyright file="IGameRepository.cs" company="Yarik Home">
// All rights maybe reserved. © Yarik
// </copyright>

namespace Data.Repository.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;
    using Models.Data;
    using Models.Data.GameState;

    /// <summary>
    /// The game repository.
    /// </summary>
    public interface IGameRepository : IRepository<Game, Guid>
    {
        /// <summary>
        /// Builds paged result for player by involved games.
        /// </summary>
        /// <param name="alias">The player id.</param>
        /// <param name="page">page number.</param>
        /// <param name="count">count number.</param>
        /// <param name="gameState">the game state.</param>
        /// <returns>List of games.</returns>
        Task<List<Game>> GetPagedAsync(string alias, int page, int count, GameStateEnum gameState);

        /// <summary>
        /// Adds the player asynchronous.
        /// </summary>
        /// <param name="playerGame">The player game.</param>
        /// <returns>Void result.</returns>
        Task AddPlayerAsync(PlayerGame playerGame);

        /// <summary>
        /// Gets the admin identifier.
        /// </summary>
        /// <param name="gameId">The game identifier.</param>
        /// <returns>Admins id.</returns>
        Task<string> GetAdminId(Guid gameId);

        /// <summary>
        /// Adds the vote asynchronous.
        /// </summary>
        /// <param name="vote">The vote.</param>
        /// <returns></returns>
        Task AddVoteAsync(PlayerVote vote);

        /// <summary>
        /// Removes the vote asynchronous.
        /// </summary>
        /// <param name="meetingTime">The meeting time.</param>
        /// <param name="playerId">The player identifier.</param>
        /// <returns></returns>
        Task RemoveVoteAsync(Guid meetingTime, string playerId);
    }
}
