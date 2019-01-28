using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.DataBaseContext.Map
{
    class PlayersGamesMap : IEntityTypeConfiguration<PlayerGame>
    {
        public void Configure(EntityTypeBuilder<PlayerGame> builder)
        {
            builder.HasKey(pg => new { pg.PlayerId, pg.GameId });
            builder.HasOne<Player>(pg => pg.Player).WithMany(p => p.PlayerGames).HasForeignKey(pg => pg.PlayerId);
            builder.HasOne<Game>(pg => pg.Game).WithMany(g => g.PlayerGames).HasForeignKey(pg => pg.GameId);
        }
    }
}
