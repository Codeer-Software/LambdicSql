using LambdicSql.Inside;
using LambdicSql.SqlBase;
using LambdicSql.SqlBase.TextParts;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LambdicSql
{
    /// <summary>
    /// Enhancement of ISqlExpressionBase.
    /// </summary>
    public static class SqlExpressionExtensions
    {
        /// <summary>
        /// Sql information.
        /// This have static information of the type selected in the SELECT clause.
        /// </summary>
        /// <typeparam name="TSelected">Type of selected.</typeparam>
        /// <param name="expression">Object with information of expression representing SQL.</param>
        /// <param name="connectionType">IDbConnection's type.</param>
        /// <returns>Sql information.</returns>
        public static BuildedSql<TSelected> Build<TSelected>(this SqlExpression<TSelected> expression, Type connectionType)
          => new BuildedSql<TSelected>(Build((ISqlExpression)expression, connectionType));

        /// <summary>
        /// Sql information.
        /// </summary>
        /// <param name="expression">Object with information of expression representing SQL.</param>
        /// <param name="connectionType">IDbConnection's type.</param>
        /// <returns>Sql information.</returns>
        public static BuildedSql Build(this ISqlExpression expression, Type connectionType)
            => Build(expression, DialectResolver.CreateCustomizer(connectionType.FullName));

        /// <summary>
        /// Sql information.
        /// </summary>
        /// <param name="expression">Object with information of expression representing SQL.</param>
        /// <param name="option">Options for converting from C # to SQL string.</param>
        /// <returns>Sql information.</returns>
        public static BuildedSql Build(this ISqlExpression expression, DialectOption option)
        {
            var context = new ExpressionConvertingContext(option);
            return new BuildedSql(expression.DbInfo, expression.ExpressionElement.ToString(true, 0, context), context.ObjectCreateInfo, context.ParameterInfo.GetDbParams());
        }
    }
}
