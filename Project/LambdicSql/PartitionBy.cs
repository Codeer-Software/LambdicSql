using LambdicSql.Inside;
using LambdicSql.SqlBase;
using LambdicSql.SqlBase.TextParts;
using System.Linq;
using System.Linq.Expressions;

namespace LambdicSql
{
    /*
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

        static SqlText Convert(ISqlStringConverter converter, NewExpression exp)
        {
            var partitionBy = new VText();
            partitionBy.Add("PARTITION BY");

            var elements = new VText() { Indent = 1, Separator = "," };
            var array = exp.Arguments[0] as NewArrayExpression;
            foreach (var e in array.Expressions.Select(e => converter.Convert(e)))
            {
                elements.Add(e);
            }
            partitionBy.Add(elements);

            return partitionBy;
        }
    }*/

    /// <summary>
    /// 
    /// </summary>
    public interface IPartitionBy { }
}
