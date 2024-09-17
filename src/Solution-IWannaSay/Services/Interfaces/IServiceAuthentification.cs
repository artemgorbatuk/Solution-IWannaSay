using System.Security.Claims;

namespace Services.Interfaces;
public interface IServiceAuthentification {
    Task<bool> IsUserExist(string login, string password);
    ClaimsIdentity GetIdentity(string login);
}