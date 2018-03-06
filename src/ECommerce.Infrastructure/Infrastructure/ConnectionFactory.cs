using System.Data;
using System.Data.SqlClient;

namespace ECommerce.Infrastructure.Infrastructure
{
    public class ConnectionFactory : IConnectionFactory
    {
        private static string _connectionString;

        public ConnectionFactory(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IDbConnection GetConnection()
        {
            return new SqlConnection(_connectionString);
        }
    }
}
