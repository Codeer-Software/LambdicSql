using System.Linq.Expressions;

namespace LambdicSql.QueryInfo
{
    public class JoinClause
    {
        public Expression JoinTable { get; }
        public Expression Condition { get; }

        public JoinClause(Expression joinTable, Expression condition)
        {
            JoinTable = joinTable;
            Condition = condition;
        }
    }
}
