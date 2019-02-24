// <copyright file="GameDto.cs" company="Yarik Home">
// All rights maybe reserved. © Yarik
// </copyright>

namespace Models.Dto
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Models.Data.GameState;

    /// <summary>
    /// The game dto.
    /// </summary>
    public class GameDto
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the players involved in game.
        /// </summary>
        public List<PlayerDto> Players { get; set; }

        /// <summary>
        /// Gets or sets the list of meeting time with users voting.
        /// </summary>
        public List<MeetingTimeDto> MeetingTimes { get; set; }

        /// <summary>
        /// Gets or sets the game state.
        /// </summary>
        public GameStateEnum GameState { get; set; }
    }
}
