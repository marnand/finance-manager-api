using FinanceManager.Model;
using FinanceManager.Model.Control;
using FinanceManager.Model.DTO;

namespace FinanceManager.Service.Interfaces;

public interface ITransactionService
{
    Task<Result<int>> Create(CreateTransactionDTO user);
    Task<Result<IEnumerable<Transaction>>> Get(int id = 0);
}
