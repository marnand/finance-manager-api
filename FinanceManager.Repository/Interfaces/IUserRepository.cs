using FinanceManager.Model;

namespace FinanceManager.Repository.Interfaces;

public interface IUserRepository
{
    Task<int> Create(User user);
    Task<IEnumerable<User>> Get(int id);
}
