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
        public static SqlInfo<TSelected> ToSqlInfo<TSelected>(this ISqlExpression<IClauseChain<TSelected>> expression, Type connectionType)
          => new SqlInfo<TSelected>(ToSqlInfo((ISqlExpression)expression, connectionType));

        /// <summary>
        /// Sql information.
        /// </summary>
        /// <param name="expression">Object with information of expression representing SQL.</param>
        /// <param name="connectionType">IDbConnection's type.</param>
        /// <returns>Sql information.</returns>
        public static SqlInfo ToSqlInfo(this ISqlExpression expression, Type connectionType)
            => ToSqlInfo(expression, DialectResolver.CreateCustomizer(connectionType.FullName));

        /// <summary>
        /// Sql information.
        /// </summary>
        /// <param name="expression">Object with information of expression representing SQL.</param>
        /// <param name="option">Options for converting from C # to SQL string.</param>
        /// <returns>Sql information.</returns>
        public static SqlInfo ToSqlInfo(this ISqlExpression expression, SqlConvertOption option)
        {
            var context = new SqlConvertingContext(option);
            return new SqlInfo(expression.DbInfo, expression.SqlText.ToString(true, 0, context), context.ObjectCreateInfo, context.ParameterInfo);
        }
    }
}
