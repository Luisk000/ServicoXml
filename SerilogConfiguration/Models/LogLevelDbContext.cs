using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace SerilogConfiguration.Models
{
    public class LogLevelDbContext : DbContext
    {
        public LogLevelDbContext() { }

        private static IConfigurationRoot configuration = new ConfigurationBuilder()
           .AddJsonFile("DbSettings.json", optional: false, reloadOnChange: true).Build();

        public LogLevelDbContext(DbContextOptions<LogLevelDbContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("LogLevelConnection"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<LogLevel> Levels { get; set; }
    }
}
