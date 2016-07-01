using LambdicSql.QueryBase;
using System.Linq.Expressions;

namespace LambdicSql.Clause.Condition
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

        public string ToString(ISqlStringConverter decoder)
           => decoder.ToString(Expression);
    }
}
