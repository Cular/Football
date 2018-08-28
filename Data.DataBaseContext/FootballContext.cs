using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Models.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.DataBaseContext
{
    public class FootballContext : DbContext, IFootballContext
    {
        private readonly string connectionString;

        public FootballContext()
        {
            Database.EnsureCreated();

            var configs = new ConfigurationBuilder()
                .AddJsonFile("dbsettings.json")
                .Build();

            this.connectionString = configs.GetConnectionString("DefaultConnection");
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(connectionString);

            base.OnConfiguring(optionsBuilder);
        }

        public DbSet<Game> Games { get; set; }
        public DbSet<Player> Players { get; set; }
    }
}
