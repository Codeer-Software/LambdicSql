using LambdicSql.Clause.InsertInto;
using LambdicSql.Inside;
using LambdicSql.QueryBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace LambdicSql
{
    public static class InsertIntoExtensions
    {
        public static IInsertIntoQuery<TDB, TTable> InsertInto<TDB, TTable>(this IQuery<TDB, TDB> query, Expression<Func<TDB, TTable>> getTable, params Expression<Func<TTable, object>>[] getElements)
            where TDB : class
            where TTable: class
            => new InsertIntoClauseMakingQuery<TDB, TTable>(query, new InsertIntoClause<TDB, TTable>(getTable, getElements));

        public static IQuery<TDB, TDB> Values<TDB, TTable>(this IInsertIntoQuery<TDB, TTable> query, params TTable[] src)
            where TDB : class
            where TTable : class
            => Values(query, (IEnumerable<TTable>)src);

        public static IQuery<TDB, TDB> Values<TDB, TTable>(this IInsertIntoQuery<TDB, TTable> query, IEnumerable<TTable> src)
            where TDB : class
            where TTable : class
             => query.CustomClone(e => e.Values(src));

        public static IQuery<TDB, TDB> Values<TDB, TInput, Table>(this IInsertIntoQuery<TDB, Table> query, IEnumerable<TInput> src, Action<TInput, Table> convertor)
            where TDB : class
            where Table : class
        {
            var defaultValue = new SqlResultDefault();
            return query.CustomClone(clause => clause.Values(src.Select(e =>
            {
                var db = query.Create(defaultValue);
                var tbl = query.GetTable(db);
                convertor(e, tbl);
                return tbl;
            })));
        }
    }
}
