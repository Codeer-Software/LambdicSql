namespace LambdicSql.Inside
{
    static class DialectResolver
    {
        internal static SqlConvertOption CreateCustomizer(string connectionTypeFullName)
        {
            switch (connectionTypeFullName)
            {
                case "Npgsql.NpgsqlConnection":
                case "System.Data.SQLite.SQLiteConnection":
                case "IBM.Data.DB2.DB2Connection":
                    return new SqlConvertOption() { StringAddOperator = "||" };
                case "Oracle.ManagedDataAccess.Client.OracleConnection":
                    return new SqlConvertOption() { StringAddOperator = "||", ParameterPrefix = ":" };
                default:
                    return new SqlConvertOption();
            }
        }
    }
}
