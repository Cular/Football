// <copyright file="RefreshTokenRepository.cs" company="Yarik Home">
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

    /// <summary>
    /// The refresh token repository.
    /// </summary>
    public class RefreshTokenRepository : BaseRepository<RefreshToken, string>, IRefreshTokenRepository
    {
        private readonly DbSet<RefreshToken> tokens;

        /// <summary>
        /// Initializes a new instance of the <see cref="RefreshTokenRepository"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public RefreshTokenRepository(FootballContext context)
            : base(context)
        {
            this.tokens = context.RefreshTokens;
        }

        /// <summary>
        /// Revokes the users tokens asynchronous.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>
        /// void result.
        /// </returns>
        public Task RevokeUsersTokensAsync(string userId)
        {
            var utokens = this.tokens.Where(r => r.UserId == userId);

            return utokens.ForEachAsync(r => r.Active = false);
        }
    }
}
