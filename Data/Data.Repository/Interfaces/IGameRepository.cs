// <copyright file="IGameRepository.cs" company="Yarik Home">
// All rights maybe reserved. © Yarik
// </copyright>

namespace Data.Repository.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Models.Data;

    /// <summary>
    /// The game repository.
    /// </summary>
    public interface IGameRepository : IRepository<Game, Guid>
    {
    }
}
