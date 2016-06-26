using System.Linq.Expressions;

namespace LambdicSql.QueryInfo
{
    public class SelectElementInfo
    {
        public string Name { get; }
        public Expression Expression { get; }

        public SelectElementInfo(string name, Expression expression)
        {
            Name = name;
            Expression = expression;
        }
    }
}
