using System.Linq.Expressions;

namespace LambdicSql.QueryInfo
{
    public class JoinInfo
    {
        public TableInfo JoinTable { get; }
        public Expression Condition { get; }

        public JoinInfo(TableInfo joinTable, Expression condition)
        {
            JoinTable = joinTable;
            Condition = condition;
        }
    }
}
