using LambdicSql.ConverterServices.Inside;
using LambdicSql.BuilderServices.CodeParts;
using System.Linq;
using System.Linq.Expressions;

namespace LambdicSql.ConverterServices.SymbolConverters
{
    /// <summary>
    /// SQL symbol converter attribute for clause.
    /// </summary>
    public class ClauseConverterAttribute : MethodConverterAttribute
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
        /// Separator between arguments.By default it uses " ".
        /// </summary>
        public string Separator { get; set; } = " ";

        /// <summary>
        /// It is the predicate attached at the end. By default it is empty.
        /// </summary>
        public string AfterPredicate { get; set; }

        /// <summary>
        /// Convert expression to code.
        /// </summary>
        /// <param name="expression">Expression.</param>
        /// <param name="converter">Expression converter.</param>
        /// <returns>Parts.</returns>
        public override Code Convert(MethodCallExpression expression, ExpressionConverter converter)
        {
            var name = string.IsNullOrEmpty(Name) ? expression.Method.Name.ToUpper() : Name;
            name = name.Trim();

            var index = expression.SkipMethodChain(0);
            var args = expression.Arguments.Skip(index).Select(e => converter.Convert(e)).ToList();
            if (!string.IsNullOrEmpty(AfterPredicate)) args.Add(AfterPredicate);
            
            return new HCode(name, new HCode(args) { Separator = Separator }) { IsFunctional = true, Separator = " ", Indent = Indent };
        }
    }
}
