// <copyright file="IPlayerRepository.cs" company="Yarik Home">
// All rights maybe reserved. © Yarik
// </copyright>

namespace Data.Repository.Interfaces
{
    using System;
    using Models.Data;

    /// <summary>
    /// The player repository.
    /// </summary>
    public interface IPlayerRepository : IRepository<Player, Guid>
    {
    }
}
