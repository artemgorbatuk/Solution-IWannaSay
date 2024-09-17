namespace Repositories.Ef.Interfaces;

public interface IRepositoryAuthentication {
    Task<bool> IsUserExist(string login, string password);
}