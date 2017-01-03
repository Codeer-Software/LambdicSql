using LambdicSql.SqlBuilder.Sentences;
using System.Linq.Expressions;

namespace LambdicSql.ConverterService.SqlSyntaxes
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
        /// <param name="converter"></param>
        /// <param name="member"></param>
        /// <returns></returns>
        public override Sentence Convert(ExpressionConverter converter, MemberExpression member)
            => string.IsNullOrEmpty(Name) ? member.Member.Name.ToUpper() : Name;
    }

}
