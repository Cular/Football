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
            modelBuilder.ApplyConfiguration(new PlayerActivationMap());

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Game> Games { get; set; }
        public DbSet<Player> Players { get; set; }
        public DbSet<PlayerActivation> PlayerActivations { get; set; }
    }
}
