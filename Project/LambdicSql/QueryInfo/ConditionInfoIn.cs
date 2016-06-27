using System.Collections.Generic;
using System.Linq.Expressions;

namespace LambdicSql.QueryInfo
{
    public class ConditionInfoIn : IConditionInfo
    {
        public bool IsNot { get; }
        public ConditionConnection ConditionConnection { get; }
        public Expression Target { get; }
        public List<object> Arguments { get; }
        
        public ConditionInfoIn(bool isNot, ConditionConnection connection, Expression target, List<object> arguments)
        {
            IsNot = isNot;
            ConditionConnection = connection;
            Target = target;
            Arguments = arguments;
        }
    }
}
