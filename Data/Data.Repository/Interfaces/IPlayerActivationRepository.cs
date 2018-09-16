// <copyright file="IPlayerActivationRepository.cs" company="Yarik Home">
// All rights maybe reserved. © Yarik
// </copyright>

namespace Data.Repository.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Models.Data;

    /// <summary>
    /// Activation repository
    /// </summary>
    public interface IPlayerActivationRepository : IRepository<PlayerActivation, string>
    {
    }
}
