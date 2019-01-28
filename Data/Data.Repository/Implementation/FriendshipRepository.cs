// <copyright file="FriendshipRepository.cs" company="Yarik Home">
// All rights maybe reserved. © Yarik
// </copyright>

namespace Data.Repository.Implementation
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Data.DataBaseContext;
    using Data.Repository.Interfaces;
    using Microsoft.EntityFrameworkCore;
    using Models.Data;

    /// <summary>
    /// The friendship repository for managment players relationships.
    /// </summary>
    public class FriendshipRepository : BaseRepository<Friendship, Guid>, IFriendshipRepository
    {
        private readonly DbSet<Friendship> friendships;

        /// <summary>
        /// Initializes a new instance of the <see cref="FriendshipRepository"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public FriendshipRepository(FootballContext context)
            : base(context)
        {
            this.friendships = context.Friendships;
        }
    }
}
