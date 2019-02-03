using Data.DataBaseContext.Map;
using Microsoft.EntityFrameworkCore;
using Models.Data;

namespace Data.DataBaseContext
{
    public class FootballContext : DbContext
    {
        public FootballContext(DbContextOptions opts) : base(opts)
        {
            //Database.EnsureCreated();
            //Database.Migrate();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new PlayerMap());
            modelBuilder.ApplyConfiguration(new GameMap());
            modelBuilder.ApplyConfiguration(new PlayerActivationMap());
            modelBuilder.ApplyConfiguration(new RefreshTokenMap());
            modelBuilder.ApplyConfiguration(new PlayersGamesMap());
            modelBuilder.ApplyConfiguration(new MeetTimeMap());
            modelBuilder.ApplyConfiguration(new FriendshipMap());

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Game> Games { get; set; }
        public DbSet<Player> Players { get; set; }
        public DbSet<PlayerActivation> PlayerActivations { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }
        public DbSet<PlayerGame> PlayerGames { get; set; }
        public DbSet<MeetingTime> MeetingTimes { get; set; }
        public DbSet<Friendship> Friendships { get; set; }
    }
}
