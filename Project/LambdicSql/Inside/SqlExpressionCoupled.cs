using LambdicSql.ConverterService;
using LambdicSql.SqlBuilder.Sentences;

namespace LambdicSql.Inside
{
    class SqlExpressionCoupled<TSelected> : SqlExpression<TSelected>
    {
        public override DbInfo DbInfo { get; protected set; }
        public override Sentence Sentence { get; }

        public SqlExpressionCoupled(ISqlExpression before, ISqlExpression after)
        {
            if (before.DbInfo != null) DbInfo = before.DbInfo;
            else if (before.DbInfo != null) DbInfo = before.DbInfo;
            Sentence = new VSentence(before.Sentence, after.Sentence);
        }
    }
}
