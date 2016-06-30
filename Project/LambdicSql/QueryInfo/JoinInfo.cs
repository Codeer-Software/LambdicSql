using System.Linq.Expressions;

namespace LambdicSql.QueryInfo
{
    public class JoinInfo
    {
        public Expression JoinTable { get; }
        public Expression Condition { get; }

        public JoinInfo(Expression joinTable, Expression condition)
        {
            JoinTable = joinTable;
            Condition = condition;
        }
    }
}
