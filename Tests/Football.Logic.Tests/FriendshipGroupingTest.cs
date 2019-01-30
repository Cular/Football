using Models.Data;
using Models.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Football.Logic.Tests
{
    public class FriendshipGroupingTest
    {
        [Fact]
        public void _ListOfFriendships_GroupedByPlayer()
        {

            //Arrange
            var max = 999;
            var players = Enumerable.Range(1, max).Select(i => new Player { Id = $"player{i}", Active = true, Email = $"test{i}@test.com", PasswordHash = "asdb" }).ToList();
            var forward = Enumerable.Range(1, max).Select(i => i == max 
                ? new Friendship { PlayerId = players[i-1].Id, FriendId = players[0].Id, Player = players[i-1], Friend = players[0], Id = Guid.NewGuid(), IsApproved = true }
                : new Friendship { PlayerId = players[i-1].Id, FriendId = players[i].Id, Player = players[i-1], Friend = players[i], Id = Guid.NewGuid(), IsApproved = true });

            var oposite = Enumerable.Range(1, max).Select(i => i == max
                ? new Friendship { PlayerId = players[0].Id, FriendId = players[i-1].Id, Player = players[0], Friend = players[i-1], Id = Guid.NewGuid(), IsApproved = true }
                : new Friendship { PlayerId = players[i].Id, FriendId = players[i-1].Id, Player = players[i], Friend = players[i-1], Id = Guid.NewGuid(), IsApproved = true });

            var friendships = Enumerable.Concat(forward, oposite).ToList();

            //Action
            var res = friendships.GroupBy(fs => new FriendKey { PlayerId = fs.PlayerId, FriendId = fs.FriendId }, new FriendshipComparer()).ToList();

            //Assert
            Assert.Equal(max, res.Count);

            foreach (var r in res)
            {
                Assert.Equal(2, r.Count());
                Assert.True(r.First().FriendId == r.Skip(1).First().PlayerId);
                Assert.True(r.First().PlayerId == r.Skip(1).First().FriendId);
            }
        }
    }

    public class FriendKey
    {
        public string PlayerId { get; set; }

        public string FriendId { get; set; }
    }

    public class FriendshipComparer : IEqualityComparer<FriendKey>
    {
        public bool Equals(FriendKey x, FriendKey y)
        {
            return x.PlayerId.Equals(y.PlayerId) && x.FriendId.Equals(y.FriendId) || x.PlayerId.Equals(y.FriendId) && x.FriendId.Equals(y.PlayerId);
        }

        public int GetHashCode(FriendKey obj)
        {
            return obj.PlayerId.GetHashCode() ^ obj.FriendId.GetHashCode();
        }
    }
}
