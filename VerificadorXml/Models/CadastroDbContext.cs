using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace VerificadorXml.Models
{
    public class CadastroDbContext : DbContext
    {
        public CadastroDbContext() { }

        private static IConfigurationRoot configuration = new ConfigurationBuilder()
           .AddJsonFile("registersettings.json", optional: false, reloadOnChange: true).Build();

        public CadastroDbContext(DbContextOptions<CadastroDbContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("CadastroDbConnection"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Cadastro> Cadastros { get; set; }
    }
}
