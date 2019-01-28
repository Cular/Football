// <copyright file="IFriendshipRepository.cs" company="Yarik Home">
// All rights maybe reserved. © Yarik
// </copyright>

namespace Data.Repository.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Models.Data;

    /// <summary>
    /// The friendship repository for managment players relationships.
    /// </summary>
    public interface IFriendshipRepository : IRepository<Friendship, Guid>
    {
    }
}
