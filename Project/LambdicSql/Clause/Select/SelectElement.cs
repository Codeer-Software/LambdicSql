using System.Linq.Expressions;

namespace LambdicSql.Clause.Select
{
    public class SelectElement
    {
        public string Name { get; }
        public Expression Expression { get; }

        public SelectElement(string name, Expression expression)
        {
            Name = name;
            Expression = expression;
        }
    }
}
