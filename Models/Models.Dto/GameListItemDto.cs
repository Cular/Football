// <copyright file="GameListItemDto.cs" company="Yarik Home">
// All rights maybe reserved. © Yarik
// </copyright>

namespace Models.Dto
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Models.Data.GameState;

    /// <summary>
    /// Game list item dto.
    /// </summary>
    public class GameListItemDto
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the game state.
        /// </summary>
        public GameStateEnum GameState { get; set; }
    }
}
