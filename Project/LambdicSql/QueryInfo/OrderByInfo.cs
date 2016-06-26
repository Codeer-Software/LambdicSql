using System.Collections.Generic;
using System.Linq.Expressions;

namespace LambdicSql.QueryInfo
{
    public class OrderByInfo
    {
        List<OrderByElement> _elements = new List<OrderByElement>();
        public IReadOnlyList<OrderByElement> Elements => _elements;

        public OrderByInfo Clone()
        {
            var clone = new OrderByInfo();
            clone._elements.AddRange(_elements);
            return clone;
        }

        internal void Add(Order order, Expression element) => _elements.Add(new OrderByElement(order, element));
    }
}
