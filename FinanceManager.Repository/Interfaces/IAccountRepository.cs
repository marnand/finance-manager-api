using FinanceManager.Model;

namespace FinanceManager.Repository.Interfaces
{
    public interface IAccountRepository
    {
        Task<IEnumerable<Account>> Get(int id = 0);
    }
}
