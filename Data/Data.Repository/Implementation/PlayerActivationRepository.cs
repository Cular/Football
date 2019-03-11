// <copyright file="PlayerActivationRepository.cs" company="Yarik Home">
// All rights maybe reserved. © Yarik
// </copyright>

namespace Data.Repository.Implementation
{
    using Data.DataBaseContext;
    using Data.Repository.Interfaces;
    using Microsoft.EntityFrameworkCore;
    using Models.Data;

    /// <summary>
    /// Activation repository.
    /// </summary>
    public class PlayerActivationRepository : BaseRepository<PlayerActivation, string>, IPlayerActivationRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PlayerActivationRepository"/> class.
        /// </summary>
        /// <param name="connectionString">The connectionString.</param>
        public PlayerActivationRepository(string connectionString)
            : base(connectionString)
        {
        }
    }
}
