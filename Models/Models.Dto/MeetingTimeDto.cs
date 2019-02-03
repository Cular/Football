// <copyright file="MeetingTimeDto.cs" company="Yarik Home">
// All rights maybe reserved. © Yarik
// </copyright>

namespace Models.Dto
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    /// <summary>
    /// The meeting time dto.
    /// </summary>
    public class MeetingTimeDto
    {
        /// <summary>
        /// Gets or sets meeting identifier.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets meeting time.
        /// </summary>
        public DateTimeOffset TimeOfMeet { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether is meeting choosen.
        /// </summary>
        public bool IsChosen { get; set; }

        /// <summary>
        /// Gets or sets the players.
        /// </summary>
        public List<string> Players { get; set; }
    }
}
