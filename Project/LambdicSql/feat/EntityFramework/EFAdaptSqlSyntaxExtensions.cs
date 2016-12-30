using LambdicSql.ExpressionConverterService.SqlSyntaxConverter;
using LambdicSql.Inside;
using LambdicSql.SqlBase;
using LambdicSql.SqlBase.TextParts;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace LambdicSql.feat.EntityFramework
{
    /// <summary>
    /// Extensions for adjust Entity Framework.
    /// It can only be used within methods of the LambdicSql.Sql class.
    /// </summary>
    public static class EFAdaptSqlSyntaxExtensions
    {
        /// <summary>
        /// Get entity.
        /// It can only be used within methods of the LambdicSql.Sql class.
        /// </summary>
        /// <typeparam name="TEntity">Entity.</typeparam>
        /// <param name="queryable">Queryable.</param>
        /// <returns>Entity.</returns>
        [SqlSyntaxT]
        public static TEntity T<TEntity>(this IQueryable<TEntity> queryable) => InvalitContext.Throw<TEntity>(nameof(T));
    }

    class SqlSyntaxTAttribute : SqlSyntaxConverterMethodAttribute
    {
        public override ExpressionElement Convert(IExpressionConverter converter, MethodCallExpression method)
            => converter.Convert(method.Arguments[0]);
    }
}
