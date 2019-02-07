// <copyright file="PlayerVote.cs" company="Yarik Home">
// All rights maybe reserved. © Yarik
// </copyright>

namespace Models.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Text;

    /// <summary>
    /// The player vote in game on specified <see cref="MeetingTime"/>.
    /// </summary>
    [Table("playervotes")]
    public class PlayerVote : Entity<Guid>
    {
        /// <summary>
        /// Gets or sets the meeting time identifier.
        /// </summary>
        /// <value>
        /// The meeting time identifier.
        /// </value>
        [Column("meetingtimeid")]
        public Guid MeetingTimeId { get; set; }

        /// <summary>
        /// Gets or sets the meeting time.
        /// </summary>
        /// <value>
        /// The meeting time.
        /// </value>
        public virtual MeetingTime MeetingTime { get; set; }

        /// <summary>
        /// Gets or sets the player identifier.
        /// </summary>
        /// <value>
        /// The player identifier.
        /// </value>
        [Column("playerid")]
        public string PlayerId { get; set; }
    }
}
