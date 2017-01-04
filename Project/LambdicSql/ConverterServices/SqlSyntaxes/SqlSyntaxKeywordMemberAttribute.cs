using LambdicSql.BuilderServices.Parts;
using System.Linq.Expressions;

namespace LambdicSql.ConverterServices.SqlSyntaxes
{
    /// <summary>
    /// 
    /// </summary>
    public class SqlSyntaxKeywordMemberAttribute : SqlSyntaxConverterMemberAttribute
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
        public override BuildingParts Convert(MemberExpression expression, ExpressionConverter converter)
            => string.IsNullOrEmpty(Name) ? expression.Member.Name.ToUpper() : Name;
    }

}
