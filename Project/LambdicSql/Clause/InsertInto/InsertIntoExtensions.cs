using LambdicSql.Clause.InsertInto;
using LambdicSql.QueryBase;
using System;
using System.Linq.Expressions;

namespace LambdicSql
{
    public static class InsertIntoExtensions
    {
        public static IInsertIntoQuery<TDB, TTable> InsertInto<TDB, TTable>(this IQuery<TDB, TDB> query, Expression<Func<TDB, TTable>> getTable, params Expression<Func<TTable, object>>[] getElements)
            where TDB : class
            where TTable: class
            => new InsertIntoClauseMakingQuery<TDB, TTable>(query, new InsertIntoClause<TDB, TTable>(getTable, getElements));

        public static IQuery<TDB, TDB> Values<TDB, TTable>(this IInsertIntoQuery<TDB, TTable> query, TTable src)
            where TDB : class
            where TTable : class
            => query.CustomClone(e => e.Values(src));
    }
}
