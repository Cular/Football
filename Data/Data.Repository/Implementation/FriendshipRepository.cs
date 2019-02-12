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
        /// <param name="context">The context.</param>
        public FriendshipRepository(FootballContext context)
            : base(context)
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
        public async Task CreateAsync(string playerId, string friendId)
        {
            var friendship = new Friendship { Id = Guid.NewGuid(), PlayerId = playerId, FriendId = friendId, IsApproved = true };
            var oposite = new Friendship { Id = Guid.NewGuid(), PlayerId = friendId, FriendId = playerId, IsApproved = false };

            using (var transaction = this.context.Database.BeginTransaction())
            {
                await this.context.Friendships.AddRangeAsync(friendship, oposite);
                await this.context.SaveChangesAsync();

                transaction.Commit();
            }
        }

        /// <summary>
        /// Deletes the asynchronous.
        /// </summary>
        /// <param name="friendshipId">The identifier of friendship.</param>
        /// <returns>
        /// void result
        /// </returns>
        /// <exception cref="NotFoundException">Friendship with Id {friendshipId}</exception>
        public new async Task DeleteAsync(Guid friendshipId)
        {
            var deleted = await this.context.Friendships.FirstOrDefaultAsync(fs => fs.Id == friendshipId) ?? throw new NotFoundException($"Friendship with Id {friendshipId} not found.");
            var oposite = this.context.Friendships.FirstOrDefaultAsync(fs => fs.PlayerId == deleted.FriendId && fs.FriendId == deleted.PlayerId);

            using (var transaction = this.context.Database.BeginTransaction())
            {
                this.context.Friendships.Remove(deleted);

                if (oposite.Result != null)
                {
                    this.context.Friendships.Remove(oposite.Result);
                }

                await this.context.SaveChangesAsync();

                transaction.Commit();
            }
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
            var shouldDelete = await this.context.Friendships.Where(fs => (fs.PlayerId == playerId && fs.FriendId == friendId) || (fs.PlayerId == friendId && fs.FriendId == playerId)).ToListAsync();
            if (shouldDelete.Count < 2)
            {
                throw new NotFoundException($"Friendship with playerId:{playerId} and friendId:{friendId} not found.");
            }

            this.context.Friendships.RemoveRange(shouldDelete);
            await this.context.SaveChangesAsync();
        }

        /// <summary>
        /// Gets the asynchronous.
        /// </summary>
        /// <param name="playerId">The player identifier.</param>
        /// <param name="friendId">The friend identifier.</param>
        /// <returns>
        /// The friendship by playerId and friendId
        /// </returns>
        public Task<Friendship> GetAsync(string playerId, string friendId)
        {
            return this.context.Friendships.FirstOrDefaultAsync(fs => fs.PlayerId == playerId && fs.FriendId == friendId);
        }

        /// <summary>
        /// Determines whether the specified player identifier is exists.
        /// </summary>
        /// <param name="playerId">The player identifier.</param>
        /// <param name="friendId">The friend identifier.</param>
        /// <returns>
        /// true if exist both varients.
        /// </returns>
        public Task<bool> IsExists(string playerId, string friendId)
        {
            return this.context.Friendships.AnyAsync(fs => fs.PlayerId == playerId && fs.FriendId == friendId);
        }
    }
}
