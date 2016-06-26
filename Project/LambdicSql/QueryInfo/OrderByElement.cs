using System.Linq.Expressions;

namespace LambdicSql.QueryInfo
{
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
}
