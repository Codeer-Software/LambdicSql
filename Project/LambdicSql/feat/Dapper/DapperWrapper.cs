using System;
using System.Collections.Generic;
using System.Data;
using System.Linq.Expressions;
using System.Reflection;

namespace LambdicSql.feat.Dapper
{
    static class DapperWrapper
    {
        internal delegate int ExecuteDelegate(IDbConnection cnn, string sql, object param, IDbTransaction transaction, int? commandTimeout, CommandType? commandType);
        internal static ExecuteDelegate Execute;

        static DapperWrapper()
        {
            Assembly asm = null;
            try
            {
                asm = Assembly.LoadFrom("Dapper.dll");
            }
            catch { throw new PackageIsNotInstalledException("Dapper is not installed. Please install dapper of your faverit version."); }
            if (asm == null) throw new PackageIsNotInstalledException("Dapper is not installed. Please install dapper of your faverit version.");

            var sqlMapper = asm.GetType("Dapper.SqlMapper");

            var cnn = Expression.Parameter(typeof(IDbConnection), "cnn");
            var sql = Expression.Parameter(typeof(string), "sql");
            var param = Expression.Parameter(typeof(object), "param");

            var transaction = Expression.Parameter(typeof(IDbTransaction), "transaction");
            var commandTimeout = Expression.Parameter(typeof(int?), "commandTimeout");
            var commandType = Expression.Parameter(typeof(CommandType?), "commandType");

            var executeArgs = new[] { cnn, sql, param, transaction, commandTimeout, commandType };
            Execute = Expression.Lambda<ExecuteDelegate>(Expression.Call(sqlMapper, "Execute", new Type[0], executeArgs), executeArgs).Compile();
        }
    }

    static class DapperWrapper<T>
    {
        internal delegate IEnumerable<T> QueryDelegate(IDbConnection cnn, string sql, object param, IDbTransaction transaction, bool buffered, int? commandTimeout, CommandType? commandType);
        internal static QueryDelegate Query;

        static DapperWrapper()
        {
            Assembly asm = null;
            try
            {
                asm = Assembly.LoadFrom("Dapper.dll");
            }
            catch { throw new PackageIsNotInstalledException("Dapper is not installed. Please install dapper of your faverit version."); }
            if (asm == null) throw new PackageIsNotInstalledException("Dapper is not installed. Please install dapper of your faverit version.");

            var sqlMapper = asm.GetType("Dapper.SqlMapper");

            var cnn = Expression.Parameter(typeof(IDbConnection), "cnn");
            var sql = Expression.Parameter(typeof(string), "sql");
            var param = Expression.Parameter(typeof(object), "param");

            var transaction = Expression.Parameter(typeof(IDbTransaction), "transaction");
            var buffered = Expression.Parameter(typeof(bool), "buffered");
            var commandTimeout = Expression.Parameter(typeof(int?), "commandTimeout");
            var commandType = Expression.Parameter(typeof(CommandType?), "commandType");

            var queryArgs = new[] { cnn, sql, param, transaction, buffered, commandTimeout, commandType };
            Query = Expression.Lambda<QueryDelegate>(Expression.Call(sqlMapper, "Query", new[] { typeof(T) }, queryArgs), queryArgs).Compile();;
        }
    }
}
