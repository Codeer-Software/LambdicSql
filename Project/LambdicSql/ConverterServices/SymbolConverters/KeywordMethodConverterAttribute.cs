using LambdicSql.BuilderServices.Syntaxes;
using System.Linq.Expressions;

namespace LambdicSql.ConverterServices.SymbolConverters
{
    /// <summary>
    /// SQL symbol converter attribute for keyword.
    /// </summary>
    public class KeywordMethodConverterAttribute : SymbolConverterMethodAttribute
    {
        /// <summary>
        /// Name.If it is empty, use the name of the method.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Convert expression to syntax.
        /// </summary>
        /// <param name="expression">Expression.</param>
        /// <param name="converter">Expression converter.</param>
        /// <returns>Syntax.</returns>
        public override Syntax Convert(MethodCallExpression expression, ExpressionConverter converter)
            => string.IsNullOrEmpty(Name) ? expression.Method.Name.ToUpper() : Name;
    }
}
