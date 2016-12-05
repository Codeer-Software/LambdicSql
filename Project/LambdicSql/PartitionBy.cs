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

        static string ToString(ISqlStringConverter converter, NewExpression exp)
        {
            var arg = exp.Arguments[0];
            var array = arg as NewArrayExpression;
            return Environment.NewLine + "\tPARTITION BY" + Environment.NewLine + "\t\t" +
                string.Join("," + Environment.NewLine + "\t\t", array.Expressions.Select(e => converter.ToString(e)).ToArray());
        }
    }
}
