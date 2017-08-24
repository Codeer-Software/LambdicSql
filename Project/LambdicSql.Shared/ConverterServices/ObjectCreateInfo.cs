using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;

namespace LambdicSql.ConverterServices
{
    /// <summary>
    /// Object generation information.
    /// </summary>
    public class ObjectCreateInfo
    {
        /// <summary>
        /// Members.
        /// </summary>
        public ObjectCreateMemberInfo[] Members { get; }

        /// <summary>
        /// Expression became the basis.
        /// It is null when made from type information.
        /// </summary>
        public Expression Expression { get; }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="members">Members.</param>
        /// <param name="expression">Expression became the basis.It is null when made from type information.</param>
        public ObjectCreateInfo(IEnumerable<ObjectCreateMemberInfo> members, Expression expression)
        {
            Members = members.ToArray();
            Expression = expression;
        }
    }
}
