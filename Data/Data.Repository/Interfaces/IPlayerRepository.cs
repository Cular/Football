// <copyright file="IPlayerRepository.cs" company="Yarik Home">
// All rights maybe reserved. © Yarik
// </copyright>

namespace Data.Repository.Interfaces
{
    using System;
    using System.Threading.Tasks;
    using Models.Data;

    /// <summary>
    /// The player repository.
    /// </summary>
    public interface IPlayerRepository : IRepository<Player, string>
    {
        /// <summary>
        /// Gets the player by alias.
        /// </summary>
        /// <param name="alias">The alias.</param>
        /// <returns>The player.</returns>
        Task<Player> GetPlayerByAlias(string alias);

        /// <summary>
        /// Determines whether the specified alias is unique.
        /// </summary>
        /// <param name="alias">The alias.</param>
        /// <param name="email">The email.</param>
        /// <returns>Is unique alias and email.</returns>
        Task<bool> IsExist(string alias, string email);
    }
}
