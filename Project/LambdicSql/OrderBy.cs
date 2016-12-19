using LambdicSql.Inside;
using LambdicSql.SqlBase;
using LambdicSql.SqlBase.TextParts;
using System.Linq;
using System.Linq.Expressions;

namespace LambdicSql
{
    /// <summary>
    /// 
    /// </summary>
    public interface IOrderBy : IClauseChain<Non> { }

    /*
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

        static SqlText Convert(ISqlStringConverter converter, NewExpression exp)
        {
            var orderBy = new VText();
            orderBy.Add("ORDER BY");

            var elements = new VText() { Indent = 1, Separator = "," };
            var array = exp.Arguments[0] as NewArrayExpression;
            foreach (var e in array.Expressions.Select(e => converter.Convert(e)))
            {
                elements.Add(e);
            }
            orderBy.Add(elements);

            return orderBy;
        }
    }*/
}
