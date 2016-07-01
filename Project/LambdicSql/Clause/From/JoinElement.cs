using System.Linq.Expressions;

namespace LambdicSql.Clause.From
{
    public class JoinElement
    {
        public Expression JoinTable { get; }
        public Expression Condition { get; }

        public JoinElement(Expression joinTable, Expression condition)
        {
            JoinTable = joinTable;
            Condition = condition;
        }
    }
}
