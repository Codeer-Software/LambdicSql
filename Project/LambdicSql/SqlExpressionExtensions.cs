using LambdicSql.Inside;
using LambdicSql.QueryBase;
using System;
using System.Data;
using System.Linq.Expressions;

namespace LambdicSql
{
    public static class SqlExpressionExtensions
    {
        public static DB Cast<DB>(this ISqlExpression query)
        {
            throw new NotSupportedException("do not call cast except in expression.");
        }

        public static DB Cast<DB, TResult>(this ISqlExpression<DB, TResult> query)
        {
            throw new NotSupportedException("do not call cast except in expression.");
        }

        public static SqlInfo ToSqlInfo<T>(this ISqlExpression exp)
             where T : IDbConnection
        {
            var parameters = new PrepareParameters();
            var converter = new SqlStringConverter(exp.Query.Db, parameters, QueryCustomizeResolver.CreateCustomizer(typeof(T).FullName), 0);
            return new SqlInfo(exp.ToString(converter), parameters.GetParameters());
        }

        public static ISqlExpression<TDB, TResult> Expression<TDB, TResult>(this IQuery<TDB> query, Expression<Func<TDB, TResult>> exp)
            where TDB : class
            => new SqlExpressionCore<TDB, TResult>(query, exp.Body);
    }
}
