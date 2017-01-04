using LambdicSql.BuilderServices.Parts;
using System.Linq.Expressions;

namespace LambdicSql.ConverterServices.SqlSyntaxes
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
        /// <param name="expression"></param>
        /// <param name="converter"></param>
        /// <returns></returns>
        public override BuildingParts Convert(MethodCallExpression expression, ExpressionConverter converter)
            => string.IsNullOrEmpty(Name) ? expression.Method.Name.ToUpper() : Name;
    }
}
