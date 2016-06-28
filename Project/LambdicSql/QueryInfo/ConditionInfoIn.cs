using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;

namespace LambdicSql.QueryInfo
{
    public class ConditionInfoIn : IConditionInfo
    {
        object[] _arguments { get; }
        public bool IsNot { get; }
        public ConditionConnection ConditionConnection { get; }
        public Expression Target { get; }
        public object[] GetArguments() => _arguments.ToArray();

        public ConditionInfoIn(bool isNot, ConditionConnection connection, Expression target, object[] arguments)
        {
            IsNot = isNot;
            ConditionConnection = connection;
            Target = target;
            _arguments = arguments;
        }
    }
}
