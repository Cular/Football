// <copyright file="FriendKey.cs" company="Yarik Home">
// All rights maybe reserved. © Yarik
// </copyright>

namespace Models.Data.FriendshipComparer
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    /// <summary>
    /// The domain key for grouping friendships between players.
    /// </summary>
    public class FriendKey
    {
        /// <summary>
        /// Gets or sets the player identifier.
        /// </summary>
        /// <value>
        /// The player identifier.
        /// </value>
        public string PlayerId { get; set; }

        /// <summary>
        /// Gets or sets the friend identifier.
        /// </summary>
        /// <value>
        /// The friend identifier.
        /// </value>
        public string FriendId { get; set; }
    }
}
