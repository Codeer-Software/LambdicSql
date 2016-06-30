using System.Linq.Expressions;

namespace LambdicSql.Condition
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

        public string ToString(IExpressionDecoder decoder)
            => decoder.ToString(Target) + " BETWEEN " + decoder.ToStringObject(Min) + " AND " + decoder.ToStringObject(Max);
    }
}
