using System.Collections.Generic;
using System.Linq.Expressions;

namespace LambdicSql.QueryInfo
{
    public class OrderByClause
    {
        List<OrderByElement> _elements = new List<OrderByElement>();
        public OrderByElement[] GetElements() => _elements.ToArray();

        public OrderByClause Clone()
        {
            var clone = new OrderByClause();
            clone._elements.AddRange(_elements);
            return clone;
        }

        internal void Add(Order order, Expression element) => _elements.Add(new OrderByElement(order, element));
    }
}
