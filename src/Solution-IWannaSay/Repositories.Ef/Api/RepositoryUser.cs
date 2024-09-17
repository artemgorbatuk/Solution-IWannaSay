using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Repositories.Ef.Interfaces;
using Datasource.Ef.Contexts;
using Datasource.Ef.Models;
using Datasource.Ef.Factories;

namespace Repositories.Ef.Api;
public class RepositoryUser : IRepositoryUser {

    private readonly IServiceScopeFactory serviceScopeFactory;

    public RepositoryUser(IServiceScopeFactory serviceScopeFactory)
    {
        this.serviceScopeFactory = serviceScopeFactory;
    }

    public IEnumerable<User> GetUsers() {

        using var context = serviceScopeFactory.GetDbContext<DbContextIWannaSay>();

        var users = context.Users;

        return users;
    }

    public async Task<User?> GetUser(int id) {

        using var context = serviceScopeFactory.GetDbContext<DbContextIWannaSay>();

        var user = await context.Users.FirstOrDefaultAsync(p => p.Id == id);

        return user;
    }
}