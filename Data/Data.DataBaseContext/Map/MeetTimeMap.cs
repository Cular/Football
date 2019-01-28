using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.DataBaseContext.Map
{
    public class MeetTimeMap : IEntityTypeConfiguration<MeetingTime>
    {
        public void Configure(EntityTypeBuilder<MeetingTime> builder)
        {
            builder.HasKey(mt => mt.Id);
            builder.HasOne(mt => mt.Game).WithMany(g => g.MeetingTimes).HasForeignKey(mt => mt.GameId).HasPrincipalKey(g => g.Id).OnDelete(DeleteBehavior.Cascade).IsRequired();
        }
    }
}
