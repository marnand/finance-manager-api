using FinanceManager.Model;
using FinanceManager.Model.Control;
using FinanceManager.Model.DTO;
using FinanceManager.Repository.Interfaces;
using FinanceManager.Service.Interfaces;

namespace FinanceManager.Service;

internal class TransactionService(ITransactionRepository transactionRepository) : ITransactionService
{
    private readonly ITransactionRepository _transactionRepository = transactionRepository;

    public async Task<Result<int>> Create(CreateTransactionDTO transaction)
    {
        var result = Transaction.Create(transaction.Type, transaction.Description,
            transaction.Category, transaction.Amount, transaction.Date);
        if (!result.IsSuccess)
            return Result<int>.ResultError(result.Error!.Message, result.Error.StatusCode);

        return Result<int>.Success(await _transactionRepository.Create(result.Data!));
    }

    public async Task<Result<IEnumerable<Transaction>>> Get(int id)
    {
        var transactions = await _transactionRepository.Get(id);
        return Result<IEnumerable<Transaction>>.Success(transactions);
    }
}
