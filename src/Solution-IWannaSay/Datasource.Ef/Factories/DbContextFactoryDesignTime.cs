using Datasource.Ef.Contexts;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Datasource.Ef.Factories;

/// <summary>
/// migration use
/// </summary>
public class DbContextFactoryDesignTime : IDesignTimeDbContextFactory<DbContextIWannaSay> {
    public DbContextIWannaSay CreateDbContext(string[] args) {
        var pathtoApi = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..", "..", "Client.Web"));

        var configuration = new ConfigurationBuilder()
            .SetBasePath(pathtoApi) // package: Microsoft.Extensions.Configuration.Json
            .AddJsonFile("appsettings.json")
            .Build();

        var connectionString = configuration.GetConnectionString("Docker");

        var optionsBuilder = new DbContextOptionsBuilder<DbContextIWannaSay>();
        optionsBuilder.UseSqlServer(connectionString);

        return new DbContextIWannaSay(optionsBuilder.Options);

    }
}