using LambdicSql.SqlBuilder.ExpressionElements;
using System.Linq.Expressions;

namespace LambdicSql.ExpressionConverterService.SqlSyntaxes
{
    /// <summary>
    /// 
    /// </summary>
    public class SqlSyntaxKeywordMethodAttribute : SqlSyntaxConverterMethodAttribute
    {
        /// <summary>
        /// 
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="converter"></param>
        /// <param name="method"></param>
        /// <returns></returns>
        public override ExpressionElement Convert(IExpressionConverter converter, MethodCallExpression method)
            => string.IsNullOrEmpty(Name) ? method.Method.Name.ToUpper() : Name;
    }
}
