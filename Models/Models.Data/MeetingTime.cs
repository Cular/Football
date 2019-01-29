// <copyright file="MeetingTime.cs" company="Yarik Home">
// All rights maybe reserved. © Yarik
// </copyright>

namespace Models.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Text;

    /// <summary>
    /// The meeting time of game.
    /// </summary>
    [Table("meetingtimes")]
    public class MeetingTime : Entity<Guid>
    {
        /// <summary>
        /// Gets or sets game indentity.
        /// </summary>
        [Column("gameid")]
        public Guid GameId { get; set; }

        /// <summary>
        /// Gets or sets the game.
        /// </summary>
        public virtual Game Game { get; set; }

        /// <summary>
        /// Gets or sets the proposed meet time of game.
        /// </summary>
        [Column("timeofmeet")]
        public DateTimeOffset TimeOfMeet { get; set; }

        /// <summary>
        /// Gets or sets players votes in game.
        /// </summary>
        public virtual List<PlayerVote> PlayerVotes { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is choosen.
        /// </summary>
        [Column("ischoosen")]
        public bool IsChoosen { get; set; }
    }
}
