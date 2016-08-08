using LambdicSql.QueryBase;

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
            if (connectionTypeFullName == "System.Data.SQLite.SQLiteConnection")
            {
                return new SQLiteCustomizer();
            }
            return null;
        }
    }
}
