// <copyright file="PlayerRepository.cs" company="Yarik Home">
// All rights maybe reserved. © Yarik
// </copyright>

namespace Data.Repository.Implementation
{
    using System.Threading.Tasks;
    using Data.DataBaseContext;
    using Data.Repository.Interfaces;
    using Microsoft.EntityFrameworkCore;
    using Models.Data;

    /// <summary>
    /// The players repository
    /// </summary>
    public class PlayerRepository : BaseRepository<Player, string>, IPlayerRepository
    {
        private readonly DbSet<Player> players;

        /// <summary>
        /// Initializes a new instance of the <see cref="PlayerRepository"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public PlayerRepository(FootballContext context)
            : base(context)
        {
            this.players = context.Players;
        }

        /// <summary>
        /// Gets the player by alias.
        /// </summary>
        /// <param name="alias">The alias.</param>
        /// <returns>
        /// The player.
        /// </returns>
        public async Task<Player> GetPlayerByAlias(string alias)
        {
            return await this.players.FirstOrDefaultAsync(p => p.Id == alias);
        }
    }
}
