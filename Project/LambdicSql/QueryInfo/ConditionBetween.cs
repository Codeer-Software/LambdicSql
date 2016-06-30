using System.Linq.Expressions;

namespace LambdicSql.QueryInfo
{
    public class ConditionBetween : ICondition
    {
        public bool IsNot { get; }
        public ConditionConnection ConditionConnection { get; }
        public Expression Target { get; }
        public object Min { get; }
        public object Max { get; }
        
        public ConditionBetween(bool isNot, ConditionConnection connection, Expression target, object min, object max)
        {
            IsNot = isNot;
            ConditionConnection = connection;
            Target = target;
            Min = min;
            Max = max;
        }
    }
}
