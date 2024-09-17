using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Datasource.Ef.Factories;

/// <summary>
/// repositories use, not needed to write create scope each time
/// </summary>
public static class IServiceScopeFactoryExtension {
    public static TDbContext GetDbContext<TDbContext>(this IServiceScopeFactory scopeFactory) where TDbContext : DbContext {
        var scope = scopeFactory.CreateAsyncScope();
        return scope.ServiceProvider.GetRequiredService<TDbContext>();
    }
}