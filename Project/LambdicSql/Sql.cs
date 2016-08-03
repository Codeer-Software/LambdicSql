using LambdicSql.Inside;
using LambdicSql.QueryBase;
using System;
using System.Linq.Expressions;

namespace LambdicSql
{
    public class Sql<TDB>
        where TDB : class, new()
    {
        //TODO
        public static ISqlExpression<TDB, TResult> Create<TResult>(Expression<Func<TDB, IQueryDesigner<NoSelected>, TResult>> exp)
        {
            var db = DBDefineAnalyzer.GetDbInfo(() => new TDB());
            return new SqlExpressionCore<TDB, TResult>(db, exp.Body);
        }
    }
}
