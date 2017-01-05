using LambdicSql.BuilderServices.Syntaxes;
using System.Linq.Expressions;

namespace LambdicSql.ConverterServices.SymbolConverters
{
    /// <summary>
    /// SQL symbol converter attribute for keyword.
    /// </summary>
    public class KeywordMemberConverterAttribute : SymbolConverterMemberAttribute
    {
        /// <summary>
        /// Name.If it is empty, use the name of the member.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Convert expression to syntax.
        /// </summary>
        /// <param name="expression">Expression.</param>
        /// <param name="converter">Expression converter.</param>
        /// <returns>Syntax.</returns>
        public override Syntax Convert(MemberExpression expression, ExpressionConverter converter)
            => string.IsNullOrEmpty(Name) ? expression.Member.Name.ToUpper() : Name;
    }
}
