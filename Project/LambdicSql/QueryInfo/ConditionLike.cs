using System.Linq.Expressions;

namespace LambdicSql.QueryInfo
{
    public class ConditionLike : ICondition
    {
        public bool IsNot { get; }
        public ConditionConnection ConditionConnection { get; }
        public Expression Target { get; }
        public object SearchText { get; }
        
        public ConditionLike(bool isNot, ConditionConnection connection, Expression target, object searchText)
        {
            IsNot = isNot;
            ConditionConnection = connection;
            Target = target;
            SearchText = searchText;
        }
    }
}
