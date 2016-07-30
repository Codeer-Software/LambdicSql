using LambdicSql.QueryBase;
using System;
using System.Linq.Expressions;

namespace LambdicSql
{
    public static class ConditionBuilderExtensions
    {
        public static ISqlExpression<TDB, bool> ConditionBuilder<TDB>(this IQuery<TDB> query)
            where TDB : class
            => new SqlExpressionCore<TDB, bool>(query);

        public static ISqlExpression<TDB, bool> Continue<TDB>(this ISqlExpression<TDB, bool> expSrc, Expression<Func<TDB, PreviousCondition, bool>> exp)
            where TDB : class
            => new SqlExpressionCore<TDB, bool>((SqlExpressionCore<TDB>)expSrc, exp.Body);

        public static ISqlExpression<TDB, bool> Continue<TDB>(this ISqlExpression<TDB, bool> expSrc, bool isEnable, Expression<Func<TDB, PreviousCondition, bool>> exp)
            where TDB : class
            => isEnable ? new SqlExpressionCore<TDB, bool>((SqlExpressionCore<TDB>)expSrc, exp.Body) : expSrc;
    }
}
