using LambdicSql.SqlBase;
using System.Linq.Expressions;
using LambdicSql.SqlBase.TextParts;

namespace LambdicSql.Inside
{
    class SqlExpressionSingle<TSelected> : SqlExpression<TSelected>
    {
        public override DbInfo DbInfo { get; protected set; }
        public override SqlText SqlText { get; }

        public SqlExpressionSingle(DbInfo dbInfo, Expression core)
        {
            DbInfo = dbInfo;
            var converter = new SqlStringConverter(dbInfo);
            if (core == null) SqlText = string.Empty;
            else SqlText = converter.Convert(core);
        }
    }
}
