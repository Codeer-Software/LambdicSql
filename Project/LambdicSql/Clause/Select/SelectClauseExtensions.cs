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
        public static IQuery<TDB, TSelect> Select<TDB, TSelect>(this IQuery<TDB, TDB> query, Expression<Func<TDB, TSelect>> define)
            where TDB : class
            where TSelect : class
            => SelectCore<TDB, TSelect>(query, define);

        public static IQuery<TDB, TSelect> Select<TDB, TSelect>(this IQuery<TDB, TDB> query, Expression<Func<TDB, ISelectFuncs, TSelect>> define)
            where TDB : class
            where TSelect : class
            => SelectCore<TDB, TSelect>(query, define);

        static IQuery<TDB, TSelect> SelectCore<TDB, TSelect>(this IQuery<TDB, TDB> query, LambdaExpression define)
            where TDB : class
            where TSelect : class
        {
            var src = query as Query<TDB, TDB>;
            var select = SelectDefineAnalyzer.MakeSelectInfo(define.Body);

            var indexInSelect = select.GetElements().Select(e => e.Name).ToList();
            var dst = src.ConvertType(ExpressionToCreateFunc.ToCreateUseDbResult<TSelect>(name => indexInSelect.IndexOf(name), define.Body));
            dst.Select = select;
            return dst;
        }
    }
}
