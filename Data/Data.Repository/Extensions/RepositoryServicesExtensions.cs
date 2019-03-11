// <copyright file="RepositoryServicesExtensions.cs" company="Yarik Home">
// All rights maybe reserved. © Yarik
// </copyright>

namespace Data.Repository.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Data.Repository.Implementation;
    using Data.Repository.Interfaces;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    /// <summary>
    /// Added to IServiceCollection implementations of repository interfaces
    /// </summary>
    public static class RepositoryServicesExtensions
    {
        /// <summary>
        /// Adds the repositories.
        /// </summary>
        /// <param name="services">The services.</param>
        /// <param name="configuration">The configuration.</param>
        /// <returns>Service collection</returns>
        public static IServiceCollection AddRepositories(this IServiceCollection services, IConfiguration configuration)
        {
            return services
                    .AddScoped<IFriendshipRepository, FriendshipRepository>(sp => new FriendshipRepository(configuration.GetConnectionString("SqlConnection")))
                    .AddScoped<IGameRepository, GameRepository>(sp => new GameRepository(configuration.GetConnectionString("SqlConnection")))
                    .AddScoped<IMeetingTimeRepository, MeetingTimeRepository>(sp => new MeetingTimeRepository(configuration.GetConnectionString("SqlConnection")))
                    .AddScoped<IPlayerActivationRepository, PlayerActivationRepository>(sp => new PlayerActivationRepository(configuration.GetConnectionString("SqlConnection")))
                    .AddScoped<IPlayerRepository, PlayerRepository>(sp => new PlayerRepository(configuration.GetConnectionString("SqlConnection")))
                    .AddScoped<IRefreshTokenRepository, RefreshTokenRepository>(sp => new RefreshTokenRepository(configuration.GetConnectionString("SqlConnection")));
        }
    }
}
