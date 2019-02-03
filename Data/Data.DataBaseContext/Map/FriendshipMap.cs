using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.DataBaseContext.Map
{
    public class FriendshipMap : IEntityTypeConfiguration<Friendship>
    {
        public void Configure(EntityTypeBuilder<Friendship> builder)
        {
            builder.HasKey(fs => fs.Id);
            builder.HasOne(fs => fs.Player).WithMany(/*p => p.Friendships*/).HasForeignKey(fs => fs.PlayerId).HasPrincipalKey(p => p.Id).IsRequired();
            builder.HasOne(fs => fs.Friend).WithMany(/*p => p.Friendships*/).HasForeignKey(fs => fs.FriendId).HasPrincipalKey(p => p.Id).OnDelete(DeleteBehavior.ClientSetNull).IsRequired(false);
        }
    }
}
