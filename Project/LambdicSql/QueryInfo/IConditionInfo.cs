namespace LambdicSql.QueryInfo
{
    public interface IConditionInfo
    {
        bool IsNot { get; }
        ConditionConnection ConditionConnection { get; }
    }
}
