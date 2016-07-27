using LambdicSql.PostgreSql;
using LambdicSql.QueryBase;
using LambdicSql.SQLite;

namespace LambdicSql
{
    public static class QueryCustomizeResolver
    {
        public static IQueryCustomizer CreateCustomizer(string connectionTypeFullName)
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
