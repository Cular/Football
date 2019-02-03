// <copyright file="GameRepository.cs" company="Yarik Home">
// All rights maybe reserved. © Yarik
// </copyright>

namespace Data.Repository.Implementation
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Data.DataBaseContext;
    using Data.Repository.Interfaces;
    using Microsoft.EntityFrameworkCore;
    using Models.Data;
    using Models.Data.GameState;

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

        /// <inheritdoc/>
        public Task<List<Game>> GetPagedAsync(string alias, int page, int count, GameStateEnum gameState)
        {
            return this.context.Games
                .AsNoTracking()
                ////.Include(g => g.MeetingTimes)
                ////    .ThenInclude(mt => mt.PlayerVotes)
                ////.Include(g => g.PlayerGames)
                ////    .ThenInclude(pg => pg.Player)
                .Where(g => g.State.ToEnum() == gameState && g.PlayerGames.Any(pg => pg.PlayerId == alias))
                .Skip((page - 1) * count)
                .Take(count)
                .ToListAsync();
        }
    }
}
