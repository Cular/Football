using Data.Repository.Interfaces;
using Football.Core.Exceptions;
using System;
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

        public async Task ApproveFriendshipAsync(Guid friendshipId)
        {
            var friendship = await this.friendshipRepository.GetAsync(friendshipId) ?? throw new NotFoundException($"Friendship with Id {friendshipId} not exists.");
            if (!friendship.IsApproved)
            {
                friendship.IsApproved = true;
                await this.friendshipRepository.UpdateAsync(friendship);
            }
        }

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
    }
}
