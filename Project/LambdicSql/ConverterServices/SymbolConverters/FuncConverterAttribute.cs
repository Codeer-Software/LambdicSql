using LambdicSql.ConverterServices.Inside;
using LambdicSql.BuilderServices.Syntaxes;
using System.Linq;
using System.Linq.Expressions;
using static LambdicSql.BuilderServices.Syntaxes.Inside.SyntaxFactoryUtils;

namespace LambdicSql.ConverterServices.SymbolConverters
{
    /// <summary>
    /// SQL symbol converter attribute for SQL Function.
    /// </summary>
    public class FuncConverterAttribute : SymbolConverterMethodAttribute
    {
        /// <summary>
        /// Name.If it is empty, use the name of the method.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Separator between arguments.By default it uses ", ".
        /// </summary>
        public string Separator { get; set; } = ", ";

        /// <summary>
        /// Convert expression to syntax.
        /// </summary>
        /// <param name="expression">Expression.</param>
        /// <param name="converter">Expression converter.</param>
        /// <returns>Syntax.</returns>
        public override Syntax Convert(MethodCallExpression expression, ExpressionConverter converter)
        {
            var index = expression.SkipMethodChain(0);
            var args = expression.Arguments.Skip(index).Select(e => converter.Convert(e)).ToArray();
            var name = string.IsNullOrEmpty(Name) ? expression.Method.Name.ToUpper() : Name;

            var hArgs = new HSyntax(args) { Separator = Separator }.ConcatToBack(")");
            return new HSyntax(Line(name, "("), hArgs) { IsFunctional = true };
        }
    }
}
