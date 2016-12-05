using LambdicSql.Inside;
using LambdicSql.SqlBase;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace LambdicSql
{
    /// <summary>
    /// ORDERBY keyword.
    /// Use it with the OVER function.
    /// </summary>
    [SqlSyntax]
    public class OrderBy
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="elements">Specify column and sort order. Asc(column) or Desc(column).</param>
        public OrderBy(params ISortedBy[] elements) { InvalitContext.Throw("new " + nameof(OrderBy)); }

        static string ToString(ISqlStringConverter converter, NewExpression exp)
        {
            var arg = exp.Arguments[0];
            var array = arg as NewArrayExpression;
            return Environment.NewLine + "\tORDER BY" + Environment.NewLine + "\t\t" +
                string.Join("," + Environment.NewLine + "\t\t", array.Expressions.Select(e => converter.ToString(e)).ToArray());
        }
    }
}
