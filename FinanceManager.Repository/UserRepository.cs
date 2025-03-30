using FinanceManager.Config;
using FinanceManager.Model;
using FinanceManager.Repository.Interfaces;

namespace FinanceManager.Repository;

public class UserRepository(DatabaseConnectionFactory connection)
    : BaseRepository(connection), IUserRepository
{
    public async Task<int> Create(User user)
    {
        const string commandSql = @"INSERT INTO users (name, email, password, created_at) 
            VALUES (@Name, @Email, @Password, @CreatedAt) RETURNING Id;";
        return await ExecuteScalarAsync<int>(commandSql, user);
    }

    public async Task<IEnumerable<User>> Get(int id)
    {
        string commandSql = id == 0 ? "SELECT id, name, email, password, created_at as createdat FROM users"
            : "SELECT id, name, email, password, created_at as createdat FROM users where Id = @id";
        return await QueryAsync<User>(commandSql, new { id });
    }

    public async Task<User> GetByEmail(string email)
    {
        const string commandSql = "SELECT * FROM users where Email = @email";
        return await QuerySingleOrDefaultAsync<User>(commandSql, new { email });
    }
}
