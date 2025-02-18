using FinanceManager.Config;
using FinanceManager.Model;
using FinanceManager.Repository.Interfaces;

namespace FinanceManager.Repository;

public class TransactionRepository(DatabaseConnectionFactory connection)
    : BaseRepository(connection), ITransactionRepository
{
    public async Task<int> Create(Transaction transaction)
    {
        const string commandSql = @"INSERT INTO transaction (type,description,category,amount,date,
        created_at) VALUES (@Type,@Description,@Category,@Amount,@Date,@CreatedAt) RETURNING Id;";
        return await ExecuteScalarAsync<int>(commandSql, transaction);
    }

    public async Task<IEnumerable<Transaction>> Get(int id)
    {
        string commandSql = id == 0 ? "SELECT * FROM transaction"
            : "SELECT * FROM transaction where Id = @id";
        return await QueryAsync<Transaction>(commandSql, new { id });
    }
}
