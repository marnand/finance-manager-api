using Microsoft.Extensions.Options;
using Npgsql;
using System.Data;

namespace FinanceManager.Config
{
    public class DatabaseConnectionFactory(IOptions<Database> config)
    {
        private readonly Database _config = config.Value;

        public IDbConnection CreateConnection() => new NpgsqlConnection(_config.DefaultConnection);
    }
}
