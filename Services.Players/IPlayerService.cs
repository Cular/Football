using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Services.Players
{
    public interface IPlayerService
    {
        Task RequestFriendshipAsync(string playerid, string friendid);
        Task ApproveFriendshipAsync(Guid friendshipId);
    }
}
