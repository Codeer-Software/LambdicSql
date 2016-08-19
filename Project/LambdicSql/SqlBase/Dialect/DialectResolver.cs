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
              connectionTypeFullName == "Oracle.ManagedDataAccess.Client.OracleConnection")//TODO refactoring.
            {
                return new SQLiteCustomizer();
            }
            return null;
        }
    }
}
