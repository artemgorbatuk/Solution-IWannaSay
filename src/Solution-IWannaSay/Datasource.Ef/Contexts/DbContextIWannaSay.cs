using Datasource.Ef.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Datasource.Ef.Contexts;
public class DbContextIWannaSay : DbContext {

    public DbContextIWannaSay(DbContextOptions<DbContextIWannaSay> options) : base(options) { }

    public virtual DbSet<User> Users { get; set; }
    public virtual DbSet<Message> Messages { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder options) {
        options.EnableSensitiveDataLogging(false);
        //options.UseLazyLoadingProxies(false);
        //options.UseChangeTrackingProxies(false);
        //options.UseInMemoryDatabase(Guid.NewGuid().ToString());
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder) {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        //modelBuilder.HasDefaultSchema("dto");
        base.OnModelCreating(modelBuilder);
    }
}