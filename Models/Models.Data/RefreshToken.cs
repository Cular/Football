// <copyright file="RefreshToken.cs" company="Yarik Home">
// All rights maybe reserved. © Yarik
// </copyright>

namespace Models.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Text;

    /// <summary>
    /// The refresh token entity
    /// </summary>
    public class RefreshToken : Entity<string>
    {
        /// <summary>
        /// Gets or sets the user identifier.
        /// </summary>
        /// <value>
        /// The user identifier.
        /// </value>
        [Required]
        [Column("userId")]
        public string UserId { get; set; }

        /// <summary>
        /// Gets or sets the token.
        /// </summary>
        /// <value>
        /// The token.
        /// </value>
        [Required]
        [Column("token")]
        public string Token { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="RefreshToken"/> is active.
        /// </summary>
        /// <value>
        ///   <c>true</c> if active; otherwise, <c>false</c>.
        /// </value>
        [Required]
        [Column("active")]
        [DefaultValue(false)]
        public bool Active { get; set; }
    }
}
