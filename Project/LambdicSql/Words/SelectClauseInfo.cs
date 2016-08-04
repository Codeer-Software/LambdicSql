using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;

namespace LambdicSql
{
    public class SelectClauseInfo
    {
        public SelectElement[] Elements { get; }
        public Expression Expression { get; }

        public SelectClauseInfo(IEnumerable<SelectElement> elements, Expression exp)
        {
            Elements = elements.ToArray();
            Expression = exp;
        }
    }
}
