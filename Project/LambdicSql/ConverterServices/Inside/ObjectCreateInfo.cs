using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;

namespace LambdicSql.ConverterServices.Inside
{
    class ObjectCreateInfo
    {
        internal ObjectCreateMemberInfo[] Members { get; }
        internal Expression Expression { get; }

        internal ObjectCreateInfo(IEnumerable<ObjectCreateMemberInfo> members, Expression expression)
        {
            Members = members.ToArray();
            Expression = expression;
        }
    }
}
