// <copyright file="GameCreateDto.cs" company="Yarik Home">
// All rights maybe reserved. © Yarik
// </copyright>

namespace Models.Dto
{
    using System.Collections.Generic;

    /// <summary>
    /// The game dto.
    /// </summary>
    public class GameCreateDto
    {
        /// <summary>
        /// Gets or sets the meeting times variants.
        /// </summary>
        public List<MeetingTimeCreateDto> MeetingTimes { get; set; }
    }
}
