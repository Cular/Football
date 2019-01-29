// <copyright file="Friendship.cs" company="Yarik Home">
// All rights maybe reserved. © Yarik
// </copyright>

namespace Models.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Text;

    /// <summary>
    /// The entity that describes relation between players.
    /// </summary>
    [Table("friendships")]
    public class Friendship : Entity<Guid>
    {
        /// <summary>
        /// Gets or sets the player identifier.
        /// </summary>
        [Column("playerid")]
        public string PlayerId { get; set; }

        /// <summary>
        /// Gets or sets the player.
        /// </summary>
        public virtual Player Player { get; set; }

        /// <summary>
        /// Gets or sets the friend identifier.
        /// </summary>
        [Column("friendid")]
        public string FriendId { get; set; }

        /// <summary>
        /// Gets or sets the friend.
        /// </summary>
        public virtual Player Friend { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is aprooved.
        /// </summary>
        [Column("isapproved")]
        public bool IsApproved { get; set; }
    }
}
