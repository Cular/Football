// <copyright file="Game.cs" company="Yarik Home">
// All rights maybe reserved. © Yarik
// </copyright>

namespace Models.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;
    using Models.Data.GameState;

    /// <summary>
    /// The event for players.
    /// </summary>
    [Table("games")]
    public class Game : Entity<Guid>
    {
        /// <summary>
        /// Gets or sets the admin identifier.
        /// </summary>
        /// <value>
        /// The admin identifier.
        /// </value>
        [Column("adminid")]
        public string AdminId { get; set; }

        /// <summary>
        /// Gets or sets the admin.
        /// </summary>
        /// <value>
        /// The admin.
        /// </value>
        public virtual Player Admin { get; set; }

        /// <summary>
        /// Gets or sets state of game.
        /// </summary>
        [Column("state")]
        public State State { get; set; } //TODO: workaround changing state.

        /// <summary>
        /// Gets or sets lift of players in game.
        /// </summary>
        public virtual List<PlayerGame> PlayerGames { get; set; }

        /// <summary>
        /// Gets or sets list of time variants.
        /// </summary>
        public virtual List<MeetingTime> MeetingTimes { get; set; }
    }
}
