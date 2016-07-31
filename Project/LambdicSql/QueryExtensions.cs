using LambdicSql.Inside;
using LambdicSql.QueryBase;
using System;
using System.Data;

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

        public static SqlInfo ToSqlInfo<T>(this IQuery query)
             where T : IDbConnection
        {
            var parameters = new PrepareParameters();
            var text = SqlStringConverter.ToString(query, parameters, QueryCustomizeResolver.CreateCustomizer(typeof(T).FullName));
            return new SqlInfo(text, parameters.GetParameters());
        }

        public static ISqlExecutor<TSelect> ToExecutor<TDB, TSelect>(this IQuery<TDB, TSelect> query, IDbConnection connection)
             where TDB : class
             where TSelect : class
            => new SqlExecutor<TSelect>(connection, query as IQuery<TDB, TSelect>);

        public static IQuery<TDB, TSelect> Concat<TDB, TSelect>(this IQuery<TDB, TSelect> query, IQuery addQuery)
            where TDB : class
            where TSelect : class
            => new ClauseMakingQuery<TDB, TSelect, IClause>(query, addQuery.GetClausesClone());
    }
}
