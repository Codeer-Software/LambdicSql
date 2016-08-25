using LambdicSql.SqlBase;
using System;
using System.Collections.Generic;
using System.Data;

namespace LambdicSql.feat.Dapper
{
    public static class DapperApaptExtensions
    {
        public static IEnumerable<T> Query<T>(this IDbConnection cnn, ISqlExpression<IQuery<T>> exp, IDbTransaction transaction = null, bool buffered = true, int? commandTimeout = default(int?), CommandType? commandType = default(CommandType?))
            => Query<T>(cnn, (ISqlExpression)exp, transaction, buffered, commandTimeout, commandType);

        public static IEnumerable<T> Query<T>(this IDbConnection cnn, ISqlExpression exp, IDbTransaction transaction = null, bool buffered = true, int? commandTimeout = default(int?), CommandType? commandType = default(CommandType?))
        {
            var info = exp.ToSqlInfo(cnn.GetType());
            try
            {
                return DapperWrapper<T>.Query(cnn, info.SqlText, info.Params, transaction, buffered, commandTimeout, commandType);
            }
            catch (Exception e)
            {
                throw GetCoreException(e);
            }
        }
        
        public static int Execute(this IDbConnection cnn, ISqlExpression exp, IDbTransaction transaction = null, int? commandTimeout = default(int?), CommandType? commandType = default(CommandType?))
        {
            try
            {
                var info = exp.ToSqlInfo(cnn.GetType());
                return DapperWrapper.Execute(cnn, info.SqlText, info.Params, transaction, commandTimeout, commandType);
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
    }
}
