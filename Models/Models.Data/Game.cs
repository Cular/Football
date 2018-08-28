// <copyright file="Game.cs" company="Yarik Home">
// All rights maybe reserved. © Yarik
// </copyright>

namespace Models.Data
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    /// <summary>
    /// The event for players.
    /// </summary>
    public class Game : Entity<Guid>
    {
        /// <summary>
        /// Gets or sets the admin.
        /// </summary>
        /// <value>
        /// The admin.
        /// </value>
        public Player Admin { get; set; }
    }
}
