using Dapper;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace IdentityAPIWithoutEF.Library.Internal
{
    public class SqlDataAccess : ISqlDataAccess
    {
        private readonly IConfiguration _config;
        private const string ConnectionStringName = "IdentityAPIWithoutEF_DB";

        public SqlDataAccess(IConfiguration config)
        {
            _config = config;
        }

        public string GetConnectionString(string name)
        {
            var cnn = _config.GetConnectionString(name);

            return cnn;
        }

        public async Task<IEnumerable<T>> QueryAsync<T, U>(string storedProcedure, U parameters)
        {
            string connectionString = GetConnectionString(ConnectionStringName);

            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                var rows = await connection.QueryAsync<T>(storedProcedure, parameters,
                                                          commandType: CommandType.StoredProcedure);
                return rows;
            }
        }
    }
}
