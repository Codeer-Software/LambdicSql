using LambdicSql.QueryBase;
using System.Data.Common;
using System.Data.SqlClient;

namespace LambdicSql.SqlServer
{
    public class SqlServerAdapter : IDbAdapter
    {
        public string ConnectionString { get; set; }

        public SqlServerAdapter() { }

        public SqlServerAdapter(string connectionString)
        {
            ConnectionString = connectionString;
        }

        public DbCommand CreateCommand() => new SqlCommand();
        public DbConnection CreateConnection()=> new SqlConnection(ConnectionString);
        public IQueryCustomizer CreateQueryCustomizer()=> null;
    }
}
