// <copyright file="IMeetingTimeRepository.cs" company="Yarik Home">
// All rights maybe reserved. © Yarik
// </copyright>

namespace Data.Repository.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Models.Data;

    /// <summary>
    /// The meeting time repository interface.
    /// </summary>
    public interface IMeetingTimeRepository : IRepository<MeetingTime, Guid>
    {
    }
}
