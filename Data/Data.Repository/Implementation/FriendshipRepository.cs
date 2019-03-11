// <copyright file="FriendshipRepository.cs" company="Yarik Home">
// All rights maybe reserved. © Yarik
// </copyright>

namespace Data.Repository.Implementation
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Data.DataBaseContext;
    using Data.Repository.Interfaces;
    using Football.Exceptions;
    using Microsoft.EntityFrameworkCore;
    using Models.Data;

    /// <summary>
    /// The friendship repository for managment players relationships.
    /// </summary>
    public class FriendshipRepository : BaseRepository<Friendship, Guid>, IFriendshipRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FriendshipRepository"/> class.
        /// </summary>
        /// <param name="connectionString">The connectionString.</param>
        public FriendshipRepository(string connectionString)
            : base(connectionString)
        {
        }

        /// <summary>
        /// Creates two rows in relationship. First approved by requester and second waiting for approving.
        /// </summary>
        /// <param name="playerId">The player identifier.</param>
        /// <param name="friendId">The friend identifier.</param>
        /// <returns>
        /// void result
        /// </returns>
        public Task CreateAsync(string playerId, string friendId)
        {
            var friendship = new Friendship { Id = Guid.NewGuid(), PlayerId = playerId, FriendId = friendId, IsApproved = true };
            var oposite = new Friendship { Id = Guid.NewGuid(), PlayerId = friendId, FriendId = playerId, IsApproved = false };

            return this.CreateManyAsync(new List<Friendship> { friendship, oposite });
        }

        /// <summary>
        /// Deletes the asynchronous.
        /// </summary>
        /// <param name="friendshipId">The identifier of friendship.</param>
        /// <returns>
        /// void result
        /// </returns>
        /// <exception cref="NotFoundException">Friendship with Id {friendshipId}</exception>
        public override async Task DeleteAsync(Guid friendshipId)
        {
            var deleted = await this.GetAsync(friendshipId) ?? throw new NotFoundException($"Friendship with Id {friendshipId} not found.");
            var oposite = await this.GetAsync(deleted.FriendId, deleted.PlayerId);

            await Task.WhenAll(this.DeleteAsync(deleted), this.DeleteAsync(oposite));
        }

        /// <summary>
        /// Deletes the asynchronous.
        /// </summary>
        /// <param name="playerId">The player identifier</param>
        /// <param name="friendId">The friend identifier</param>
        /// <returns>
        /// void result
        /// </returns>
        /// <exception cref="NotFoundException">Friendship with playerId:{playerId} and friendId:{friendId}</exception>
        public async Task DeleteAsync(string playerId, string friendId)
        {
            var shouldDelete = await this.GetAllAsync($"WHERE (playerid = '{playerId}' AND friendid = '{friendId}') OR (playerid = '{friendId}' AND friendid = '{playerId}')");

            if (shouldDelete.Count < 2)
            {
                throw new NotFoundException($"Friendship with playerId:{playerId} and friendId:{friendId} not found.");
            }

            await Task.WhenAll(this.DeleteAsync(shouldDelete[0]), this.DeleteAsync(shouldDelete[1]));
        }

        /// <summary>
        /// Gets the asynchronous.
        /// </summary>
        /// <param name="playerId">The player identifier.</param>
        /// <param name="friendId">The friend identifier.</param>
        /// <returns>
        /// The friendship by playerId and friendId
        /// </returns>
        public async Task<Friendship> GetAsync(string playerId, string friendId)
        {
            return (await this.GetAsync<Friendship>($"WHERE playerid = @playerId AND friendId = @friendId", new { playerId, friendId })).FirstOrDefault();
        }

        /// <summary>
        /// Determines whether the specified player identifier is exists.
        /// </summary>
        /// <param name="playerId">The player identifier.</param>
        /// <param name="friendId">The friend identifier.</param>
        /// <returns>
        /// true if exist both varients.
        /// </returns>
        public async Task<bool> IsExists(string playerId, string friendId)
        {
            return await this.GetAsync(playerId, friendId) != null;
        }
    }
}
