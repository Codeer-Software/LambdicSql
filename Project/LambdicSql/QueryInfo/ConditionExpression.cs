using System.Linq.Expressions;

namespace LambdicSql.QueryInfo
{
    public class ConditionExpression : ICondition
    {
        public bool IsNot { get; }
        public ConditionConnection ConditionConnection { get; }
        public Expression Expression { get; }

        public ConditionExpression(bool isNot, ConditionConnection connection, Expression expression)
        {
            IsNot = isNot;
            ConditionConnection = connection;
            Expression = expression;
        }

        public string ToString(IExpressionDecoder decoder)
           => decoder.ToString(Expression);
    }
}
