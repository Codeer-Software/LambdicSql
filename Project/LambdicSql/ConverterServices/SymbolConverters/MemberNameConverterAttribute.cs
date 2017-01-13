using LambdicSql.BuilderServices.CodeParts;
using System.Linq.Expressions;

namespace LambdicSql.ConverterServices.SymbolConverters
{
    //TODO Keywordっていけてない
    /// <summary>
    /// SQL symbol converter attribute for keyword.
    /// </summary>
    public class MemberNameConverterAttribute : MemberConverterAttribute
    {
        /// <summary>
        /// Name.If it is empty, use the name of the member.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Convert expression to code.
        /// </summary>
        /// <param name="expression">Expression.</param>
        /// <param name="converter">Expression converter.</param>
        /// <returns>Parts.</returns>
        public override Code Convert(MemberExpression expression, ExpressionConverter converter)
            => string.IsNullOrEmpty(Name) ? expression.Member.Name.ToUpper() : Name;
    }
}
