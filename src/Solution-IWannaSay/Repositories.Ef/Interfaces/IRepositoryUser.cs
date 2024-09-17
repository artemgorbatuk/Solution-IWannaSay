using Datasource.Ef.Models;

namespace Repositories.Ef.Interfaces;
public interface IRepositoryUser {
    IEnumerable<User> GetUsers();
    Task<User?> GetUser(int id);
}