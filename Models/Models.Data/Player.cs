// <copyright file="Player.cs" company="Yarik Home">
// All rights maybe reserved. © Yarik
// </copyright>

namespace Models.Data
{
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
    }
}
