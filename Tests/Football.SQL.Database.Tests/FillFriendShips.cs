////using Microsoft.EntityFrameworkCore;
////using Models.Data;
////using System;
////using System.ComponentModel.DataAnnotations.Schema;
////using System.Linq;
////using Xunit;

////namespace Football.SQL.Database.Tests
////{
////    public class FillFriendShips
////    {
////        private readonly TestDbContext dbContext;

////        public FillFriendShips()
////        {
////            dbContext = new TestDbContext();
////        }

////        [Fact]
////        public void FillFriendships()
////        {
////            //Arrange
////            var players = Enumerable.Range(1, 1000).Select(i => new Player { Id = $"player{i}", Active = true, Email = $"test{i}@test.com", PasswordHash = "asdb" }).ToList();

////            //Action
////            dbContext.AddRange(players);

////            for (int i = 0; i < players.Count; i++)
////            {
////                if (i == players.Count - 1)
////                    dbContext.Add(new Friendships { Player1 =  players[i].Id, Player2 = players[0].Id});
////                else
////                    dbContext.Add(new Friendships { Player1 = players[i].Id, Player2 = players[i + 1].Id });
////            }

////            dbContext.SaveChanges();

////            //Assert

////        }
////    }

////    [Table("friendships")]
////    public class Friendships
////    {
////        [Column("player1")]
////        public string Player1 { get; set; }

////        [Column("player2")]
////        public string Player2 { get; set; }
////    }

////    public class TestDbContext : DbContext
////    {
////        const string connectionString = "Data Source=.\\SQLEXPRESS;Database=Football;Integrated Security=True;MultipleActiveResultSets=True";

////        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
////        {
////            optionsBuilder.UseSqlServer(connectionString);
////        }

////        protected override void OnModelCreating(ModelBuilder modelBuilder)
////        {
////            modelBuilder.Entity<Player>().HasKey(p => p.Id);
////            modelBuilder.Entity<Player>().HasIndex(p => p.Email).IsUnique();

////            modelBuilder.Entity<Friendships>();
////            //modelBuilder.Entity<Friendships>().HasOne(f => f.Player1).WithOne().HasPrincipalKey<Player>(p => p.Id).IsRequired().OnDelete(DeleteBehavior.Cascade);
////            //modelBuilder.Entity<Friendships>().HasOne(f => f.Player2).WithOne().HasPrincipalKey<Player>(p => p.Id).IsRequired();
////        }
////    }
////}
