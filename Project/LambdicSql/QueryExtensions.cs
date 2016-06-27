using LambdicSql.Inside;
using LambdicSql.QueryInfo;
using System;
using System.Linq.Expressions;
using System.Linq;

namespace LambdicSql
{
    public static class QueryExtensions
    {
        public static IQuerySelelectEnd<TDB, TSelect> Select<TDB, TSelect>(this IQueryStart<TDB, TDB> query, Expression<Func<TDB, TSelect>> define)
            where TDB : class
            where TSelect : class
            => SelectCore<TDB, TSelect>(query, define);

        public static IQuerySelelectEnd<TDB, TSelect> Select<TDB, TSelect>(this IQueryStart<TDB, TDB> query, Expression<Func<TDB, ISelectFuncs, TSelect>> define)
            where TDB : class
            where TSelect : class
            => SelectCore<TDB, TSelect>(query, define);

        public static IQueryFrom<TDB, TSelect> From<TDB, TSelect>(this IQuerySelelectEnd<TDB, TSelect> query, Expression<Func<TDB, object>> table)
            where TDB : class
            where TSelect : class
             => query.CustomClone(dst => dst.From = new FromInfo(dst.Db.LambdaNameAndTable[ExpressionToSqlString.GetElementName(table)]));

        public static IQueryWhere<TDB, TSelect> Where<TDB, TSelect>(this IQueryFromEnd<TDB, TSelect> query)
            where TDB : class
            where TSelect : class
             => query.CustomClone(dst => dst.Where = new ConditionClauseInfo());

        public static IQueryWhere<TDB, TSelect> Where<TDB, TSelect>(this IQueryFromEnd<TDB, TSelect> query, Expression<Func<TDB, bool>> condition)
            where TDB : class
            where TSelect : class
             => query.CustomClone(dst => dst.Where = new ConditionClauseInfo(condition.Body));

        public static IQueryWhere<TDB, TSelect> Where<TDB, TSelect>(this IQueryFromEnd<TDB, TSelect> query, Expression<Func<TDB, IWhereFuncs, bool>> condition)
            where TDB : class
            where TSelect : class
             => query.CustomClone(dst => dst.Where = new ConditionClauseInfo(condition.Body));

        public static IQueryHaving<TDB, TSelect> Having<TDB, TSelect>(this IQueryGroupByEnd<TDB, TSelect> query)
            where TDB : class
            where TSelect : class
             => query.CustomClone(dst => dst.Having = new ConditionClauseInfo());
        
        public static IQueryHaving<TDB, TSelect> Having<TDB, TSelect>(this IQueryGroupByEnd<TDB, TSelect> query, Expression<Func<TDB, IHavingFuncs, bool>> condition)
            where TDB : class
            where TSelect : class
             => query.CustomClone(dst => dst.Having = new ConditionClauseInfo(condition.Body));

        public static IQueryGroupByEnd<TDB, TSelect> GroupBy<TDB, TSelect>(this IQueryWhereEnd<TDB, TSelect> query, params Expression<Func<TDB, object>>[] targets)
            where TDB : class
            where TSelect : class
            => query.CustomClone(dst => dst.GroupBy = new GroupByInfo(targets.Select(e => e.Body).ToList()));

        public static IQueryOrderBy<TDB, TSelect> OrderBy<TDB, TSelect>(this IQueryGroupByEnd<TDB, TSelect> query)
            where TDB : class
            where TSelect : class
            => query.CustomClone(dst => dst.OrderBy = new OrderByInfo());

        public static T ToSubQuery<T>(this IQuery query) => default(T);

        public static ISqlExecutor<TSelect> ToExecutor<TDB, TSelect>(this IQuery<TDB, TSelect> query, string connectionString)
            where TDB : class
            where TSelect : class
        {
            var text = query.ToQueryString();
            Sql.Log?.Invoke(text);
            return new SqlExecutor<TSelect>(connectionString, text, ((IQueryInfo<TSelect>)query).Create);
        }

        public static string ToQueryString<TDB, TSelect>(this IQuery<TDB, TSelect> query)
            where TDB : class
            where TSelect : class
            => new QueryToSql().MakeQueryString((IQueryInfo)query);

        static IQueryStart<TDB, TSelect> SelectCore<TDB, TSelect>(this IQueryStart<TDB, TDB> query, LambdaExpression define)
            where TDB : class
            where TSelect : class
        {
            var src = query as Query<TDB, TDB>;
            var select = SelectDefineAnalyzer.MakeSelectInfo(define.Body);

            var indexInSelect = select.Elements.Select(e => e.Name).ToList();
            var dst = src.ConvertType(ExpressionToCreateFunc.ToCreateUseDbResult<TSelect>(name => indexInSelect.IndexOf(name), define.Body));
            dst.Select = select;
            return dst;
        }
    }
}
/*TODO@ clauses
LIMIT
Distinct
Case
UNION/EXCEPT/EXCEPT 
*/
