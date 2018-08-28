// <copyright file="Player.cs" company="Yarik Home">
// All rights maybe reserved. © Yarik
// </copyright>

namespace Models.Data
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    /// <summary>
    /// The user.
    /// </summary>
    [Table("players")]
    public class Player : Entity<Guid>
    {
        /// <summary>
        /// Gets or sets the password hash.
        /// </summary>
        /// <value>
        /// The password hash.
        /// </value>
        [Column("passwordhash")]
        public string PasswordHash { get; set; }

        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        /// <value>
        /// The email.
        /// </value>
        [EmailAddress]
        [Column("email")]
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the alias player.
        /// </summary>
        [Column("alias")]
        public string Alias { get; set; }
    }
}
