namespace LambdicSql.QueryInfo
{
    public class OrderByElement
    {
        public Order Order { get; }
        public string Target { get; }
        public OrderByElement(Order order, string element)
        {
            Order = order;
            Target = element;
        }
    }
}
