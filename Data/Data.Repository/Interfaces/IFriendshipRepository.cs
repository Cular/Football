// <copyright file="IFriendshipRepository.cs" company="Yarik Home">
// All rights maybe reserved. © Yarik
// </copyright>

namespace Data.Repository.Interfaces
{
    using System;
    using System.Threading.Tasks;
    using Football.Exceptions;
    using Models.Data;

    /// <summary>
    /// The friendship repository for managment players relationships.
    /// </summary>
    public interface IFriendshipRepository : IRepository<Friendship, Guid>
    {
        /// <summary>
        /// Determines whether the specified player identifier is exists.
        /// </summary>
        /// <param name="playerId">The player identifier.</param>
        /// <param name="friendId">The friend identifier.</param>
        /// <returns>true if exist both varients.</returns>
        Task<bool> IsExists(string playerId, string friendId);

        /// <summary>
        /// Creates the asynchronous.
        /// </summary>
        /// <param name="playerId">The player identifier.</param>
        /// <param name="friendId">The friend identifier.</param>
        /// <returns>void result</returns>
        Task CreateAsync(string playerId, string friendId);

        /// <summary>
        /// Deletes the asynchronous.
        /// </summary>
        /// <param name="playerId">The player identifier</param>
        /// <param name="friendId">The friend identifier</param>
        /// <returns>
        /// void result
        /// </returns>
        /// <exception cref="NotFoundException">Friendship with Id {friendshipId}</exception>
        Task DeleteAsync(string playerId, string friendId);

        /// <summary>
        /// Gets the asynchronous.
        /// </summary>
        /// <param name="playerId">The player identifier.</param>
        /// <param name="friendId">The friend identifier.</param>
        /// <returns>The friendship by playerId and friendId</returns>
        Task<Friendship> GetAsync(string playerId, string friendId);
    }
}
