// <copyright file="MeetingTimeCreateDto.cs" company="Yarik Home">
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
    public class MeetingTimeCreateDto
    {
        /// <summary>
        /// Gets or sets meeting time.
        /// </summary>
        public DateTimeOffset TimeOfMeet { get; set; }
    }
}
