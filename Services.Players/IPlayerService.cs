using Models.Data;
using System;
using System.Collections.Generic;
using Football.Core.Exceptions;
using System.Threading.Tasks;

namespace Services.Players
{
    public interface IPlayerService
    {
        /// <summary>
        /// Requests the friendship asynchronous.
        /// </summary>
        /// <param name="playerid">The playerid.</param>
        /// <param name="friendid">The friendid.</param>
        /// <returns>void result</returns>
        /// <exception cref="NotFoundException">Requested friend with id: {friendid}</exception>
        Task RequestFriendshipAsync(string playerid, string friendid);

        /// <summary>
        /// Approves the friendship asynchronous.
        /// </summary>
        /// <param name="friendshipId">The friendship identifier.</param>
        /// <returns>void result</returns>
        /// <exception cref="NotFoundException">Friendship with Id {friendshipId}</exception>
        Task ApproveFriendshipAsync(string playerId, string friendId);

        /// <summary>
        /// Gets the friendships asynchronous.
        /// </summary>
        /// <param name="playerId">The player identifier.</param>
        /// <returns>List of friendships</returns>
        Task<List<Friendship>> GetFriendshipsAsync(string playerId);

        /// <summary>
        /// Removes the friendship asynchronous.
        /// </summary>
        /// <param name="friendshipId">The friendship identifier.</param>
        /// <returns>void result</returns>
        /// <exception cref="NotFoundException">Friendship with Id {friendshipId}</exception>
        Task RemoveFriendshipAsync(string playerId, string friendId);
    }
}
