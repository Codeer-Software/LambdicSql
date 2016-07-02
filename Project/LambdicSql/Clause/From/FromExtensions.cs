using LambdicSql.QueryBase;
using LambdicSql.Clause.From;
using System;
using System.Linq.Expressions;
using System.Linq;

namespace LambdicSql
{
    public static class FromExtensions
    {
        public static IQuery<TDB, TSelect, FromClause> From<TDB, TSelect, T>(this IQuery<TDB, TSelect> query, Expression<Func<TDB, T>> table)
            where TDB : class
            where TSelect : class
             => new ClauseMakingQuery<TDB, TSelect, FromClause>(query, new FromClause(table.Body));

        public static IQuery<TDB, TSelect, FromClause> From<TDB, TSelect>(this IQuery<TDB, TSelect> query)
            where TDB : class
            where TSelect : class
        { 
            //table count must be 1.
            if (query.Db.GetLambdaNameAndTable().Count != 1)
            {
                throw new NotSupportedException();
            }
            return new ClauseMakingQuery<TDB, TSelect, FromClause>(query, new FromClause(query.Db.GetLambdaNameAndTable().First().Value.SqlFullName));
        }

        public static IQuery<TDB, TSelect, FromClause> Join<TDB, TSelect>(this IQuery<TDB, TSelect, FromClause> query, Expression<Func<TDB, object>> table, Expression<Func<TDB, bool>> condition)
            where TDB : class
            where TSelect : class
             => query.CustomClone(e => e.Join(new JoinElement(JoinType.Join, table.Body, condition.Body)));

        public static IQuery<TDB, TSelect, FromClause> LeftJoin<TDB, TSelect>(this IQuery<TDB, TSelect, FromClause> query, Expression<Func<TDB, object>> table, Expression<Func<TDB, bool>> condition)
            where TDB : class
            where TSelect : class
             => query.CustomClone(e => e.Join(new JoinElement(JoinType.LeftJoin, table.Body, condition.Body)));

        public static IQuery<TDB, TSelect, FromClause> RightJoin<TDB, TSelect>(this IQuery<TDB, TSelect, FromClause> query, Expression<Func<TDB, object>> table, Expression<Func<TDB, bool>> condition)
            where TDB : class
            where TSelect : class
             => query.CustomClone(e => e.Join(new JoinElement(JoinType.RightJoin, table.Body, condition.Body)));

        public static IQuery<TDB, TSelect, FromClause> CrossJoin<TDB, TSelect>(this IQuery<TDB, TSelect, FromClause> query, Expression<Func<TDB, object>> table)
            where TDB : class
            where TSelect : class
             => query.CustomClone(e => e.Join(new JoinElement(JoinType.CrossJoin, table.Body, null)));
    }
}
