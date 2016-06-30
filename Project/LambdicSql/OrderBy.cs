using LambdicSql.Inside;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace LambdicSql
{
    public enum Order
    {
        ASC,
        DESC
    }

    public class OrderByElement
    {
        public Order Order { get; }
        public Expression Target { get; }
        public OrderByElement(Order order, Expression element)
        {
            Order = order;
            Target = element;
        }
    }

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

    public interface IQueryOrderBy<TDB, TSelect> : IQuery<TDB, TSelect>
        where TDB : class
        where TSelect : class
    { }

    public static class OrderByQueryExtensions
    {
        public static IQueryOrderBy<TDB, TSelect> OrderBy<TDB, TSelect>(this IQuery<TDB, TSelect> query)
            where TDB : class
            where TSelect : class
            => query.CustomClone(dst => dst.OrderBy = new OrderByClause());

        public static T ToSubQuery<T>(this IQuery query) => default(T);

        public static IQueryOrderBy<TDB, TSelect> ASC<TDB, TSelect, T>(this IQueryOrderBy<TDB, TSelect> query, Expression<Func<TDB, T>> exp)
            where TDB : class
            where TSelect : class
            => query.CustomClone(dst => dst.OrderBy.Add(Order.ASC, exp.Body));

        public static IQueryOrderBy<TDB, TSelect> DESC<TDB, TSelect, T>(this IQueryOrderBy<TDB, TSelect> query, Expression<Func<TDB, T>> exp)
            where TDB : class
            where TSelect : class
            => query.CustomClone(dst => dst.OrderBy.Add(Order.DESC, exp.Body));
    }
}
