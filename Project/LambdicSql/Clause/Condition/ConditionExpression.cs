using LambdicSql.QueryBase;
using System.Linq.Expressions;

namespace LambdicSql.Clause.Condition
{
    public class ConditionExpression : ICondition
    {
        public bool IsNot { get; }
        public ConditionConnection ConditionConnection { get; }
        public Expression Expression { get; }
        public Parameters Parameters { get; }

        public ConditionExpression(bool isNot, ConditionConnection connection, Expression expression)
        {
            IsNot = isNot;
            ConditionConnection = connection;
            Expression = expression;
        }

        public ConditionExpression(bool isNot, ConditionConnection connection, Expression expression, Parameters parameters)
        {
            IsNot = isNot;
            ConditionConnection = connection;
            Expression = expression;
            Parameters = parameters;
        }
        
        public string ToString(ISqlStringConverter decoder)
           => decoder.ToString(Expression, Parameters);
    }
}
