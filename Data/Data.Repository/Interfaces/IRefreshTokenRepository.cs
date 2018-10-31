// <copyright file="IRefreshTokenRepository.cs" company="Yarik Home">
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
    /// The refresh token repository,
    /// </summary>
    public interface IRefreshTokenRepository : IRepository<RefreshToken, string>
    {
        /// <summary>
        /// Revokes the users tokens asynchronous.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>void result.</returns>
        Task RevokeUsersTokensAsync(string userId);
    }
}
