using System.Linq.Expressions;
using System.Linq;

namespace LambdicSql.QueryInfo
{
    public class GroupByClause
    {
        Expression[] _elements;
        public Expression[] GetElements() => _elements.ToArray();

        public GroupByClause(Expression[] elements)
        {
            _elements = elements;
        }
    }
}
