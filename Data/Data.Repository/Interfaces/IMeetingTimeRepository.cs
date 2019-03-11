// <copyright file="IMeetingTimeRepository.cs" company="Yarik Home">
// All rights maybe reserved. © Yarik
// </copyright>

namespace Data.Repository.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;
    using Models.Data;

    /// <summary>
    /// The meeting time repository interface.
    /// </summary>
    public interface IMeetingTimeRepository : IRepository<MeetingTime, Guid>
    {
        /// <summary>
        /// Gets list the by game identifier.
        /// </summary>
        /// <param name="gameId">The game identifier.</param>
        /// <returns>List of meeting time in game by game id.</returns>
        Task<List<MeetingTime>> GetByGameId(Guid gameId);
    }
}
