using LambdicSql.SqlBuilder.Parts;
using System.Linq.Expressions;

namespace LambdicSql.ConverterService.SqlSyntaxes
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
        public override BuildingParts Convert(ExpressionConverter converter, MethodCallExpression method)
            => string.IsNullOrEmpty(Name) ? method.Method.Name.ToUpper() : Name;
    }
}
