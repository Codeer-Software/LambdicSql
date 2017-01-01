using LambdicSql.ExpressionConverterService;
using LambdicSql.ExpressionConverterService.Inside;
using LambdicSql.SqlBuilder.ExpressionElements;
using System.Linq.Expressions;

namespace LambdicSql.Inside
{
    class SqlExpressionSingle<TSelected> : SqlExpression<TSelected>
    {
        public override DbInfo DbInfo { get; protected set; }
        public override ExpressionElement ExpressionElement { get; }

        public SqlExpressionSingle(DbInfo dbInfo, Expression core)
        {
            DbInfo = dbInfo;
            var converter = new ExpressionConverter(dbInfo);
            if (core == null) ExpressionElement = string.Empty;
            else ExpressionElement = converter.Convert(core);
        }

        public SqlExpressionSingle(DbInfo dbInfo, ExpressionElement core)
        {
            DbInfo = dbInfo;
            ExpressionElement = core;
        }
    }
}
