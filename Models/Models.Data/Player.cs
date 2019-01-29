// <copyright file="Player.cs" company="Yarik Home">
// All rights maybe reserved. © Yarik
// </copyright>

namespace Models.Data
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    /// <summary>
    /// The user.
    /// </summary>
    [Table("players")]
    public class Player : Entity<string>
    {
        /// <summary>
        /// Gets or sets the password hash.
        /// </summary>
        /// <value>
        /// The password hash.
        /// </value>
        [Required]
        [Column("passwordhash")]
        public string PasswordHash { get; set; }

        /// <summary>
        /// Gets or sets the email player.
        /// </summary>
        [Required]
        [Column("email")]
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="Player"/> is active.
        /// </summary>
        /// <value>
        ///   <c>true</c> if active; otherwise, <c>false</c>.
        /// </value>
        [Column("active")]
        public bool Active { get; set; }

        /// <summary>
        /// Gets or sets the games of player
        /// </summary>
        public List<PlayerGame> PlayerGames { get; set; }
    }
}
