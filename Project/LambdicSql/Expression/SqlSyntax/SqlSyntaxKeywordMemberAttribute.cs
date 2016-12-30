using LambdicSql.SqlBase;
using LambdicSql.SqlBase.TextParts;
using System.Linq.Expressions;

namespace LambdicSql.Expression.SqlSyntax
{
    /// <summary>
    /// 
    /// </summary>
    public class SqlSyntaxKeywordMemberAttribute : SqlSyntaxMemberAttribute
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
        public override ExpressionElement Convert(IExpressionConverter converter, MemberExpression member)
            => string.IsNullOrEmpty(Name) ? member.Member.Name.ToUpper() : Name;
    }

}
