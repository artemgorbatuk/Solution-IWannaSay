using Datasource.Ef.Contexts;
using Datasource.Ef.Factories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Repositories.Ef.Interfaces;


namespace Repositories.Ef.Api;
public class RepositoryAuthentication : IRepositoryAuthentication {

    private readonly IServiceScopeFactory serviceScopeFactory;

    public RepositoryAuthentication(IServiceScopeFactory serviceScopeFactory) {
        this.serviceScopeFactory = serviceScopeFactory;
    }

    public async Task<bool> IsUserExist(string login, string password) {

        var context = serviceScopeFactory.GetDbContext<DbContextIWannaSay>();

        return await context.Users.AnyAsync(u => u.Login == login && u.Password == password);
    }
}