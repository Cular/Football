// <copyright file="PlayerVote.cs" company="Yarik Home">
// All rights maybe reserved. © Yarik
// </copyright>

namespace Models.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Text;

    /// <summary>
    /// The player vote in game on specified <see cref="MeetingTime"/>.
    /// </summary>
    [Table("playervotes")]
    public class PlayerVote : Entity<Guid>
    {
        [Column("meetingtimeid")]
        public Guid MeetingTimeId { get; set; }

        public virtual MeetingTime MeetingTime { get; set; }

        [Column("playerid")]
        public string PlayerId { get; set; }
    }
}
