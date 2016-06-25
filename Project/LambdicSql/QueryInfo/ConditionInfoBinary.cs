using System.Linq.Expressions;

namespace LambdicSql.QueryInfo
{
    public class ConditionInfoBinary : IConditionInfo
    {
        public bool IsNot { get; }
        public ConditionConnection ConditionConnection { get; }
        public BinaryExpression Expression { get; }

        public ConditionInfoBinary(bool isNot, ConditionConnection connection, BinaryExpression expression)
        {
            IsNot = isNot;
            ConditionConnection = connection;
            Expression = expression;
        }
    }
}
