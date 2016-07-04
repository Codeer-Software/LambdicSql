using LambdicSql.Clause.Update;
using LambdicSql.QueryBase;
using System;
using System.Linq.Expressions;

namespace LambdicSql
{
    public static class UpdateExtensions
    {
        public static IUpdateQuery<TDB, TTable> Update<TDB, TTable>(this IQuery<TDB, TDB> query, Expression<Func<TDB, TTable>> getTable)
            where TDB : class
            where TTable : class
            => new UpdateClauseMakingQuery<TDB, TTable>(query, new UpdateClause(getTable.Body));


        public static ISetQuery<TDB, TTable> Set<TDB, TTable>(this IUpdateQuery<TDB, TTable> query)
            where TDB : class
            where TTable : class
            => (ISetQuery<TDB, TTable>)query;

        public static ISetQuery<TDB, TTable> Assign<TDB, TTable, TVal>(this ISetQuery<TDB, TTable> query, Expression<Func<TTable, object>> selectData, TVal value)
            where TDB : class
            where TTable : class
            => (ISetQuery<TDB, TTable>)query.CustomClone(e => e.Set(selectData.Body, value));

        public static ISetQuery<TDB, TTable> Assign<TDB, TTable>(this ISetQuery<TDB, TTable> query, Expression<Func<TTable, object>> selectData, Expression<Func<TTable, object>> value)
            where TDB : class
            where TTable : class
            => (ISetQuery<TDB, TTable>)query.CustomClone(e => e.Set(selectData.Body, value.Body));
    }
}
