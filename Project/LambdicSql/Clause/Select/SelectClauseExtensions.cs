using LambdicSql.Clause.Select;
using LambdicSql.Inside;
using LambdicSql.QueryBase;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace LambdicSql
{
    public static class SelectClauseExtensions
    {
        public static IQuery<TDB, TDB, SelectClause> Select<TDB>(this IQuery<TDB, TDB> query)
            where TDB : class
        {
            var select = new SelectClause();
            foreach (var e in query.Db.GetLambdaNameAndColumn())
            {
                select.Add(new SelectElement(e.Key, null));
            }
            return new ClauseMakingQuery<TDB, TDB, SelectClause>(query, select);
        }

        public static IQuery<TDB, TSelect, SelectClause> Select<TDB, TSelect>(this IQuery<TDB, TDB> query, Expression<Func<TDB, TSelect>> define)
            where TDB : class
            where TSelect : class
            => SelectCore<TDB, TSelect>(query, define);

        public static IQuery<TDB, TSelect, Clause.From.FromClause> SelectFrom<TDB, TSelect>(this IQuery<TDB, TDB> query, Expression<Func<TDB, TSelect>> selectTable)
            where TDB : class
            where TSelect : class
        {
            var select = SelectCore<TDB, TSelect>(query, selectTable);
            return select.From(selectTable);
        }

        static IQuery<TDB, TSelect, SelectClause> SelectCore<TDB, TSelect>(this IQuery<TDB, TDB> query, LambdaExpression define)
            where TDB : class
            where TSelect : class
        {
            var select = SelectDefineAnalyzer.MakeSelectInfo(define.Body);
            var indexInSelect = select.GetElements().Select(e => e.Name).ToList();
            return new ClauseMakingQuery<TDB, TSelect, SelectClause>(query.Db,
                ExpressionToCreateFunc.ToCreateUseDbResult<TSelect>(name => indexInSelect.IndexOf(name), define.Body),
                query.GetClausesClone().Concat(new IClause[] { select }).ToArray());
        }
    }
}