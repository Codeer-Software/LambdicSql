using System.Linq.Expressions;

namespace LambdicSql.Clause.From
{
    public class JoinElement
    {
        public JoinType JoinType { get; }
        public Expression JoinTable { get; }
        public Expression Condition { get; }

        public JoinElement(JoinType joinType, Expression joinTable, Expression condition)
        {
            JoinType = joinType;
            JoinTable = joinTable;
            Condition = condition;
        }
    }
}
