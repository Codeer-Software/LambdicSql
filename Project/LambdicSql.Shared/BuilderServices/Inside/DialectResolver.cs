﻿namespace LambdicSql.BuilderServices.Inside
{
    static class DialectResolver
    {
        internal static DialectOption CreateCustomizer(string connectionTypeFullName)
        {
            switch (connectionTypeFullName)
            {
                case "Npgsql.NpgsqlConnection":
                    return new DialectOption() { ConnectionTypeFullName = connectionTypeFullName, StringAddOperator = "||", ExistRecursiveClause = true };
                case "SQLite.SQLiteConnection":
                case "System.Data.SQLite.SQLiteConnection":
                    return new DialectOption() { ConnectionTypeFullName = connectionTypeFullName, StringAddOperator = "||" };
                case "IBM.Data.DB2.DB2Connection":
                    return new DialectOption() { ConnectionTypeFullName = connectionTypeFullName, StringAddOperator = "||", CurrentDateTimeSeparator = " " };
                case "Oracle.ManagedDataAccess.Client.OracleConnection":
                    return new DialectOption() { ConnectionTypeFullName = connectionTypeFullName, StringAddOperator = "||", ParameterPrefix = ":" };
                default:
                    return new DialectOption() { ConnectionTypeFullName = connectionTypeFullName };
            }
        }
    }
}
