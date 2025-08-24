using Models.Models;

namespace Persistence.Repositories;
public interface IRepoUser : IRepository<long, User>
{
    Task<User> FindByUsername(string username);
}

