using Microsoft.Extensions.DependencyInjection;

namespace Datasource.Ef.Factories;

/// <summary>
/// repositories use
/// </summary>
public class DbContextFactoryScope : IServiceScopeFactory {

    private readonly IServiceScopeFactory _serviceScopeFactory;

    public DbContextFactoryScope(IServiceScopeFactory serviceScopeFactory) {
        _serviceScopeFactory = serviceScopeFactory;
    }

    public IServiceScope CreateScope() {
        return _serviceScopeFactory.CreateScope();
    }
}
