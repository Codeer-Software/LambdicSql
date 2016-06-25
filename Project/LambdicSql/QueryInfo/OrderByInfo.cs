using System.Collections.Generic;

namespace LambdicSql.QueryInfo
{
    public class OrderByInfo
    {
        List<OrderByElement> _elements = new List<OrderByElement>();
        public IReadOnlyList<OrderByElement> Elements { get; }

        public OrderByInfo Clone()
        {
            var clone = new OrderByInfo();
            clone._elements.AddRange(_elements);
            return clone;
        }

        internal void Add(Order order, string element) => _elements.Add(new OrderByElement(order, element));
    }
}
