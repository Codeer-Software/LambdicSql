namespace LambdicSql.QueryInfo
{
    public interface ICondition
    {
        bool IsNot { get; }
        ConditionConnection ConditionConnection { get; }
    }
}
