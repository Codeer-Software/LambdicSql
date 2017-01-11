using LambdicSql.ConverterServices.Inside;
using LambdicSql.BuilderServices.Parts;
using System.Linq;
using System.Linq.Expressions;
using static LambdicSql.BuilderServices.Inside.PartsFactoryUtils;

namespace LambdicSql.ConverterServices.SymbolConverters
{
    /// <summary>
    /// SQL symbol converter attribute for SQL Function.
    /// </summary>
    public class FuncConverterAttribute : SymbolConverterMethodAttribute
    {
        /// <summary>
        /// Indent.
        /// </summary>
        public int Indent { get; set; }

        /// <summary>
        /// Name.If it is empty, use the name of the method.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Separator between arguments.By default it uses ", ".
        /// </summary>
        public string Separator { get; set; } = ", ";

        /// <summary>
        /// It is the predicate attached at the end. By default it is empty.
        /// </summary>
        public string AfterPredicate { get; set; }

        /// <summary>
        /// Convert expression to code parts.
        /// </summary>
        /// <param name="expression">Expression.</param>
        /// <param name="converter">Expression converter.</param>
        /// <returns>Parts.</returns>
        public override CodeParts Convert(MethodCallExpression expression, ExpressionConverter converter)
        {
            var index = expression.SkipMethodChain(0);
            var args = expression.Arguments.Skip(index).Select(e => converter.Convert(e)).ToArray();
            var name = string.IsNullOrEmpty(Name) ? expression.Method.Name.ToUpper() : Name;

            var hArgs = new HParts(args) { Separator = Separator }.ConcatToBack(")");
            var parts = new HParts(Line(name, "("), hArgs) { IsFunctional = true, Indent = Indent };
            return string.IsNullOrEmpty(AfterPredicate) ? parts : LineSpace(parts, AfterPredicate);
        }
    }
}
