using System.Linq.Expressions;

namespace LambdicSql.QueryInfo
{
    public class ConditionInfoExpression : IConditionInfo
    {
        public bool IsNot { get; }
        public ConditionConnection ConditionConnection { get; }
        public Expression Expression { get; }

        public ConditionInfoExpression(bool isNot, ConditionConnection connection, Expression expression)
        {
            IsNot = isNot;
            ConditionConnection = connection;
            Expression = expression;
        }
    }
}
