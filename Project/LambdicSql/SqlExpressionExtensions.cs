using LambdicSql.Inside;
using LambdicSql.QueryBase;
using System;
using System.Data;
using System.Linq.Expressions;

namespace LambdicSql
{
    public static class SqlExpressionExtensions
    {
        public static T Cast<T>(this ISqlExpression query)
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

        public static ISqlExpression<TDB> Expression<TDB>(this IQuery<TDB> query)
            where TDB : class
            => new SqlExpressionCore<TDB>(query);

        public static ISqlExpression<TDB> Expression<TDB, T>(this IQuery<TDB> query, Expression<Func<TDB, T>> exp)
            where TDB : class
            => new SqlExpressionCore<TDB>(query, exp.Body);

        public static ISqlExpression<TDB> Continue<TDB>(this ISqlExpression<TDB> expSrc, Expression<Func<TDB, ConnectionSqlExpression<bool>, bool>> exp)
            where TDB : class
            => new SqlExpressionCore<TDB>((SqlExpressionCore<TDB>)expSrc, exp.Body);

        public static ISqlExpression<TDB> Continue<TDB>(this ISqlExpression<TDB> expSrc, bool isEnable, Expression<Func<TDB, ConnectionSqlExpression<bool>, bool>> exp)
            where TDB : class
            => isEnable ? new SqlExpressionCore<TDB>((SqlExpressionCore<TDB>)expSrc, exp.Body) : expSrc;

        public static ISqlExpression<TDB> ContinuEx<TDB, TValue>(this ISqlExpression<TDB> expSrc, Expression<Func<TDB, IConnectionSqlExpression, TValue>> exp)
            where TDB : class
            => new SqlExpressionCore<TDB>((SqlExpressionCore<TDB>)expSrc, exp.Body);

        public static ISqlExpression<TDB> ContinuEx<TDB, TValue>(this ISqlExpression<TDB> expSrc, bool isEnable, Expression<Func<TDB, IConnectionSqlExpression, TValue>> exp)
            where TDB : class
            => isEnable ? new SqlExpressionCore<TDB>((SqlExpressionCore<TDB>)expSrc, exp.Body) : expSrc;
    }
}
