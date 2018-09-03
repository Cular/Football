using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Data.DataBaseContext
{
    public class MigrationContextFactory : IDesignTimeDbContextFactory<FootballContext>
    {
        public FootballContext CreateDbContext(string[] args)
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            var opts = new DbContextOptionsBuilder();

            opts.UseSqlServer(config.GetConnectionString("DefaultConnection"));
            
            return new FootballContext(opts.Options);
        }
    }
}
