using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace ImportaXml.Models
{
    public class XmlDbContext : DbContext
    {
        public XmlDbContext() { }

        private static IConfigurationRoot configuration = new ConfigurationBuilder()
            .AddJsonFile("filesettings.json", optional: false, reloadOnChange: true).Build();

        public XmlDbContext(DbContextOptions<XmlDbContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("XmlDbConnection"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<XmlFile> Files { get; set; }
        public DbSet<XmlFileAlgorithm> Algorithms { get; set; }
        public DbSet<XmlFileDet> Dets { get; set; }
        public DbSet<XmlFileNaoRegistrado> NRs { get; set; }
    }
}
