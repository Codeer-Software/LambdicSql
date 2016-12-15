using LambdicSql.Inside;
using LambdicSql.SqlBase;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace LambdicSql
{
    //TODO この辺全部関数化
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

        static TextParts Convert(ISqlStringConverter converter, NewExpression exp)
        {
            var arg = exp.Arguments[0];
            var array = arg as NewArrayExpression;
            var texts = new VText();
            texts.Add("ORDER BY");
            var elements = new VText() { Indent = 1, Separator = "," };
            foreach (var e in array.Expressions.Select(e => converter.Convert(e)))
            {
                elements.Add(e);
            }
            texts.Add(elements);
            return texts;
        }
    }
}
