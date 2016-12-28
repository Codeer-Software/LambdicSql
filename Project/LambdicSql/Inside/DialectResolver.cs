using LambdicSql.SqlBase;

namespace LambdicSql.Inside
{
    static class DialectResolver
    {
        internal static DialectOption CreateCustomizer(string connectionTypeFullName)
        {
            switch (connectionTypeFullName)
            {
                case "Npgsql.NpgsqlConnection":
                    return new DialectOption() { StringAddOperator = "||", ExistRecursive = true };
                case "System.Data.SQLite.SQLiteConnection":
                case "IBM.Data.DB2.DB2Connection":
                    return new DialectOption() { StringAddOperator = "||" };
                case "Oracle.ManagedDataAccess.Client.OracleConnection":
                    return new DialectOption() { StringAddOperator = "||", ParameterPrefix = ":" };
                default:
                    return new DialectOption();
            }
        }
    }
}
