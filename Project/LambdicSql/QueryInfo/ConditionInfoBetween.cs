namespace LambdicSql.QueryInfo
{
    public class ConditionInfoBetween : IConditionInfo
    {
        public bool IsNot { get; }
        public ConditionConnection ConditionConnection { get; }
        public string Target { get; }
        public object Min { get; }
        public object Max { get; }

        public ConditionInfoBetween(bool isNot, ConditionConnection connection, string target, object min, object max)
        {
            IsNot = isNot;
            ConditionConnection = connection;
            Target = target;
            Min = min;
            Max = max;
        }
    }
}
