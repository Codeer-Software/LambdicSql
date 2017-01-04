using LambdicSql.BuilderServices.Parts;
using System.Linq.Expressions;

namespace LambdicSql.ConverterServices.SqlSyntaxes
{
    /// <summary>
    /// SQL syntax attribute for keyword.
    /// </summary>
    public class SqlSyntaxKeywordMemberAttribute : SqlSyntaxConverterMemberAttribute
    {
        /// <summary>
        /// Name.If it is empty, use the name of the member.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Convert expression to building parts.
        /// </summary>
        /// <param name="expression">Expression.</param>
        /// <param name="converter">Expression converter.</param>
        /// <returns>BuildingParts.</returns>
        public override BuildingParts Convert(MemberExpression expression, ExpressionConverter converter)
            => string.IsNullOrEmpty(Name) ? expression.Member.Name.ToUpper() : Name;
    }
}
