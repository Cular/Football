// <copyright file="GameCreateDto.cs" company="Yarik Home">
// All rights maybe reserved. © Yarik
// </copyright>

namespace Models.Dto
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// The game dto.
    /// </summary>
    public class GameCreateDto
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        [MinLength(4, ErrorMessage = "Minimal length equals 4")]
        [MaxLength(15, ErrorMessage = "Maximal length equals 15")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the information.
        /// </summary>
        /// <value>
        /// The information.
        /// </value>
        [MaxLength(100, ErrorMessage = "Maximal length equals 100")]
        public string Info { get; set; }
    }
}
