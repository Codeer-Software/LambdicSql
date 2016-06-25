using System.Collections.Generic;

namespace LambdicSql.QueryInfo
{
    public class ConditionInfoIn : IConditionInfo
    {
        public bool IsNot { get; }
        public ConditionConnection ConditionConnection { get; }
        public string Target { get; }
        public IReadOnlyList<object> Arguments { get; }

        public ConditionInfoIn(bool isNot, ConditionConnection connection, string target, IReadOnlyList<object> arguments)
        {
            IsNot = isNot;
            ConditionConnection = connection;
            Target = target;
            Arguments = arguments;
        }
    }
}
