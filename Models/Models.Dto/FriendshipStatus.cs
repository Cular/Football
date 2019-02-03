// <copyright file="FriendshipStatus.cs" company="Yarik Home">
// All rights maybe reserved. © Yarik
// </copyright>

namespace Models.Dto
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    /// <summary>
    /// The friendship status enumeration.
    /// </summary>
    public enum FriendshipStatus
    {
        /// <summary>
        /// When player1 has sent request to add player2
        /// </summary>
        Requested = 0,

        /// <summary>
        /// When player2 get request and should approve or decline.
        /// </summary>
        Pending = 1,

        /// <summary>
        /// When player2 approve requested relationships.
        /// </summary>
        Approved = 2
    }
}
