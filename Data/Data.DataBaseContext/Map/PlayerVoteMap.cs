using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Models.Data;
using Models.Data.GameState;

namespace Data.DataBaseContext.Map
{
    public class PlayerVoteMap : IEntityTypeConfiguration<PlayerVote>
    {
        public void Configure(EntityTypeBuilder<PlayerVote> builder)
        {
            builder.HasKey(pv => pv.Id);
            builder.HasKey(pv => new { pv.MeetingTimeId, pv.PlayerId });
            builder.HasOne(pv => pv.MeetingTime).WithMany(mt => mt.PlayerVotes).HasPrincipalKey(mt => mt.Id).OnDelete(DeleteBehavior.Cascade).IsRequired();
        }
    }
}
