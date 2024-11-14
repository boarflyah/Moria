using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace DatabaseGenerator.Settings
{
    public class MoriaDataContextFactory : IDesignTimeDbContextFactory<MoriaDataContext>
    {
        public MoriaDataContext CreateDbContext(string[] args)
        {
            // Wczytaj konfigurację z appsettings.json
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            // Skonfiguruj DbContextOptionsBuilder z połączeniem do bazy danych
            var optionsBuilder = new DbContextOptionsBuilder<MoriaDataContext>();
            optionsBuilder.UseNpgsql(configuration.GetConnectionString("MoriaDatabase"));

            return new MoriaDataContext(optionsBuilder.Options);
        }
    }
}
