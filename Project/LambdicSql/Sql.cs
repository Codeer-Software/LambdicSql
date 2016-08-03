using LambdicSql.Inside;
using LambdicSql.QueryBase;
using System;
using System.Linq.Expressions;

namespace LambdicSql
{
    public class Sql<TDB> where TDB : class, new()
    {
        public static ISqlExpression<TResult> Create<TResult>(Expression<Func<TDB, IQueryDesigner<NoSelected>, TResult>> exp)
        {
            var db = DBDefineAnalyzer.GetDbInfo(() => new TDB());
            return new SqlExpression<TResult>(db, exp.Body);
        }
    }
}
