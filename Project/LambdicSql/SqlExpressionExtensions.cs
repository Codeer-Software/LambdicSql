using LambdicSql.Inside;
using LambdicSql.SqlBase;
using System;
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
        public static SqlInfo<TSelected> ToSqlInfo<TSelected>(this ISqlExpressionBase<IQuery<TSelected>> expression, Type connectionType)
          => new SqlInfo<TSelected>(ToSqlInfo((ISqlExpressionBase)expression, connectionType));

        /// <summary>
        /// Sql information.
        /// </summary>
        /// <param name="expression">Object with information of expression representing SQL.</param>
        /// <param name="connectionType">IDbConnection's type.</param>
        /// <returns>Sql information.</returns>
        public static SqlInfo ToSqlInfo(this ISqlExpressionBase expression, Type connectionType)
            => ToSqlInfo(expression, DialectResolver.CreateCustomizer(connectionType.FullName));

        /// <summary>
        /// Sql information.
        /// </summary>
        /// <param name="expression">Object with information of expression representing SQL.</param>
        /// <param name="option">Options for converting from C # to SQL string.</param>
        /// <returns>Sql information.</returns>
        public static SqlInfo ToSqlInfo(this ISqlExpressionBase expression, SqlConvertOption option)
            => ToSqlInfo(expression, option, null);

        static SqlInfo ToSqlInfo(this ISqlExpressionBase exp, SqlConvertOption option, ISqlSyntaxCustomizer customizer)
        {
            var context = new DecodeContext(exp.DbInfo, option.ParameterPrefix);
            var converter = new SqlStringConverter(context, option, customizer);
            var text = exp.ToString(converter);

            //adjust. remove empty line.
            var lines = text.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            text = string.Join(Environment.NewLine, lines.Where(e => !string.IsNullOrEmpty(e.Trim())).ToArray());

            return new SqlInfo(exp.DbInfo, text, context.SelectClauseInfo, context.Parameters);
        }
    }
}
