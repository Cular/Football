using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Models.Data;
using Models.Data.GameState;

namespace Data.DataBaseContext.Map
{
    public class GameMap : IEntityTypeConfiguration<Game>
    {
        ValueConverter converter = new ValueConverter<State, GameStateEnum>(s => s.ToEnum(), e => e.ToState());

        public void Configure(EntityTypeBuilder<Game> builder)
        {
            builder.HasKey(g => g.Id);
            builder.HasOne(g => g.Admin).WithMany().HasForeignKey(g => g.AdminId).HasPrincipalKey(p => p.Id).OnDelete(DeleteBehavior.SetNull).IsRequired();
            builder.Property(g => g.State).HasConversion(converter).IsRequired();
            builder.Property(g => g.Name).IsRequired();
        }
    }
}
