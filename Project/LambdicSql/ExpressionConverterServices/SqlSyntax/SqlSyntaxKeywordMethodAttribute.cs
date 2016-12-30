using LambdicSql.SqlBase;
using LambdicSql.SqlBase.TextParts;
using System.Linq.Expressions;

namespace LambdicSql.ExpressionConverterServices.SqlSyntax
{
    /// <summary>
    /// 
    /// </summary>
    public class SqlSyntaxKeywordMethodAttribute : SqlSyntaxMethodAttribute
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
