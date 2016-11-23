using LambdicSql.SqlBase;
using System;
using System.Collections.Generic;
using System.Data;

namespace LambdicSql.feat.Dapper
{
    public static class DapperApaptExtensions
    {
        public static IEnumerable<T> Query<T>(this IDbConnection cnn, ISqlExpressionBase<IQuery<T>> exp, IDbTransaction transaction = null, bool buffered = true, int? commandTimeout = default(int?), CommandType? commandType = default(CommandType?))
            => Query<T>(cnn, (ISqlExpressionBase)exp, transaction, buffered, commandTimeout, commandType);

        public static IEnumerable<T> Query<T>(this IDbConnection cnn, ISqlExpressionBase exp, IDbTransaction transaction = null, bool buffered = true, int? commandTimeout = default(int?), CommandType? commandType = default(CommandType?))
        {
            var info = exp.ToSqlInfo(cnn.GetType());

            //for testing.
            if (DapperApaptExtensionsForTest.Query != null) return new T[DapperApaptExtensionsForTest.Query(cnn, info)];

            try
            {
                return DapperWrapper<T>.Query(cnn, info.SqlText, CreateDynamicParam(info.DbParams), transaction, buffered, commandTimeout, commandType);
            }
            catch (Exception e)
            {
                throw GetCoreException(e);
            }
        }

        public static int Execute(this IDbConnection cnn, ISqlExpressionBase exp, IDbTransaction transaction = null, int? commandTimeout = default(int?), CommandType? commandType = default(CommandType?))
        {
            var info = exp.ToSqlInfo(cnn.GetType());

            //for testing.
            if (DapperApaptExtensionsForTest.Execute != null) return DapperApaptExtensionsForTest.Execute(cnn, info);

            try
            {
                return DapperWrapper.Execute(cnn, info.SqlText, CreateDynamicParam(info.DbParams), transaction, commandTimeout, commandType);
            }
            catch (Exception e)
            {
                throw GetCoreException(e);
            }
        }

        static Exception GetCoreException(Exception e)
        {
            while (true)
            {
                if (e.InnerException == null) return e;
                e = e.InnerException;
            }
        }

        private static object CreateDynamicParam(Dictionary<string, DbParam> dbParams)
        {
            var target = DynamicParametersWrapper.Create();
            foreach (var e in dbParams)
            {
                DynamicParametersWrapper.Add(target, e.Key, e.Value.Value, e.Value.DbType, e.Value.Direction, e.Value.Size, e.Value.Precision, e.Value.Scale);
            }
            return target;
        }
    }


    public static class DapperApaptExtensionsForTest
    {
        public delegate int QueryDelegate(IDbConnection cnn, SqlInfo info);
        public delegate int ExecuteDelegate(IDbConnection cnn, SqlInfo info);
        public static QueryDelegate Query { get; set; }
        public static ExecuteDelegate Execute { get; set; }
    }
}
