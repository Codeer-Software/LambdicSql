namespace LambdicSql.QueryInfo
{
    public class ConditionInfoLike : IConditionInfo
    {
        public bool IsNot { get; }
        public ConditionConnection ConditionConnection { get; }
        public string Target { get; }
        public string SearchString { get; }

        public ConditionInfoLike(bool isNot, ConditionConnection connection, string target, string searchText)
        {
            IsNot = isNot;
            ConditionConnection = connection;
            Target = target;
            SearchString = searchText;
        }
    }
}
