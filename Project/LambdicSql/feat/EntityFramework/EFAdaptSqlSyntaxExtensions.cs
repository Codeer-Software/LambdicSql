using LambdicSql.ConverterService;
using LambdicSql.ConverterService.SqlSyntaxes;
using LambdicSql.Inside;
using LambdicSql.SqlBuilder.Parts;
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
        public override BuildingParts Convert(ExpressionConverter converter, MethodCallExpression method)
            => converter.Convert(method.Arguments[0]);
    }
}
