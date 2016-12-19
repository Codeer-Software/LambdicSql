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
    static class Utils
    {
        internal static SqlText Condition(ISqlStringConverter converter, MethodCallExpression method)
        {
            var obj = converter.ToObject(method.Arguments[0]);
            return (bool)obj ? converter.Convert(method.Arguments[1]) : (SqlText)string.Empty;
        }

        internal static SqlText TextSql(ISqlStringConverter converter, MethodCallExpression method)
        {
            var text = (string)converter.ToObject(method.Arguments[0]);
            var array = method.Arguments[1] as NewArrayExpression;
            return new StringFormatText(text, array.Expressions.Select(e => converter.Convert(e)).ToArray());
        }

        internal static SqlText TwoWaySql(ISqlStringConverter converter, MethodCallExpression method)
        {
            var obj = converter.ToObject(method.Arguments[0]);
            var text = TowWaySqlSpec.ToStringFormat((string)obj);
            var array = method.Arguments[1] as NewArrayExpression;
            return new StringFormatText(text, array.Expressions.Select(e => converter.Convert(e)).ToArray());
        }

        internal static SqlText ColumnOnly(ISqlStringConverter converter, MethodCallExpression method)
        {
            var col = converter.Convert(method.Arguments[0]) as DbColumnText;
            if (col == null) throw new NotSupportedException("invalid column.");
            return col.Customize(new CustomizeColumnOnly());
        }
    }
}
