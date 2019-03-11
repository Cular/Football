// <copyright file="PlayerGame.cs" company="Yarik Home">
// All rights maybe reserved. © Yarik
// </copyright>

namespace Models.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Text;

    /// <summary>
    /// The many to many relation binding.
    /// </summary>
    [Table("playersgames", Schema = "public")]
    public class PlayerGame
    {
        /// <summary>
        /// Gets or sets players identity.
        /// </summary>
        [Column("playerid")]
        public string PlayerId { get; set; }

        /// <summary>
        /// Gets or sets player
        /// </summary>
        public virtual Player Player { get; set; }

        /// <summary>
        /// Gets or sets game identity
        /// </summary>
        [Column("gameid")]
        public Guid GameId { get; set; }

        /// <summary>
        /// Gets or sets the game.
        /// </summary>
        public virtual Game Game { get; set; }
    }
}
