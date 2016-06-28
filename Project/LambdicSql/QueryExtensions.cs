using LambdicSql.Inside;
using LambdicSql.QueryInfo;
using System;
using System.Linq.Expressions;
using System.Linq;

namespace LambdicSql
{
    public static class QueryExtensions
    {
        public static IQuery<TDB, TSelect> Select<TDB, TSelect>(this IQuery<TDB, TDB> query, Expression<Func<TDB, TSelect>> define)
            where TDB : class
            where TSelect : class
            => SelectCore<TDB, TSelect>(query, define);

        public static IQuery<TDB, TSelect> Select<TDB, TSelect>(this IQuery<TDB, TDB> query, Expression<Func<TDB, ISelectFuncs, TSelect>> define)
            where TDB : class
            where TSelect : class
            => SelectCore<TDB, TSelect>(query, define);

        public static IQueryFrom<TDB, TSelect> From<TDB, TSelect, T>(this IQuery<TDB, TSelect> query, Expression<Func<TDB, T>> table)
            where TDB : class
            where TSelect : class
             => query.CustomClone(dst => dst.From = new FromInfo(dst.Db.GetLambdaNameAndTable()[ExpressionToSqlString.GetElementName(table)]));

        public static IQueryWhere<TDB, TSelect> Where<TDB, TSelect>(this IQuery<TDB, TSelect> query)
            where TDB : class
            where TSelect : class
             => query.CustomClone(dst => dst.Where = new ConditionClauseInfo());

        public static IQueryWhere<TDB, TSelect> Where<TDB, TSelect>(this IQuery<TDB, TSelect> query, Expression<Func<TDB, bool>> condition)
            where TDB : class
            where TSelect : class
             => query.CustomClone(dst => dst.Where = new ConditionClauseInfo(condition.Body));

        public static IQueryWhere<TDB, TSelect> Where<TDB, TSelect>(this IQuery<TDB, TSelect> query, Expression<Func<TDB, IWhereFuncs, bool>> condition)
            where TDB : class
            where TSelect : class
             => query.CustomClone(dst => dst.Where = new ConditionClauseInfo(condition.Body));

        public static IQueryHaving<TDB, TSelect> Having<TDB, TSelect>(this IQuery<TDB, TSelect> query)
            where TDB : class
            where TSelect : class
             => query.CustomClone(dst => dst.Having = new ConditionClauseInfo());
        
        public static IQueryHaving<TDB, TSelect> Having<TDB, TSelect>(this IQuery<TDB, TSelect> query, Expression<Func<TDB, IHavingFuncs, bool>> condition)
            where TDB : class
            where TSelect : class
             => query.CustomClone(dst => dst.Having = new ConditionClauseInfo(condition.Body));

        public static IQueryGroupBy<TDB, TSelect> GroupBy<TDB, TSelect>(this IQuery<TDB, TSelect> query, params Expression<Func<TDB, object>>[] targets)
            where TDB : class
            where TSelect : class
            => query.CustomClone(dst => dst.GroupBy = new GroupByInfo(targets.Select(e => e.Body).ToArray()));

        public static IQueryOrderBy<TDB, TSelect> OrderBy<TDB, TSelect>(this IQuery<TDB, TSelect> query)
            where TDB : class
            where TSelect : class
            => query.CustomClone(dst => dst.OrderBy = new OrderByInfo());

        public static T ToSubQuery<T>(this IQuery query) => default(T);

        public static TSelect ToSubQuery<TDB, TSelect>(this IQuery<TDB, TSelect> query)
            where TDB : class
            where TSelect : class 
            => default(TSelect);

        //TODO@@
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

//TODO sub query in From clauses

/*TODO clauses
Distinct
		new Distinct()
		new Distinct<int>(obj)

Case
UNION/EXCEPT/EXCEPT
Join many version. 
    INNER JOIN  → JOIN
    LEFT OUTER JOIN → LEFT JOIN
    RIGHT OUTER JOIN → RIGHT JOIN
    CROSS JOIN
*/
