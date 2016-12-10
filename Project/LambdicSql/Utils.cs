using LambdicSql.Inside;
using LambdicSql.SqlBase;
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
        /// Condition building helper.
        /// condition is used if enable is valid.
        /// </summary>
        /// <param name="enable">Whether condition is valid.</param>
        /// <param name="condition">Condition expression.</param>
        /// <returns>Condition.</returns>
        public static bool Condition(bool enable, bool condition) => InvalitContext.Throw<bool>(nameof(Condition));

        /// <summary>
        /// Put the text in the expression of LamblicSql.
        /// </summary>
        /// <param name="text">Text.You can use the same format as System.String's Format method.</param>
        /// <param name="args">Format arguments.</param>
        /// <returns>LamblicSql's expression.</returns>
        public static object TextSql(string text, params object[] args) => InvalitContext.Throw<object>(nameof(TextSql));

        /// <summary>
        /// Put the text in the expression of LamblicSql.
        /// </summary>
        /// <typeparam name="T">Destination type.</typeparam>
        /// <param name="text">Text.You can use the same format as System.String's Format method.</param>
        /// <param name="args">Format arguments.</param>
        /// <returns>LamblicSql's expression.</returns>
        public static T TextSql<T>(string text, params object[] args) => InvalitContext.Throw<T>(nameof(TextSql));

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
        public static IClauseChain<Non> TwoWaySql(string text, params object[] args) => InvalitContext.Throw<IClauseChain<Non>>(nameof(TextSql));

        /// <summary>
        /// Get column name only.
        /// It's Removing table name and schema name.
        /// </summary>
        /// <typeparam name="T">column type.</typeparam>
        /// <param name="column">column.</param>
        /// <returns>Column name only.</returns>
        public static T ColumnOnly<T>(T column) => InvalitContext.Throw<T>(nameof(ColumnOnly));

        static string ToString(ISqlStringConverter converter, MethodCallExpression[] methods)
        {
            var method = methods[0];
            switch (method.Method.Name)
            {
                case nameof(Cast):
                    if (typeof(IMethodChain).IsAssignableFrom(method.Arguments[0].Type))
                    {
                        return SqlDisplayAdjuster.AdjustSubQueryString(converter.ToString(method.Arguments[0]));
                    }
                    return converter.ToString(method.Arguments[0]);
                case nameof(Condition): return Condition(converter, method);
                case nameof(TextSql): return TextSql(converter, method);
                case nameof(TwoWaySql): return TwoWaySql(converter, method);
                case nameof(ColumnOnly): return ColumnOnly(converter, method);
            }
            throw new NotSupportedException();
        }

        static string Condition(ISqlStringConverter converter, MethodCallExpression method)
        {
            var obj = converter.ToObject(method.Arguments[0]);
            return (bool)obj ? converter.ToString(method.Arguments[1]) : string.Empty;
        }

        static string TextSql(ISqlStringConverter converter, MethodCallExpression method)
        {
            var text = (string)converter.ToObject(method.Arguments[0]);
            var array = method.Arguments[1] as NewArrayExpression;
            return string.Format(text, array.Expressions.Select(e => converter.ToString(e)).ToArray());
        }

        static string TwoWaySql(ISqlStringConverter converter, MethodCallExpression method)
        {
            var obj = converter.ToObject(method.Arguments[0]);
            var text = TowWaySqlSpec.ToStringFormat((string)obj);
            var array = method.Arguments[1] as NewArrayExpression;
            return string.Format(text, array.Expressions.Select(e => converter.ToString(e)).ToArray());
        }

        static string ColumnOnly(ISqlStringConverter converter, MethodCallExpression method)
        {
            var dic = converter.Context.DbInfo.GetLambdaNameAndColumn().ToDictionary(e => e.Value.SqlFullName, e => e.Value.SqlColumnName);
            string col;
            if (dic.TryGetValue(converter.ToString(method.Arguments[0]), out col)) return col;
            throw new NotSupportedException("invalid column.");
        }
    }
}

