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
    public static class Utils
    {
        /// <summary>
        /// Put the text in the expression of LamblicSql.
        /// </summary>
        /// <param name="text">Text.You can use the same format as System.String's Format method.</param>
        /// <param name="args">Format arguments.</param>
        /// <returns>LamblicSql's expression.</returns>
        [SqlSyntaxToSql]
        public static object ToSql(this string text, params object[] args) => InvalitContext.Throw<object>(nameof(ToSql));

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
        [SqlSyntaxTwoWaySql]
        public static object TwoWaySql(this string text, params object[] args) => InvalitContext.Throw<object>(nameof(ToSql));

        /// <summary>
        /// Get column name only.
        /// It's Removing table name and schema name.
        /// </summary>
        /// <typeparam name="T">column type.</typeparam>
        /// <param name="column">column.</param>
        /// <returns>Column name only.</returns>
        [SqlSyntaxColumnOnly]
        public static T ColumnOnly<T>(this T column) => InvalitContext.Throw<T>(nameof(ColumnOnly));
    }
    
    class SqlSyntaxToSqlAttribute : SqlSyntaxAttribute
    {
        public override ExpressionElement Convert(IExpressionConverter converter, MethodCallExpression method)
        {
            var text = (string)converter.ToObject(method.Arguments[0]);
            var array = method.Arguments[1] as NewArrayExpression;
            return new StringFormatText(text, array.Expressions.Select(e => converter.Convert(e)).ToArray());
        }
    }

    class SqlSyntaxTwoWaySqlAttribute : SqlSyntaxAttribute
    {
        public override ExpressionElement Convert(IExpressionConverter converter, MethodCallExpression method)
        {
            var obj = converter.ToObject(method.Arguments[0]);
            var text = TowWaySqlSpec.ToStringFormat((string)obj);
            var array = method.Arguments[1] as NewArrayExpression;
            return new StringFormatText(text, array.Expressions.Select(e => converter.Convert(e)).ToArray());
        }
    }

    class SqlSyntaxColumnOnlyAttribute : SqlSyntaxAttribute
    {
        public override ExpressionElement Convert(IExpressionConverter converter, MethodCallExpression method)
        {
            var col = converter.Convert(method.Arguments[0]) as DbColumnText;
            if (col == null) throw new NotSupportedException("invalid column.");
            return col.Customize(new CustomizeColumnOnly());
        }
    }
}
