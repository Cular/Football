using Data.DataBaseContext.Map;
using Microsoft.EntityFrameworkCore;
using Models.Data;

namespace Data.DataBaseContext
{
    public class FootballContext : DbContext
    {
        public FootballContext(DbContextOptions opts) : base(opts)
        {
            Database.Migrate();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new PlayerMap());
            modelBuilder.ApplyConfiguration(new GameMap());

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Game> Games { get; set; }
        public DbSet<Player> Players { get; set; }
    }
}
