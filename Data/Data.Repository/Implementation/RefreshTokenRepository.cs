// <copyright file="RefreshTokenRepository.cs" company="Yarik Home">
// All rights maybe reserved. © Yarik
// </copyright>

namespace Data.Repository.Implementation
{
    using System.Linq;
    using System.Threading.Tasks;
    using Dapper;
    using Data.Repository.Interfaces;
    using Microsoft.EntityFrameworkCore;
    using Models.Data;

    /// <summary>
    /// The refresh token repository.
    /// </summary>
    public class RefreshTokenRepository : BaseRepository<RefreshToken, string>, IRefreshTokenRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RefreshTokenRepository"/> class.
        /// </summary>
        /// <param name="connectionString">The connectionString.</param>
        public RefreshTokenRepository(string connectionString)
            : base(connectionString)
        {
        }

        /// <summary>
        /// Revokes the users tokens asynchronous.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>
        /// void result.
        /// </returns>
        public async Task RevokeUsersTokensAsync(string userId)
        {
            var schema = GetSchema(typeof(RefreshToken));
            var query = $"UPDATE {schema.SchemaName}.{schema.TableName} " +
                $"SET active = false " +
                $"WHERE active = true AND userid = @userId";

            using (var connection = this.Connection)
            {
                connection.Open();
                var result = await connection.ExecuteAsync(query, new { userId });
                connection.Close();
            }
        }
    }
}
