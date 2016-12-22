using LambdicSql.Inside;
using LambdicSql.SqlBase;
using LambdicSql.SqlBase.TextParts;
using System.Linq.Expressions;
using System.Linq;
using System;

namespace LambdicSql
{
    /// <summary>
    /// Utility.
    /// It can only be used within methods of the LambdicSql.Sql class.
    /// </summary>
    [SqlSyntax]
    public static class Utils
    {
        /// <summary>
        /// Cast.
        /// Used to place an ISqlExpressionBase object in a C # expression.
        /// </summary>
        /// <typeparam name="T">Destination type.</typeparam>
        /// <param name="expression">Expression.</param>
        /// <returns>Casted object.</returns>
        public static T Cast<T>(this ISqlExpressionBase expression) => InvalitContext.Throw<T>(nameof(Cast));

        /// <summary>
        /// Cast.
        /// Used to place an IMethodChain object in a C # expression.
        /// </summary>
        /// <typeparam name="T">Destination type.</typeparam>
        /// <param name="expression">Expression.</param>
        /// <returns>Casted object.</returns>
        [ResolveSqlSyntaxMethodChain]
        public static T Cast<T>(this IMethodChain expression) => InvalitContext.Throw<T>(nameof(Cast));

        /// <summary>
        /// Put the text in the expression of LamblicSql.
        /// </summary>
        /// <param name="text">Text.You can use the same format as System.String's Format method.</param>
        /// <param name="args">Format arguments.</param>
        /// <returns>LamblicSql's expression.</returns>
        public static object ToSql(this string text, params object[] args) => InvalitContext.Throw<object>(nameof(ToSql));

        /// <summary>
        /// Put the text in the expression of LamblicSql.
        /// </summary>
        /// <typeparam name="T">Destination type.</typeparam>
        /// <param name="text">Text.You can use the same format as System.String's Format method.</param>
        /// <param name="args">Format arguments.</param>
        /// <returns>LamblicSql's expression.</returns>
        public static T ToSql<T>(this string text, params object[] args) => InvalitContext.Throw<T>(nameof(ToSql));

        /// <summary>
        /// Put the text in the expression of LamblicSql.
        /// You can use TwoWaySql text format.
        /// It's ...
        /// TwoWaySql(@"SELECT money + /*0*/1000/**/ FROM tbl_remuneration WHERE /*1*/tbl_remuneration.money = 100/**/", 1000, tbl_remuneration.staff_id == 10)
        /// Replace /*no*/.../**/ by arguments.
        /// </summary>
        /// <param name="text">Text.</param>
        /// <param name="args">Format arguments.</param>
        /// <returns>LamblicSql's expression.</returns>
        public static IClauseChain<Non> TwoWaySql(this string text, params object[] args) => InvalitContext.Throw<IClauseChain<Non>>(nameof(ToSql));

        /// <summary>
        /// Get column name only.
        /// It's Removing table name and schema name.
        /// </summary>
        /// <typeparam name="T">column type.</typeparam>
        /// <param name="column">column.</param>
        /// <returns>Column name only.</returns>
        public static T ColumnOnly<T>(this T column) => InvalitContext.Throw<T>(nameof(ColumnOnly));

        static SqlText Convert(ISqlStringConverter converter, MethodCallExpression[] methods)
        {
            var method = methods[0];
            switch (method.Method.Name)
            {
                case nameof(Cast):
                    if (typeof(IMethodChain).IsAssignableFrom(method.Arguments[0].Type))
                    {
                        return converter.Convert(method.Arguments[0]);
                    }
                    return converter.Convert(method.Arguments[0]);
                case nameof(ToSql): return Utils.TextSql(converter, method);
                case nameof(TwoWaySql): return Utils.TwoWaySql(converter, method);
                case nameof(ColumnOnly): return Utils.ColumnOnly(converter, method);
            }
            throw new NotSupportedException();
        }

        static SqlText TextSql(ISqlStringConverter converter, MethodCallExpression method)
        {
            var text = (string)converter.ToObject(method.Arguments[0]);
            var array = method.Arguments[1] as NewArrayExpression;
            return new StringFormatText(text, array.Expressions.Select(e => converter.Convert(e)).ToArray());
        }

        static SqlText TwoWaySql(ISqlStringConverter converter, MethodCallExpression method)
        {
            var obj = converter.ToObject(method.Arguments[0]);
            var text = TowWaySqlSpec.ToStringFormat((string)obj);
            var array = method.Arguments[1] as NewArrayExpression;
            return new StringFormatText(text, array.Expressions.Select(e => converter.Convert(e)).ToArray());
        }

        static SqlText ColumnOnly(ISqlStringConverter converter, MethodCallExpression method)
        {
            var col = converter.Convert(method.Arguments[0]) as DbColumnText;
            if (col == null) throw new NotSupportedException("invalid column.");
            return col.Customize(new CustomizeColumnOnly());
        }
    }
}
