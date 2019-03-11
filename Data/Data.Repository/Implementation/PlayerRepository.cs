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
        /// <summary>
        /// Initializes a new instance of the <see cref="PlayerRepository"/> class.
        /// </summary>
        /// <param name="connectionString">The context.</param>
        public PlayerRepository(string connectionString)
            : base(connectionString)
        {
        }
    }
}
