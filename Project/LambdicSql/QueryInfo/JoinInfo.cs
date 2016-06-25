using System.Linq.Expressions;

namespace LambdicSql.QueryInfo
{
    public class JoinInfo
    {
        public string JoinTable { get; }
        public BinaryExpression Condition { get; }

        public JoinInfo(string joinTable, BinaryExpression condition)
        {
            JoinTable = joinTable;
            Condition = condition;
        }
    }
}
