using LambdicSql.BuilderServices.CodeParts;
using System.Linq.Expressions;

namespace LambdicSql.ConverterServices.SymbolConverters
{
    /// <summary>
    /// SQL symbol converter attribute for keyword.
    /// </summary>
    public class MethodNameConverterAttribute : MethodConverterAttribute
    {
        /// <summary>
        /// Name.If it is empty, use the name of the method.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Convert expression to code.
        /// </summary>
        /// <param name="expression">Expression.</param>
        /// <param name="converter">Expression converter.</param>
        /// <returns>Parts.</returns>
        public override Code Convert(MethodCallExpression expression, ExpressionConverter converter)
            => string.IsNullOrEmpty(Name) ? expression.Method.Name.ToUpper() : Name;
    }
}
