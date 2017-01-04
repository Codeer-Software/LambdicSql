using System.Linq.Expressions;

namespace LambdicSql.ConverterServices.Inside
{
    class ObjectCreateMemberInfo
    {
        internal string Name { get; }
        internal Expression Expression { get; }

        internal ObjectCreateMemberInfo(string name, Expression expression)
        {
            Name = name;
            Expression = expression;
        }
    }
}
