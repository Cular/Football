// <copyright file="GameRepository.cs" company="Yarik Home">
// All rights maybe reserved. © Yarik
// </copyright>

namespace Data.Repository.Implementation
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Data.DataBaseContext;
    using Data.Repository.Interfaces;
    using Models.Data;

    /// <summary>
    /// The game repository.
    /// </summary>
    public class GameRepository : BaseRepository<Game, Guid>, IGameRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GameRepository"/> class.
        /// </summary>
        /// <param name="context">The db context.</param>
        public GameRepository(FootballContext context)
            : base(context)
        {
        }
    }
}
