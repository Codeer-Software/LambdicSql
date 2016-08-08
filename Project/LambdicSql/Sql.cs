using LambdicSql.Inside;
using LambdicSql.SqlBase;
using System;
using System.Linq.Expressions;

namespace LambdicSql
{
    public class Sql<TDB> where TDB : class, new()
    {
        public static SqlExpression<TResult> Create<TResult>(Expression<Func<TDB, TResult>> exp)
        {
            var db = DBDefineAnalyzer.GetDbInfo(() => new TDB());
            return new SqlExpressionSingle<TResult>(db, exp.Body);
        }
    }
}
