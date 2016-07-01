using LambdicSql.QueryBase;

namespace LambdicSql.Clause.Condition
{
    public interface ICondition
    {
        bool IsNot { get; }
        ConditionConnection ConditionConnection { get; }
        string ToString(IExpressionDecoder decoder);
    }
}