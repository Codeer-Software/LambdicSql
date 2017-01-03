using LambdicSql.ConverterService.Inside;
using LambdicSql.SqlBuilder.Sentences;
using System.Linq.Expressions;

namespace LambdicSql.ConverterService
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TSelected"></typeparam>
    public class SqlRecursiveArgumentsExpression<TSelected> : SqlExpression<TSelected>
    {
        /// <summary>
        /// 
        /// </summary>
        public override DbInfo DbInfo { get; protected set; }

        /// <summary>
        /// 
        /// </summary>
        public override Sentence Sentence { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dbInfo"></param>
        /// <param name="core"></param>
        public SqlRecursiveArgumentsExpression(DbInfo dbInfo, Expression core)
        {
            DbInfo = dbInfo;
            var converter = new ExpressionConverter(dbInfo);
            if (core == null) Sentence = string.Empty;
            else Sentence = converter.Convert(core);
        }
    }
}
