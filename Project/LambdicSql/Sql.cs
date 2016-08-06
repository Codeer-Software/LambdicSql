using LambdicSql.Inside;
using LambdicSql.QueryBase;
using System;
using System.Linq.Expressions;

namespace LambdicSql
{
    public class Sql
    {
        public static ISqlExpression Format2WaySql(string sql, params ISqlExpression[] exps)
            => new SqlExpressionFormatText(TowWaySqlSpec.ToStringFormat(sql), exps);
    }

    public class Sql<TDB> where TDB : class, new()
    {
        public static SqlExpression<TResult> Create<TResult>(Expression<Func<TDB, ISqlKeyWord<Non>, TResult>> exp)
        {
            var db = DBDefineAnalyzer.GetDbInfo(() => new TDB());
            return new SqlExpressionSingle<TResult>(db, exp.Body);
        }
    }
}
