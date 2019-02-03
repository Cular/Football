// <copyright file="FriendDto.cs" company="Yarik Home">
// All rights maybe reserved. © Yarik
// </copyright>

namespace Models.Dto
{
    using System;

    /// <summary>
    /// The friend dto model.
    /// </summary>
    public class FriendDto
    {
        /// <summary>
        /// Gets or sets the players alias.
        /// </summary>
        /// <value>
        /// The alias.
        /// </value>
        public string Alias { get; set; }

        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        /// <value>
        /// The status.
        /// </value>
        public FriendshipStatus Status { get; set; }
    }
}
