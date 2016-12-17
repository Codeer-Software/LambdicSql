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
            //TODO
            var context = new SqlConvertingContext(dbInfo);
            var converter = new SqlStringConverter(context);
            if (core == null) SqlText = string.Empty;
            else SqlText = converter.Convert(core);
        }
    }
}
