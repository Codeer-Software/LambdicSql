using System.Linq.Expressions;
using System.Linq;

namespace LambdicSql.QueryInfo
{
    public class GroupByInfo
    {
        Expression[] _elements;
        public Expression[] GetElements() => _elements.ToArray();

        public GroupByInfo(Expression[] elements)
        {
            _elements = elements;
        }
    }
}
