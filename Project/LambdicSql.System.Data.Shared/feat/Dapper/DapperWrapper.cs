using System;
using System.Collections.Generic;
using System.Data;
using System.Linq.Expressions;
using System.Linq;

namespace LambdicSql.feat.Dapper
{
    static class DynamicParametersWrapper
    {
        internal delegate object CreateDelegate();
        internal delegate void AddDelegate(object target, string name, object value, DbType? dbType, ParameterDirection? direction, int? size, byte? precision, byte? scale);

        internal static CreateDelegate Create;
        internal static AddDelegate Add;

        static DynamicParametersWrapper()
        {
            var asm = DapperAdapter.Assembly;

            var dynamicParam = asm.GetType("Dapper.DynamicParameters");
            Create = Expression.Lambda<CreateDelegate>(Expression.New(dynamicParam), new ParameterExpression[0]).Compile();

            var target = Expression.Parameter(typeof(object), "target");
            var name = Expression.Parameter(typeof(string), "name");
            var value  = Expression.Parameter(typeof(object), "value");
            var dbType = Expression.Parameter(typeof(DbType?), "dbType");
            var direction = Expression.Parameter(typeof(ParameterDirection?), "direction");
            var size = Expression.Parameter(typeof(int?), "size");
            var precision = Expression.Parameter(typeof(byte?), "precision");
            var scale = Expression.Parameter(typeof(byte?), "scale");
            var executeArgs = new[] { name, value, dbType, direction, size, precision, scale };
            Add = Expression.Lambda<AddDelegate>(Expression.Call(Expression.Convert(target, dynamicParam), "Add", new Type[0], executeArgs), new[] { target }.Concat(executeArgs).ToArray()).Compile();
        }
    }

    static class DapperWrapper
    {
        internal delegate int ExecuteDelegate(IDbConnection cnn, string sql, object param, IDbTransaction transaction, int? commandTimeout, CommandType? commandType);
        internal static ExecuteDelegate Execute;

        static DapperWrapper()
        {
            var asm = DapperAdapter.Assembly;

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
            var asm = DapperAdapter.Assembly;

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
