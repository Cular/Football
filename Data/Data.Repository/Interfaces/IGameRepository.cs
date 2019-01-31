// <copyright file="IGameRepository.cs" company="Yarik Home">
// All rights maybe reserved. © Yarik
// </copyright>

namespace Data.Repository.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;
    using Models.Data;
    using Models.Data.GameState;

    /// <summary>
    /// The game repository.
    /// </summary>
    public interface IGameRepository : IRepository<Game, Guid>
    {
        /// <summary>
        /// Builds paged result for player by involved games.
        /// </summary>
        /// <param name="alias">The player id.</param>
        /// <param name="page">page number.</param>
        /// <param name="count">count number.</param>
        /// <param name="gameState">the game state.</param>
        /// <returns>List of games.</returns>
        Task<List<Game>> GetPagedAsync(string alias, int page, int count, GameStateEnum gameState);
    }
}
