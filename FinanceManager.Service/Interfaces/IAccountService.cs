using FinanceManager.Model;

namespace FinanceManager.Service.Interfaces
{
    public interface IAccountService
    {
        Task<IEnumerable<Account>> Get(int id = 0);
        Task Create(Account user);
        Task<Account> Edit(int id);
        Task Remove(int id);
    }
}
