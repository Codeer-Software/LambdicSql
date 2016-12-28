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
    [SqlSyntax]
    public static class EFAdaptSqlSyntaxExtensions
    {
        /// <summary>
        /// Get entity.
        /// It can only be used within methods of the LambdicSql.Sql class.
        /// </summary>
        /// <typeparam name="TEntity">Entity.</typeparam>
        /// <param name="queryable">Queryable.</param>
        /// <returns>Entity.</returns>
        public static TEntity T<TEntity>(this IQueryable<TEntity> queryable) => InvalitContext.Throw<TEntity>(nameof(T));

        static ExpressionElement Convert(IExpressionConverter converter, MethodCallExpression[] methods)
        {
            var method = methods[0];
            switch (method.Method.Name)
            {
                case nameof(T): return converter.Convert(method.Arguments[0]);
            }
            throw new NotSupportedException();
        }
    }
}
