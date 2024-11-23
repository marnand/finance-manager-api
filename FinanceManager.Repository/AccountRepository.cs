using FinanceManager.Config;
using FinanceManager.Model;
using FinanceManager.Repository.Interfaces;

namespace FinanceManager.Repository
{
    public class AccountRepository(DatabaseConnectionFactory connection) 
        : BaseRepository(connection), IAccountRepository
    {
        public async Task<IEnumerable<Account>> Get(int id = 0)
        {
            string sql = id == 0 ? "SELECT * FROM \"Account\"" 
                : "SELECT * FROM \"Account\" where Id = @id";
            return await QueryAsync<Account>(sql, new { id });
        }
    }
}
