using FinanceManager.Model;

namespace FinanceManager.Repository.Interfaces;

public interface ITransactionRepository
{
    Task<int> Create(Transaction transaction);
    Task<IEnumerable<Transaction>> Get(int id);
}
