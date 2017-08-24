using System.Linq.Expressions;

namespace LambdicSql.ConverterServices
{
    /// <summary>
    /// Member information of object creation.
    /// </summary>
    public class ObjectCreateMemberInfo
    {
        /// <summary>
        /// Member name.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Expression became the basis.
        /// It is null when made from type information.
        /// </summary>
        public Expression Expression { get; }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="name">Member name.</param>
        /// <param name="expression">Expression became the basis.It is null when made from type information.</param>
        public ObjectCreateMemberInfo(string name, Expression expression)
        {
            Name = name;
            Expression = expression;
        }
    }
}
