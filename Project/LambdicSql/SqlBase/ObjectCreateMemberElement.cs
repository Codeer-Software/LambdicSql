using System.Linq.Expressions;

namespace LambdicSql.SqlBase
{
    public class ObjectCreateMemberElement
    {
        public string Name { get; }
        public Expression Expression { get; }

        public ObjectCreateMemberElement(string name, Expression expression)
        {
            Name = name;
            Expression = expression;
        }
    }
}
