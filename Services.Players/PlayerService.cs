using Data.Repository.Interfaces;
using Football.Core.Exceptions;
using Models.Data;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services.Players
{
    public class PlayerService : IPlayerService
    {
        private readonly IPlayerRepository playerRepository;
        private readonly IFriendshipRepository friendshipRepository;

        public PlayerService(IPlayerRepository playerRepository, IFriendshipRepository friendshipRepository)
        {
            this.playerRepository = playerRepository;
            this.friendshipRepository = friendshipRepository;
        }

        /// <summary>
        /// Approves the friendship asynchronous.
        /// </summary>
        /// <param name="playerId"></param>
        /// <param name="friendId"></param>
        /// <returns>
        /// void result
        /// </returns>
        /// <exception cref="NotFoundException">Friendship with Id {friendshipId}</exception>
        public async Task ApproveFriendshipAsync(string playerId, string friendId)
        {
            var friendship = await this.friendshipRepository.GetAsync(playerId, friendId) ?? throw new NotFoundException($"Friendship with playerId:{playerId} and friendId:{friendId} not exists.");
            if (!friendship.IsApproved)
            {
                friendship.IsApproved = true;
                await this.friendshipRepository.UpdateAsync(friendship);
            }
        }

        /// <summary>
        /// Requests the friendship asynchronous.
        /// </summary>
        /// <param name="playerid">The playerid.</param>
        /// <param name="friendid">The friendid.</param>
        /// <returns></returns>
        /// <exception cref="NotFoundException">Requested friend with id: {friendid}</exception>
        public async Task RequestFriendshipAsync(string playerid, string friendid)
        {
            if (await playerRepository.GetAsync(friendid) == null)
            {
                throw new NotFoundException($"Requested friend with id: {friendid} does not exists");
            }

            if (await this.friendshipRepository.IsExists(playerid, friendid))
            {
                return;
            }

            await this.friendshipRepository.CreateAsync(playerid, friendid);
        }

        /// <summary>
        /// Gets the friendships asynchronous.
        /// </summary>
        /// <param name="playerId">The player identifier.</param>
        /// <returns>
        /// List of friendships
        /// </returns>
        public Task<List<Friendship>> GetFriendshipsAsync(string playerId)
        {
            return this.friendshipRepository.GetAllAsync(fs => fs.PlayerId == playerId || fs.FriendId == playerId);
        }

        /// <summary>
        /// Removes the friendship asynchronous.
        /// </summary>
        /// <param name="playerId"></param>
        /// <param name="friendId"></param>
        /// <returns>
        /// void result
        /// </returns>
        public Task RemoveFriendshipAsync(string playerId, string friendId)
        {
            return friendshipRepository.DeleteAsync(playerId, friendId);
        }
    }
}
