using LambdicSql.Inside;
using LambdicSql.SqlBase;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace LambdicSql
{
    /// <summary>
    /// PARTITION BY keyword.
    /// Use it with the OVER function.
    /// </summary>
    [SqlSyntax]
    public class PartitionBy
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="columns">Specify column or expression.</param>
        public PartitionBy(params object[] columns) { InvalitContext.Throw("new " + nameof(PartitionBy)); }

        static TextParts ToString(ISqlStringConverter converter, NewExpression exp)
        {
            var arg = exp.Arguments[0];
            var array = arg as NewArrayExpression;
            var texts = new VText();
            texts.Add("PARTITION BY");
            var elements = new VText(",") { Indent = 1 };
            foreach (var e in array.Expressions.Select(e => converter.ToString(e)))
            {
                elements.Add(e);
            }
            texts.Add(elements);
            return texts;
        }
    }
}
