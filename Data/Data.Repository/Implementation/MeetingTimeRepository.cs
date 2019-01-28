// <copyright file="MeetingTimeRepository.cs" company="Yarik Home">
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
    /// The meeting time repository.
    /// </summary>
    public class MeetingTimeRepository : BaseRepository<MeetingTime, Guid>, IMeetingTimeRepository
    {
        private readonly DbSet<MeetingTime> meetingTimes;

        /// <summary>
        /// Initializes a new instance of the <see cref="MeetingTimeRepository"/> class.
        /// </summary>
        /// <param name="context">Db context.</param>
        public MeetingTimeRepository(FootballContext context)
            : base(context)
        {
            this.meetingTimes = context.MeetingTimes;
        }
    }
}
