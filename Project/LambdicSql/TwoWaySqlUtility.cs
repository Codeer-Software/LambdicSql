using LambdicSql.Inside;
using LambdicSql.SqlBase;

namespace LambdicSql
{
    public class TwoWaySqlUtility
    {
        public static ISqlExpression Format(string sql, params ISqlExpression[] exps)
            => new SqlExpressionFormatText(TowWaySqlSpec.ToStringFormat(sql), exps);
    }
}
