using System.Linq.Expressions;

namespace LambdicSql.QueryInfo
{
    public class JoinInfo
    {
        public TableInfo JoinTable { get; }
        public BinaryExpression Condition { get; }

        public JoinInfo(TableInfo joinTable, BinaryExpression condition)
        {
            JoinTable = joinTable;
            Condition = condition;
        }
    }
}
