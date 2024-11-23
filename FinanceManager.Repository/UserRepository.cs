using FinanceManager.Config;
using FinanceManager.Model;
using FinanceManager.Repository.Interfaces;

namespace FinanceManager.Repository;

public class UserRepository(DatabaseConnectionFactory connection)
    : BaseRepository(connection), IUserRepository
{
    public async Task<int> Create(User user)
    {
        const string commandSql = @"
                INSERT INTO ""User"" (Name, Email, PasswordHash, CreatedAt) 
                VALUES (@Name, @Email, @PasswordHash, @CreatedAt)
                RETURNING Id;
            ";  
        return await ExecuteScalarAsync<int>(commandSql, user);
    }

    public async Task<IEnumerable<User>> Get(int id)
    {
        string commandSql = id == 0 ? "SELECT * FROM \"User\""
            : "SELECT * FROM \"User\" where Id = @id";
        return await QueryAsync<User>(commandSql, new { id });
    }
}
