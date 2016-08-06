using LambdicSql.Inside;
using LambdicSql.QueryBase;

namespace LambdicSql
{
    public class TwoWaySql
    {
        public static ISqlExpression Format(string sql, params ISqlExpression[] exps)
            => new SqlExpressionFormatText(TowWaySqlSpec.ToStringFormat(sql), exps);
    }
}
