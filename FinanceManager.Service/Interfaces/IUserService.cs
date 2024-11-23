using FinanceManager.Model;

namespace FinanceManager.Service.Interfaces;

public interface IUserService
{
    Task<IEnumerable<User>> Get(int id = 0);
    Task<int> Create(User user);
    Task<User> Edit();
    Task Remove(int id);
}
