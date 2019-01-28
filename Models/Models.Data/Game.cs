// <copyright file="Game.cs" company="Yarik Home">
// All rights maybe reserved. © Yarik
// </copyright>

namespace Models.Data
{
    using System;
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
        public State State { get; set; }
    }
}
