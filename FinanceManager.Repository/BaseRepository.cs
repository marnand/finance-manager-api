using Dapper;
using FinanceManager.Config;

namespace FinanceManager.Repository
{
    public abstract class BaseRepository(DatabaseConnectionFactory connection)
    {
        protected readonly DatabaseConnectionFactory _connection = connection;

        protected async Task<IEnumerable<T>> QueryAsync<T>(string sql, object parameters = null)
        {
            var conn = _connection.CreateConnection();
            return await conn.QueryAsync<T>(sql, parameters);
        }

        protected async Task<T> QuerySingleAsync<T>(string sql, object parameters = null)
        {
            var conn = _connection.CreateConnection();
            return await conn.QuerySingleAsync<T>(sql, parameters);
        }

        protected async Task<T> QuerySingleOrDefaultAsync<T>(string sql, object parameters = null)
        {
            var conn = _connection.CreateConnection();
            return await conn.QuerySingleOrDefaultAsync<T>(sql, parameters);
        }

        protected async Task<int> ExecuteAsync(string sql, object parameters = null)
        {
            var conn = _connection.CreateConnection();
            return await conn.ExecuteAsync(sql, parameters);
        }

        protected async Task<T> ExecuteScalarAsync<T>(string sql, object parameters = null)
        {
            var conn = _connection.CreateConnection();
            return await conn.ExecuteScalarAsync<T>(sql, parameters);
        }
    }
}
