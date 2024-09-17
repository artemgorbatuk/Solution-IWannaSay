using Repositories.Ef.Interfaces;
using Services.Interfaces;
using System.Security.Claims;

namespace Services.Api;

public class ServiceAuthentification : IServiceAuthentification {

    private readonly IRepositoryAuthentication repositoryAuthentication;

    public ServiceAuthentification(IRepositoryAuthentication repositoryAuthentication) {
        this.repositoryAuthentication = repositoryAuthentication;
    }

    public async Task<bool> IsUserExist(string login, string password) {
        return await repositoryAuthentication.IsUserExist(login, password);
    }

    public ClaimsIdentity GetIdentity(string login) {

        var claims = new List<Claim> { 
            new Claim(ClaimTypes.Name, login),
            new Claim(ClaimTypes.NameIdentifier, login)
        };

        return new ClaimsIdentity(claims, "Cookies");
    }
}