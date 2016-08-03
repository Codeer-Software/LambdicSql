using LambdicSql.Inside;
using LambdicSql.QueryBase;
using System;
using System.Linq.Expressions;

namespace LambdicSql
{
    public static class Sql
    {
        public static Action<string> Log { get; set; }
    }

    public class Creating<T> where T : class, new()
    {
        public IQuery<T, T> Query { get; set; }
    }

    public class Sql<TDB>
        where TDB : class, new()
    {
        //TODO
        public static ISqlExpression<TDB, TResult> Create<TResult>(Expression<Func<TDB, IQueryDesigner<NoSelected>, TResult>> exp)
        {
            var query = DBDefineAnalyzer.CreateQuery(() => new TDB());
            return new SqlExpressionCore<TDB, TResult>(query, exp.Body);
        }
    }
}
