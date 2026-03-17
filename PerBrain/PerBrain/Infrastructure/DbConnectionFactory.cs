using Microsoft.Data.SqlClient;
using System.Data;

namespace PerBrain.Infrastructure
{
    public class DbConnectionFactory
    {
        private readonly IConfiguration _configuration;

        public DbConnectionFactory(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IDbConnection CreateConnection()
        {
            var connection = new SqlConnection(
                _configuration.GetConnectionString("DefaultConnection")
            );

            return connection;
        }
    }
}
