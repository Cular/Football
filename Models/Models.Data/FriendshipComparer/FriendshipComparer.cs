// <copyright file="FriendshipComparer.cs" company="Yarik Home">
// All rights maybe reserved. © Yarik
// </copyright>

namespace Models.Data.FriendshipComparer
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    /// <summary>
    /// The comparer for grouping <see cref="Friendship"/>.
    /// </summary>
    public class FriendshipComparer : IEqualityComparer<FriendKey>
    {
        /// <summary>
        /// Determines whether the specified objects are equal.
        /// </summary>
        /// <param name="x">The first object of type T to compare.</param>
        /// <param name="y">The second object of type T to compare.</param>
        /// <returns>
        /// true if the specified objects are equal; otherwise, false.
        /// </returns>
        public bool Equals(FriendKey x, FriendKey y)
        {
            return (x.PlayerId.Equals(y.PlayerId) && x.FriendId.Equals(y.FriendId)) || (x.PlayerId.Equals(y.FriendId) && x.FriendId.Equals(y.PlayerId));
        }

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <returns>
        /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table.
        /// </returns>
        public int GetHashCode(FriendKey obj)
        {
            return obj.PlayerId.GetHashCode() ^ obj.FriendId.GetHashCode();
        }
    }
}
