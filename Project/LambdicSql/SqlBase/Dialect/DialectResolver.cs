using LambdicSql.SqlBase;

namespace LambdicSql.Dialect
{
    public static class DialectResolver
    {
        internal static ISqlStringConverterCustomizer CreateCustomizer(string connectionTypeFullName)
        {
            if (connectionTypeFullName == "Npgsql.NpgsqlConnection")
            {
                return new PostgreSqlCustomizer();
            }
            if (connectionTypeFullName == "System.Data.SQLite.SQLiteConnection" ||
              connectionTypeFullName == "Oracle.ManagedDataAccess.Client.OracleConnection" ||
              connectionTypeFullName == "IBM.Data.DB2.DB2Connection")//TODO refactoring.
            {
                return new SQLiteCustomizer();
            }
            return null;
        }
    }
}
