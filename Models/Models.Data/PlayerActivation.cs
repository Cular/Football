﻿// <copyright file="PlayerActivation.cs" company="Yarik Home">
// All rights maybe reserved. © Yarik
// </copyright>

namespace Models.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Text;

    /// <summary>
    /// The activation players email.
    /// </summary>
    [Table("playeractivations")]
    public class PlayerActivation : Entity<string>
    {
        /// <summary>
        /// Gets or sets the player identifier.
        /// </summary>
        /// <value>
        /// The player identifier.
        /// </value>
        [Required]
        [Column("playerid")]
        public string PlayerId { get; set; }

        /// <summary>
        /// Gets or sets the player.
        /// </summary>
        /// <value>
        /// The player.
        /// </value>
        public virtual Player Player { get; set; }
    }
}
