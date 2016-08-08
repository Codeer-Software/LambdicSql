using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;

namespace LambdicSql.SqlBase
{
    public class ObjectCreateInfo
    {
        public ObjectCreateMemberElement[] Elements { get; }
        public Expression Expression { get; }

        public ObjectCreateInfo(IEnumerable<ObjectCreateMemberElement> elements, Expression exp)
        {
            Elements = elements.ToArray();
            Expression = exp;
        }
    }
}
