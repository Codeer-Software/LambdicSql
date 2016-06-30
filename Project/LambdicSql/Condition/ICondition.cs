namespace LambdicSql.Condition
{
    public interface ICondition
    {
        bool IsNot { get; }
        ConditionConnection ConditionConnection { get; }
        string ToString(IExpressionDecoder decoder);
    }
}