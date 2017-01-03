using LambdicSql.ConverterService;
using LambdicSql.ConverterService.Inside;
using LambdicSql.SqlBuilder.Sentences;
using System.Linq.Expressions;

namespace LambdicSql.Inside
{
    class SqlExpressionSingle<TSelected> : SqlExpression<TSelected>
    {
        public override DbInfo DbInfo { get; protected set; }
        public override Sentence Sentence { get; }

        public SqlExpressionSingle(DbInfo dbInfo, Expression core)
        {
            DbInfo = dbInfo;
            var converter = new ExpressionConverter(dbInfo);
            if (core == null) Sentence = string.Empty;
            else Sentence = converter.Convert(core);
        }

        public SqlExpressionSingle(DbInfo dbInfo, Sentence core)
        {
            DbInfo = dbInfo;
            Sentence = core;
        }
    }
}
