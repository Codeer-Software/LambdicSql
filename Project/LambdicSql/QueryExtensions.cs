using LambdicSql.Inside;
using LambdicSql.QueryBase;
using System;
using System.Data;
using System.Data.Common;

namespace LambdicSql
{
    public static class QueryExtensions
    {
        public static T Cast<T>(this IQuery query)
        {
            throw new NotSupportedException("do not call cast except in expression.");
        }

        public static TSelect Cast<TDB, TSelect>(this IQuery<TDB, TSelect> query)
            where TDB : class
            where TSelect : class
        {
            throw new NotSupportedException("do not call cast except in expression.");
        }

        public static ISqlExecutor<TSelect> ToExecutor<TDB, TSelect>(this IQuery<TDB, TSelect> query, IDbAdapter adaptor)
             where TDB : class
             where TSelect : class
        {
            return new DbExecutor<TSelect>(adaptor, query as IQuery<TDB, TSelect>);
        }

        public static ISqlExecutor<TSelect> ToExecutor<TDB, TSelect>(this IQuery<TDB, TSelect> query, IDbConnection connection)
             where TDB : class
             where TSelect : class
        {
            //TODO
            return new DbExecutor2<TSelect>(connection, query as IQuery<TDB, TSelect>);
        }
    }
}

/*TODO clauses
Case
UNION/EXCEPT/EXCEPT
*/
