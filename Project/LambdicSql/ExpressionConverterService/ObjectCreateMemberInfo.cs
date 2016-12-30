using System.Linq.Expressions;

namespace LambdicSql.SqlBase
{
    /// <summary>
    /// Object create member info.
    /// </summary>
    public class ObjectCreateMemberInfo
    {
        /// <summary>
        /// Name.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Expression.
        /// </summary>
        public Expression Expression { get; }

        internal ObjectCreateMemberInfo(string name, Expression expression)
        {
            Name = name;
            Expression = expression;
        }
    }
}
