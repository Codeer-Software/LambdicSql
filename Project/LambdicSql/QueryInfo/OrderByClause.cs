using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace LambdicSql.QueryInfo
{
    public class OrderByClause : IClause
    {
        List<OrderByElement> _elements = new List<OrderByElement>();
        public OrderByElement[] GetElements() => _elements.ToArray();

        internal void Add(Order order, Expression element) => _elements.Add(new OrderByElement(order, element));

        public IClause Clone()
        {
            var clone = new OrderByClause();
            clone._elements.AddRange(_elements);
            return clone;
        }

        public string ToString(IExpressionDecoder decoder)
            => GetElements().Length == 0 ?
                string.Empty :
                "ORDER BY " + Environment.NewLine + "\t" + string.Join("," + Environment.NewLine + "\t", GetElements().Select(e => ToString(decoder, e)).ToArray());

        string ToString(IExpressionDecoder decoder, OrderByElement element)
            => decoder.ToString(element.Target) + " " + element.Order;
    }
}
