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

        public static string ToSqlString<T>(this IQuery query)
             where T : IDbConnection
            => SqlStringConverter.ToString(query, new PrepareParameters(), QueryCustomizeResolver.CreateCustomizer(typeof(T).FullName));

        public static ISqlExecutor<TSelect> ToExecutor<TDB, TSelect>(this IQuery<TDB, TSelect> query, IDbConnection connection)
             where TDB : class
             where TSelect : class
            => new SqlExecutor<TSelect>(connection, query as IQuery<TDB, TSelect>);

        public static IQuery<TDB, TSelect> AddQuery<TDB, TSelect>(this IQuery<TDB, TSelect> query, IQuery<TDB> addQuery)
            where TDB : class
            where TSelect : class
            => new ClauseMakingQuery<TDB, TSelect, IClause>(query, addQuery.GetClausesClone());
    }
}
